#!/bin/bash

while true
do
    ./vuln_server
    echo "Server crashed! Restarting..."
    sleep 1
done
