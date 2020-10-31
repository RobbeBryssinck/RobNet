from dataclasses import dataclass
import socket

@dataclass
class Command:
    command_socket: socket
    commandId: int
    commandDescription: str
    userId: int
