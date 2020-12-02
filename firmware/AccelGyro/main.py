## @package AccelGyro
#  Documentation for this module.

from ChargeMonitor import ChargeMonitor
from Accelerometer import Accelerometer as Accel
from CommandSocket import CommandSocket
from Config import *
import usocket as socket
import network
import ujson
import utime

from Calibration import Calibration
from I2cHelper import I2cHelper
from LedBlinker import *

## Documentation for a function.
#  @param network_name wireless network SSID
#  @param network_password wireless network password
#
#  This parameters should be configured with WIFI-router.
#  Don't change hardcoded params WIFI-router configuration recommended.
def init_network(led, network_name='IntemsLab', network_password='Embedded32'):
    led.set_state(NETWORK_CONNECTION_STATE)
    sta_if = network.WLAN(network.STA_IF)
    sta_if.active(True)
    if not sta_if.isconnected():
        print('connecting to network...')
        sta_if.connect(network_name, network_password)
        while not sta_if.isconnected():
            led.led_blink()
    print('network config:', sta_if.ifconfig())
    led.set_state(NETWORK_CONNECTED_STATE)
    return sta_if.ifconfig()[0]


def create_pkg(acc, amount):
    values = bytes()
    cnt = 0
    while cnt < amount:
        raw_val = acc.get_raw_values()
        d = acc.raw2dict_2(raw_val)
        t2 = utime.ticks_ms()
        pkg = t2.to_bytes(4, 'little') + bytes(raw_val)
        values += pkg
        cnt += 1
    return values, cnt


def send_amount(acc, led, amount=300, host='192.168.55.116', port=5000, command_port=5001):
    sock = None
    led.set_state(SENDING_STATE)

    command_sock = CommandSocket(host, command_port)
    exception_count = 0

    while True:
        try:
            total_send = 0
            sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            sock.connect((host, port))
            CommandSocket.effort_count = 0
            while True:
                print("loop")

                if command_sock.need_reconnect:
                    command_sock.connect()
                command_sock.handle_command()
                if command_sock.need_restart():
                    if sock is not None:
                        sock.close()
                    command_sock.close()
                    break

                if command_sock.is_running:
                    t1 = utime.ticks_ms()
                    values, cnt = create_pkg(acc, amount)
                    t3 = utime.ticks_ms()
                    sock.sendall(values)
                    led.send_blink()
                    t4 = utime.ticks_ms()

                    total_send += len(values)

                    print('count = ' + str(cnt))
                    print('sent_bytes = ' + str(len(values)))
                    print('total_send = ' + str(total_send))
                    print('loop_time = ' + str(utime.ticks_diff(t3, t1)))
                    print('socket_loop_time = ' + str(utime.ticks_diff(t4, t1)))
                    print(" ")

            if sock is not None:
                sock.close()
            command_sock.close()
            if command_sock.need_restart():
                break
        except Exception as e:
            print(e)
            led.led_blink()
            if e.args[0] == 113:
                exception_count += 20
            elif e.args[0] == 104:
                exception_count += 50
            else:
                exception_count += 1
            utime.sleep_ms(2)
            if sock is not None:
                sock.close()
            command_sock.close()
            if exception_count >= 50:
                break


def get_server_ip(led, port=15000):
    led.set_state(GETTING_SERVER_IP_STATE)
    sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    sock.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    sock.bind(("", port))
    data = []
    sock.settimeout(5)
    try_cnt = 10
    host = None
    while try_cnt > 0 and (data is None or len(data) <= 0):
        print("NO SERVER")
        try:
            data, addr = sock.recvfrom(1024)
            host = data.decode("utf-8")
            print("server_host: %s" % host)
        except Exception as e:
            try_cnt -= 1
            print(try_cnt)
            print(e)

        led.led_blink()
    sock.close()
    led.set_state(GOT_SERVER_IP_STATE)
    if host is None:
        print("no server ip")
    return host


def sync_info(server_ip, jbytes, server_port=9875):
    sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    sock.connect((server_ip, server_port))
    sock.send(jbytes)
    port = None
    command_port = None
    sock.settimeout(3)
    try:
        data = sock.recv(1024)
        jdict = ujson.loads(data.decode("utf-8"))
        print(jdict)
        if "Port" in jdict:
            port = jdict["Port"]
        if "CommandPort" in jdict:
            command_port = jdict["CommandPort"]
    except Exception as e:
        print(e)
    sock.close()
    return port, command_port


def main_loop(i2c, charge_monitor):
    led = LedBlinker()
    acc = Accel(i2c)
    gyro_offset, acc_offset = calibrate(acc, led)
    while True:
        try:
            self_ip = init_network(led, network_name=NetworkSSID, network_password=NetworkPassword)
            server_ip = get_server_ip(led)
            if server_ip is None:
                continue
            charge = charge_monitor.charge_percent()
            jdict = {'Id': DeviceId, 'Type': DeviceType, 'Ip': self_ip, 'SyncTicks': utime.ticks_ms(),
                     'BatteryCharge': charge,
                     'GyroOffset': gyro_offset, 'AccelOffset': acc_offset}
            jbytes = ujson.dumps(jdict).encode("utf-8")
            print(jbytes)
            server_port, command_port = sync_info(server_ip, jbytes)
            if server_port is not None:
                send_amount(acc, led, host=server_ip, port=server_port, command_port=command_port)
        except Exception as e:
            print(e)


def calibrate(acc, led):
    c = Calibration(acc, led)
    return c.calibration()


def main():
    while True:
        i2c = machine.I2C(scl=machine.Pin(22), sda=machine.Pin(21))
        charge_monitor = ChargeMonitor(i2c)
        if not charge_monitor.init():
            print("no charge controller device")
        else:
            while True:
                if charge_monitor.is_charging():
                    charge_monitor.set_charge_leds()
                    machine.deepsleep(60 * 1000)
                else:
                    main_loop(i2c, charge_monitor)
