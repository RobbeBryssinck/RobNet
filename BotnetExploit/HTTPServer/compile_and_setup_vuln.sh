#!/bin/sh

gcc vuln_server.c -o vuln_server -fno-stack-protector -z execstack -no-pie
sudo chown root ./vuln_server
sudo chmod u+s ./vuln_server
