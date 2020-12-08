import machine
import utime

from BateryCharge import *


class ChargeMonitor(object):

    def __init__(self, i2c):
        self.battery_charge = ChargeController(i2c)
        self.TIME_SLEEP = 5000
        self.ledR = machine.Pin(25, machine.Pin.OUT)
        self.ledG = machine.Pin(26, machine.Pin.OUT)
        self.stat1 = machine.Pin(35, machine.Pin.IN)
        self.stat2 = machine.Pin(33, machine.Pin.IN)
        self.vbus = machine.Pin(34, machine.Pin.IN)
        self.last_time = None
        self.red_val = 0
        self.green_val = 0

    def init(self):
        return self.battery_charge.init_device() == STC3100_OK

    def is_charging(self):
        self.battery_charge.is_charge()

    def charge_percent(self):
        return self.battery_charge.get_charge_percent()

    def check_charge(self):
        cur_time = utime.ticks_ms()
        if self.last_time is None or cur_time - self.last_time >= self.TIME_SLEEP:
            self.set_status()
            self.last_time = cur_time

    def set_status(self):
        state = 0
        if self.vbus.value() == 0:
            state = self.stat1.value() * 2 + self.stat2.value()
        # STAT1=0 STAT2=0 (Precharge)
        # LEDs OFF
        if state == 0:
            self.ledG(0)
            self.ledR(0)
        # STAT1=0 STAT2=1 (Fast charge)
        # GREEN LED
        elif state == 1:
            self.ledG(1)
            self.ledR(0)
        # STAT1=1 STAT2=0 (Charge done)
        # YELLOW LED
        elif state == 2:
            self.ledG(1)
            self.ledR(1)
        # STAT1=1 STAT2=1 (Charge suspend OR No vbus signal)
        # RED LED
        elif state == 3:
            self.ledG(0)
            self.ledR(1)

    def set_charge_leds(self):
        charge = self.battery_charge.get_charge_percent()
        count = 5
        delay = 500

        # RED LED
        if charge < 10:
            self.blink_red(count, delay)
        # YELLOW LED
        elif charge < 65:
            self.blink_yellow(count, delay)
        # GREEN LED
        else:
            self.blink_green(count, delay)

    def blink_red(self, count, delay):
        val = 0
        self.ledG(val)
        self.ledR(val)
        for i in range(2*count):
            val ^= 1
            self.ledR(val)
            utime.sleep_ms(delay)
        val = 0
        self.ledG(val)
        self.ledR(val)

    def blink_yellow(self, count, delay):
        val = 0
        self.ledG(val)
        self.ledR(val)
        for i in range(2*count):
            val ^= 1
            self.ledR(val)
            self.ledG(val)
            utime.sleep_ms(delay)
        val = 0
        self.ledG(val)
        self.ledR(val)

    def blink_green(self, count, delay):
        val = 0
        self.ledG(val)
        self.ledR(val)
        for i in range(2 * count):
            val ^= 1
            self.ledG(val)
            utime.sleep_ms(delay)
        val = 0
        self.ledG(val)
        self.ledR(val)


