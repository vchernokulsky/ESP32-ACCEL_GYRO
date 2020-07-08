#!/usr/bin/env python3

import sys
import socket
import json
import select

ID = 1
Type = 1
BroadcastServerPort = 15000


def read_args():
    if len(sys.argv) > 1:
        try:
            ID = int(sys.argv[1])
        except Exception as e:
            print(e)

    if len(sys.argv) > 2:
        try:
            Type = int(sys.argv[2])
        except Exception as e:
            print(e)


def init_network():
    return [l for l in ([ip for ip in socket.gethostbyname_ex(socket.gethostname())[2]
                         if not ip.startswith("127.")][:1], [[(s.connect(('8.8.8.8', 53)),
                                                               s.getsockname()[0], s.close()) for s in
                                                              [socket.socket(socket.AF_INET,
                                                                             socket.SOCK_DGRAM)]][0][1]]) if l][0][0]


def get_server_ip():
    client = socket.socket(socket.AF_INET, socket.SOCK_DGRAM, socket.IPPROTO_UDP)
    client.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEPORT, 1)

    # Enable broadcasting mode
    client.setsockopt(socket.SOL_SOCKET, socket.SO_BROADCAST, 1)

    client.bind(("", BroadcastServerPort))
    data, addr = client.recvfrom(1024)
    print("received message: %s" % data)
    host = data.decode("utf-8")
    return host


def sync_info(server_ip, jbytes, server_port=9875):
    sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    sock.connect((server_ip, server_port))
    sock.send(jbytes)
    poller = select.poll()
    poller.register(sock, select.POLLIN)
    res = poller.poll(5000)
    port = None
    if res:
        data = res[0][0].recv(1024)
        jdict = json.loads(data.decode("utf-8"))
        if "Port" in jdict:
            port = jdict["Port"]
    print(port)
    sock.close()
    return port


read_args()
self_ip = init_network()
server_ip = get_server_ip()

jdict = {'Id': id, 'Type': type, 'Ip': self_ip}
jbytes = json.dumps(jdict).encode("utf-8")
print(jbytes)
server_port = sync_info(server_ip, jbytes)

