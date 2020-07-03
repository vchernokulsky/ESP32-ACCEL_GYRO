import utime
import machine
import uasyncio
from Accelerometer import Accelerometer as Accel
import usocket as socket
import network


def init():
    i2c = machine.I2C(scl=machine.Pin(22), sda=machine.Pin(21))
    return Accel(i2c)


def loop(acc, duration_ms=1000):
    values = []

    sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    sock.connect(('192.168.55.113', 5000))
    while True:
        # values.clear()
        # values = []
        t1 = utime.ticks_ms()
        dt = utime.ticks_diff(t1, t1)
        cnt = 0
        while dt < duration_ms:
            raw_val = acc.get_raw_values()
            t2 = utime.ticks_ms()
            dt = utime.ticks_diff(t2, t1)
            sock.send(raw_val)
            cnt += 1
            # values.append((t2, raw_val))
        # print(len(values))
        print(cnt)
        # await uasyncio.sleep(0.0)
    sock.close
    # return values


def send_duration(duration_ms=1000, host='192.168.55.113', port=5000):
    i2c = machine.I2C(scl=machine.Pin(22), sda=machine.Pin(21))
    acc = Accel(i2c)
    init_network()
    while True:
        try:
            sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            sock.connect((host, port))
            while True:
                values = bytes()
                t1 = utime.ticks_ms()
                dt = utime.ticks_diff(t1, t1)
                cnt = 0
                while dt < duration_ms:
                    raw_val = acc.get_raw_values()
                    t2 = utime.ticks_ms()
                    dt = utime.ticks_diff(t2, t1)
                    values += t2.to_bytes(4, 'little')
                    values += bytes(raw_val)
                    cnt += 1
                sock.send(values)
                t3 = utime.ticks_ms()
                print('count = ' + str(cnt))
                print('loop_time = ' + str(utime.ticks_diff(t3, t1)))
        except Exception as e:
            print(e)
        sock.close
    # return values


async def blink():
    led = machine.Pin(2, machine.Pin.OUT)
    while True:
        led(1)
        await uasyncio.sleep(0.5)
        led(0)
        await uasyncio.sleep(0.5)


async def print_cnt():
    for i in range(10):
        print(i)
        await uasyncio.sleep(1)

    import network
    wlan = network.WLAN(network.STA_IF)
    wlan.active(True)
    if not wlan.isconnected():
        print('connecting to network...')
        wlan.connect('essid', 'password')
        while not wlan.isconnected():
            pass
    print('network config:', wlan.ifconfig())


def init_network(network_name='IntemsLab', network_password='Embedded32'):
    sta_if = network.WLAN(network.STA_IF)
    sta_if.active(True)
    if not sta_if.isconnected():
        print('connecting to network...')
        sta_if.connect(network_name, network_password)
        while not sta_if.isconnected():
            pass
    print('network config:', sta_if.ifconfig())


def sendtcp(host, port):
    data = b'hello tcp'
    sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    sock.connect((host, port))
    sock.send(data)
    sock.close()


def run_tasks():
    acc = init()
    task_loop = uasyncio.get_event_loop()
    task_loop.create_task(loop(acc, 1500))
    task_loop.create_task(blink())
    task_loop.run_forever()
