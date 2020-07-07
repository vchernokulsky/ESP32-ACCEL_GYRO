#!/usr/bin/env python3

import socket
import sys
import json

HOST = ''  # Standard loopback interface address (localhost)
PORT = 5000        # Port to listen on (non-privileged ports are > 1023)

jdict = {"port": 9874}
jstr = json.dumps(jdict)
jbytes = jstr.encode("utf-8")

if len(sys.argv) > 1:
    try:
        PORT = int(sys.argv[1])
    except Exception as e:
        print(e)


with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
    s.bind((HOST, PORT))
    s.listen()
    print('listening on PORT ' + str(PORT) + " ...")
    conn, addr = s.accept()
    with conn:
        print('Connected by', addr)
        while True:
            data = conn.recv(1024)
            print(data)
            conn.send(jbytes)
        conn.close()
        print('connection closed')

