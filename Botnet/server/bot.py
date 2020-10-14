import c2_pb2

class Bot():
    def __init__(self, address, bot_socket, user_id):
        self.address = address
        self.bot_socket = bot_socket
        self.user_id = user_id
        self.state = c2_pb2.Bot.State.WAITING
