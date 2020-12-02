import pika
import socket
import time
import json
import sys
import requests
import urllib3
import multiprocessing
import os
from dotenv import load_dotenv

urllib3.disable_warnings(urllib3.exceptions.InsecureRequestWarning)


class Client():
    def __init__(self):
        self.commands = {1: self.command1, 2: self.command2, 3: self.command3}
        load_dotenv()
        self.botnet_id = os.getenv('BOTNET_ID')
        self.bot_id = os.getenv('BOT_ID')
        self.botnetjobs_address = os.getenv('BOTNETJOBS_ADDRESS')
        self.bots_address = os.getenv('BOTS_ADDRESS')
        rabbitmq_hostname = os.getenv('RABBITMQ_HOSTNAME')
        rabbitmq_port = os.getenv('RABBITMQ_PORT')
        self.botnet_job_id = 0
        self.credentials = pika.PlainCredentials('guest', 'guest')
        self.parameters = pika.ConnectionParameters(rabbitmq_hostname, rabbitmq_port, '/', self.credentials)
        self.connection = pika.BlockingConnection(self.parameters)
        self.job_process = None


    def init_check_for_jobs(self):
        url = "http://" + self.botnetjobs_address + "/api/v1/BotnetJob/" + str(self.botnet_id)
        print(url)
        bot_data = None

        while True:
            try:
                bot_data = requests.get(url=url, verify=False)
                break
            except:
                print("[-] No connection to jobs service. Retrying in 10 seconds...")
                time.sleep(10)

        if bot_data.status_code == 200:
            self.execute_command(None, None, None, bot_data.json())


    def register_bot(self):
        try:
            self.bot_registration_socket.connect(self.bot_registration_address)
            response = self.bot_registration_socket.recv(1024).decode()
            response = json.load(response)
            if response['status'] == 'failed':
                raise Exception('Registration failed')
            (self.botnet_id, self.bot_id) = (response['botnetId'], response['botId'])
        except:
            print("[-] Registration failed")
            sys.exit()


    def listen_for_commands(self):
        while True:
            try:
                self.init_check_for_jobs()
                break
            except:
                print("[-] No connection to jobs service")
        queue_name = "c2commands" + str(self.bot_id)
        exchange_name = "C2Commands" + str(self.botnet_id)
        channel = self.connection.channel()
        channel.exchange_declare(exchange=exchange_name, exchange_type='fanout')
        channel.queue_declare(queue=queue_name, exclusive=True)
        channel.queue_bind(exchange=exchange_name, queue=queue_name, routing_key='')
        channel.basic_consume(queue=queue_name, on_message_callback=self.execute_command, auto_ack=True)
        channel.start_consuming()


    def execute_command(self, ch, method, properties, body):
        try:
            body = body.decode()
            body = json.loads(body)
            self.botnet_job_id = body['Id']
            job_action = body['JobAction']
            command_id = body['CommandId']
            command_args = body['CommandArgument']
        except:
            self.botnet_job_id = body['id']
            job_action = body['jobAction']
            command_id = body['commandId']
            command_args = body['commandArgument']

        if job_action == "Stop":
            self.stop_job()
        elif job_action == "Start":
            print("[+] Got command")
            command_function = self.commands[command_id]
            self.job_process = multiprocessing.Process(target=command_function, args=(command_args,))
            self.job_process.start()
        else:
            print("[-] Unknown job action")


    def command_wrapper(command):
        def command_controller(self, command_args):
            url = "http://" + self.bots_address + "/api/v1/Bots/bot/" + str(self.bot_id)
            bot_data = requests.get(url=url, verify=False).json()
            bot_data['status'] = "Working"
            url = "http://" + self.bots_address + "/api/v1/Bots/" + str(self.bot_id)
            headers = {'Content-type':'application/json', 'Accept':'application/json'}
            requests.put(url=url, verify=False, json=bot_data, headers=headers)

            result = command(self, command_args)
            self.finish_command(result)
        return command_controller


    def stop_job(self):
        if self.job_process.is_alive():
            self.job_process.terminate()
            print("[*] Stopped command")
            self.finish_command("Stopped")


    def finish_command(self, result):
        url = "http://" + self.botnetjobs_address + "/api/v1/BotnetJob/" + str(self.botnet_job_id)
        requests.delete(url=url, verify=False)
        url = "http://" + self.bots_address + "/api/v1/Bots/bot/" + str(self.bot_id)
        bot_data = requests.get(url=url, verify=False).json()
        bot_data['status'] = "Waiting"
        url = "http://" + self.bots_address + "/api/v1/Bots/" + str(self.bot_id)
        headers = {'Content-type':'application/json', 'Accept':'application/json'}
        requests.put(url=url, verify=False, json=bot_data, headers=headers)


    @command_wrapper
    def command1(self, command_args):
        result = "Looped"
        print("Command 1 started")
        for i in range(6):
            print(f"Iteration {i}")
            time.sleep(1)
        print("Command finished")
        return result

    @command_wrapper
    def command2(self, command_args):
        result = "Looped"
        print("Command 2 started")
        for i in range(3):
            print(f"Iteration {i}")
            time.sleep(1)
        print("Command finished")
        return result

    @command_wrapper
    def command3(self, command_args):
        result = "Looped"
        print("Command 3 started")
        for i in range(10):
            print(f"Iteration {i}")
            time.sleep(3)
        print("Command finished")
        return result


if __name__ == "__main__":
    client = Client()
    client.listen_for_commands()

