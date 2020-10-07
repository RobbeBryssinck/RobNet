class Bot():
    def __init__(self, address, bot_socket, user_id):
        self.address = address
        self.bot_socket = bot_socket
        self.user_id = user_id

    def execute_command(self, command):
        """
        Send signal to bot to execute command
        :param command: Command
        :return: bool
        """
        pass
