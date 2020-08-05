## @package AccelGyro
#  Documentation for this module.
#
#  More details.
import utime
import machine

from ChargeMonitor import ChargeMonitor
from Accelerometer import Accelerometer as Accel
from Config import *
import usocket as socket
import network
import ujson
import uselect

from Calibration import Calibration

chrg = ChargeMonitor()


def init_acc():
    i2c = machine.I2C(scl=machine.Pin(22), sda=machine.Pin(21))
    return Accel(i2c)


## Documentation for a function.
#  @param network_name wireless network SSID
#  @param network_password wireless network password
#
#  This parameters should be configured with WIFI-router.
#  Don't change hardcoded params WIFI-router configuration recommended.
def init_network(network_name='IntemsLab', network_password='Embedded32'):
    global chrg
    sta_if = network.WLAN(network.STA_IF)
    sta_if.active(True)
    if not sta_if.isconnected():
        print('connecting to network...')
        sta_if.connect(network_name, network_password)
        while not sta_if.isconnected():
            chrg.check_charge()
            pass
    print('network config:', sta_if.ifconfig())
    return sta_if.ifconfig()[0]


def send_amount(acc, amount=300, host='192.168.55.116', port=5000):
    global chrg
    sock = None
    led = machine.Pin(27, machine.Pin.OUT)
    led_val = 0
    err_cnt = 0
    count_to_restart = 0
    led(led_val)
    while True:
        try:
            total_send = 0
            sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            sock.connect((host, port))
            while True:
                values = bytes()
                t1 = utime.ticks_ms()
                cnt = 0
                while cnt < amount:
                    raw_val = acc.get_raw_values()
                    t2 = utime.ticks_ms()
                    pkg = t2.to_bytes(4, 'little') + bytes(raw_val)
                    values += pkg
                    cnt += 1
                t3 = utime.ticks_ms()
                sock.send(values)
                led_val ^= 1
                led(led_val)
                t4 = utime.ticks_ms()

                total_send += len(values)

                print('count = ' + str(cnt))
                print('sent_bytes = ' + str(len(values)))
                print('total_send = ' + str(total_send))
                print('loop_time = ' + str(utime.ticks_diff(t3, t1)))
                print('socket_loop_time = ' + str(utime.ticks_diff(t4, t1)))
                print(" ")
                err_cnt = 0

        except Exception as e:
            print(e)
            chrg.check_charge()
            err_cnt += 1
            if e.args[0] == 113:
                count_to_restart += 20
            else:
                count_to_restart += 1
            if err_cnt >= 20:
                led_val ^= 1
                # led(led_val)
                err_cnt = 0
            utime.sleep_ms(2)
            if sock is not None:
                sock.close()
            if count_to_restart >= 50:
                break


def get_server_ip(port=15000):
    sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    sock.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
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
    global chrg
    led = machine.Pin(27, machine.Pin.OUT)
    led(1)
    chrg.check_charge()
    utime.sleep_ms(100)
    acc = init_acc()
    gyro_offset, acc_offset = calibrate(acc, led)
    while True:
        try:
            self_ip = init_network(network_name=NetworkSSID, network_password=NetworkPassword)
            led(1)
            server_ip = get_server_ip()
            jdict = {'Id': DeviceId, 'Type': DeviceType, 'Ip': self_ip, 'SyncTicks': utime.ticks_ms(), 'GyroOffset': gyro_offset, 'AccelOffset': acc_offset}
            jbytes = ujson.dumps(jdict).encode("utf-8")
            print(jbytes)
            server_port = sync_info(server_ip, jbytes)
            send_amount(acc, host=server_ip, port=server_port)
        except Exception as e:
            chrg.check_charge()
            print(e)


def calibrate(acc, led):
    Calib = Calibration(acc, led)
    return Calib.calibration()



