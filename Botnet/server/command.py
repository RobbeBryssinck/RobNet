from dataclasses import dataclass
import socket

@dataclass
class Command:
    commandId: int
    commandDescription: str
    userId: int
    command_socket: socket
