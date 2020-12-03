import socket
import ujson

import utime

RUN_MSG = "socket_start"
STOP_MSG = "socket_stopp"
ASK_MSG = "socket_is_ok"

COUNT_TO_WAIT = 100


class CommandSocket(object):
    def __init__(self, host, port, charge_monitor):
        self.host = host
        self.port = port
        self.charge_monitor = charge_monitor

        self.is_running = False
        self.need_reconnect = True
        self.effort_count = 0

        self.command_sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

    def connect(self):
        self.command_sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.command_sock.connect((self.host, self.port))
        self.command_sock.setblocking(0)
        self.need_reconnect = False

    def close(self):
        self.need_reconnect = True
        if self.command_sock is not None:
            self.command_sock.close()

    def need_restart(self):
        return self.effort_count >= COUNT_TO_WAIT

    def handle_command(self):
        try:
            recv_bytes = self.command_sock.read(32)
            if recv_bytes is not None and len(recv_bytes) > 0:
                recv_str = recv_bytes.decode("utf-8")
                print("msg: {}".format(recv_str))
                if recv_str == RUN_MSG:
                    self.is_running = True
                    self.command_sock.sendall(RUN_MSG.encode("utf-8"))
                elif recv_str == STOP_MSG:
                    self.is_running = False
                    self.command_sock.sendall(STOP_MSG.encode("utf-8"))
                elif recv_str == ASK_MSG:
                    self.send_charge()
                else:
                    print("wrong msg: {}".format(recv_str))
                    self.effort_count += 1
                    utime.sleep_ms(100)
            else:
                print("no command")
                if not self.is_running:
                    self.effort_count += 1
                utime.sleep_ms(100)
        except Exception as e:
            print(e)
            self.close()

    def send_charge(self):
        charge = self.charge_monitor.charge_percent()
        jdict = {'BatteryCharge': charge}
        jbytes = ujson.dumps(jdict).encode("utf-8")
        self.command_sock.sendall(jbytes)

