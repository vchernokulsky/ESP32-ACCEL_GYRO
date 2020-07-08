import utime
import machine
from Accelerometer import Accelerometer as Accel
import usocket as socket
import network
import ujson
import uselect


def init_acc():
    i2c = machine.I2C(scl=machine.Pin(22), sda=machine.Pin(21))
    return Accel(i2c)


def init_network(network_name='Home92', network_password='24012017'):
    sta_if = network.WLAN(network.STA_IF)
    sta_if.active(True)
    if not sta_if.isconnected():
        print('connecting to network...')
        sta_if.connect(network_name, network_password)
        while not sta_if.isconnected():
            pass
    print('network config:', sta_if.ifconfig())
    return sta_if.ifconfig()[0]


def send_amount(amount=300, host='192.168.1.128', port=5000):
    acc = init_acc()
    sock = None
    led = machine.Pin(2, machine.Pin.OUT)
    led_val = 0
    err_cnt = 0
    led(led_val)
    while True:
        try:
            sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            sock.connect((host, port))
            while True:
                values = bytes()
                t1 = utime.ticks_ms()
                cnt = 0
                while cnt < amount:
                    raw_val = acc.get_raw_values()
                    t2 = utime.ticks_ms()
                    values += t2.to_bytes(4, 'little')
                    values += bytes(raw_val)
                    cnt += 1
                t3 = utime.ticks_ms()
                sock.send(values)
                led_val ^= 1
                led(led_val)
                t4 = utime.ticks_ms()

                print('count = ' + str(cnt))
                print('loop_time = ' + str(utime.ticks_diff(t3, t1)))
                print('socket_loop_time = ' + str(utime.ticks_diff(t4, t1)))
                print(" ")

        except Exception as e:
            #print(e)
            err_cnt += 1
            if err_cnt >= 20:
                led_val ^= 3
                led(led_val)
                err_cnt = 0
            utime.sleep_ms(2)
            if sock is not None:
                sock.close()


def udp_client(port=9876):
    init_network()

    sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    sock.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    # sock.setsockopt(socket.SOL_SOCKET, socket.SO_BROADCAST, 1)
    sock.bind(("", port))
    while True:
        # Thanks @seym45 for a fix
        data, addr = sock.recvfrom(1024)
        print("received message: %s" % data)


def get_server_ip(port=15000):
    sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    sock.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    # sock.setsockopt(socket.SOL_SOCKET, socket.SO_BROADCAST, 1)
    sock.bind(("", port))

    data, addr = sock.recvfrom(1024)
    sock.close()
    host = data.decode("utf-8")
    print("server_host: %s" % host)
    return host


def sync_info(server_ip, jbytes, server_port=9875):
    sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    sock.connect((server_ip, server_port))
    sock.send(jbytes)
    poller = uselect.poll()
    poller.register(sock, uselect.POLLIN)
    res = poller.poll(5000)
    port = None
    if res:
        data = res[0][0].recv(1024)
        jdict = ujson.loads(data.decode("utf-8"))
        if "Port" in jdict:
            port = jdict["Port"]
    print(port)
    sock.close()
    return port


def main():
    device_id = 1
    device_type = 1
    self_ip = init_network()
    server_ip = get_server_ip()

    jdict = {'Id': device_id, 'Type': device_type, 'Ip': self_ip}
    jbytes = ujson.dumps(jdict).encode("utf-8")
    print(jbytes)
    server_port = sync_info(server_ip, jbytes)
    send_amount(host=server_ip, port=server_port)

