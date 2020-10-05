import threading
import socket


class ServerManager():
    def __init__(self, command_entry_address, bot_entry_address):
        self.command_entry_address = command_entry_address
        self.bot_entry_address = bot_entry_address
        self.bot_clusters = {}
        self.commands = []
        self.c2servers = []

    def listen_for_commands(self):
        """
        Listen for incoming commands
        :return: None
        """
        pass

    def listen_for_bots(self):
        """
        Listen for incoming bots
        :return: None
        """
        pass

    def spawn_c2server(self, bot_socket):
        """
        Spawn new C2 server when no available bot cluster is present
        :return: None
        """
        pass



if __name__ == "__main__":
    command_entry_address = ('', 4590)
    bot_entry_address = ('', 4591)
    serverManager = ServerManager(command_entry_address, bot_entry_address)
    command_thread = threading.Thread(target=serverManager.listen_for_commands)
    command_thread.start()
    bot_thread = threading.Thread(target=serverManager.listen_for_bots)
    bot_thread.start()
