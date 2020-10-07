import threading

import botnet_exceptions


class C2server():

    def __init__(self, user_id):
        self.bots = []
        self.user_id = user_id

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
            bot_thread = threading.Thread(bot.execute_command, args=(command))
            bot_thread.start()
