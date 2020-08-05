import machine
import utime


class ChargeMonitor(object):

    def __init__(self):
        self.TIME_SLEEP = 5000
        self.ledR = machine.Pin(25, machine.Pin.OUT)
        self.ledG = machine.Pin(26, machine.Pin.OUT)
        self.stat1 = machine.Pin(35, machine.Pin.IN)
        self.stat2 = machine.Pin(33, machine.Pin.IN)
        self.vbus = machine.Pin(34, machine.Pin.IN)
        self.last_time = utime.ticks_ms()

    def check_charge(self):
        cur_time = utime.ticks_ms()
        if cur_time - self.last_time >= self.TIME_SLEEP:
            self.set_status()
            self.last_time = cur_time

    def set_status(self):
        state = 0
        if self.vbus.value() == 0:
            state = self.stat1.value() * 2 + self.stat2.value()
        if state == 0:
            self.ledG(0)
            self.ledR(0)
        elif state == 1:
            self.ledG(1)
            self.ledR(0)
        elif state == 2:
            self.ledG(1)
            self.ledR(1)
        elif state == 3:
            self.ledG(0)
            self.ledR(1)
