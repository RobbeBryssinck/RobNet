#/bin/python3

import socket

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.connect(('192.168.175.128', 5545))
s.sendall(b"192.168.175.130")
print(s.recv(1024).decode())
