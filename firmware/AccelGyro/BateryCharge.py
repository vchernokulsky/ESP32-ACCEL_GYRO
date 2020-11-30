import machine
import utime

from I2cHelper import I2cHelper

STC3100_ADDRESS = 0x70  # STC3100 8-bit address byte
STC3100_SLAVE_ADDRESS = 0xE0  # STC3100 7-bit address byte

SENSERESISTOR = 33  # sense resistor value in milliOhms (10min, 100max)

CurrentFactor = (48210 / SENSERESISTOR)
# LSB=11.77uV/R= ~48210/R/4096 - convert to mA

ChargeCountFactor = (27443 / SENSERESISTOR)
# LSB=6.7uVh/R ~27443/R/4096 - converter to mAh

VoltageFactor = 9994
# LSB=2.44mV ~9994/4096 - convert to mV

TemperatureFactor = 5120
# LSB=0.125°C ~5120/4096 - convert to 0.1°C

STC3100_OK = 0
STC3100_ERR = -1

# ------------------ Address of the STC3100 register -----------------------
STC3100_REG_MODE = 0x00  # Mode Register
STC3100_REG_CTRL = 0x01  # Control and Status Register
STC3100_REG_CHARGE_LOW = 0x02  # Gas Gauge Charge Data Bits 0-7
STC3100_REG_CHARGE_HIGH = 0x03  # Gas Gauge Charge Data Bits 8-15
STC3100_REG_COUNTER_LOW = 0x04  # Number of Conversion Bits 0-7
STC3100_REG_COUNTER_HIGH = 0x05  # Number of Conversion Bits 8-15
STC3100_REG_CURRENT_LOW = 0x06  # Battery Current Value Bits 0-7
STC3100_REG_CURRENT_HIGH = 0x07  # Battery Current Value Bits 8-15
STC3100_REG_VOLTAGE_LOW = 0x08  # Battery Voltage Value Bits 0-7
STC3100_REG_VOLTAGE_HIGH = 0x09  # Battery Voltage Value Bits 8-15
STC3100_REG_TEMPERATURE_LOW = 0x0A  # Temperature Values Bits 0-7)
STC3100_REG_TEMPERATURE_HIGH = 0x0B  # Temperature Values Bits 8-15)

# ----------------- Device ID registers Address 24 to 31 ------------------
STC3100_REG_ID0 = 0x18  # Part Type ID 10h
STC3100_REG_ID1 = 0x19  # Unique Part ID Bits 0-7
STC3100_REG_ID2 = 0x1A  # Unique Part ID Bits 8-15
STC3100_REG_ID3 = 0x1B  # Unique Part ID Bits 16-23
STC3100_REG_ID4 = 0x1C  # Unique Part ID Bits 24-31
STC3100_REG_ID5 = 0x1D  # Unique Part ID Bits 32-39
STC3100_REG_ID6 = 0x1E  # Unique Part ID Bits 40-47
STC3100_REG_ID7 = 0x1F  # Device ID CRC

#     General Purpose RAM Register Address 32-63
STC3100_RAM_SIZE = 32  # Total RAM register of STC3100

STC3100_REG_RAM0 = 0x20
STC3100_REG_RAM2 = 0x22
STC3100_REG_RAM4 = 0x24
STC3100_REG_RAM6 = 0x26
STC3100_REG_RAM8 = 0x28
STC3100_REG_RAM12 = 0x2C
STC3100_REG_RAM14 = 0x2E
STC3100_REG_RAM16 = 0x30
STC3100_REG_RAM18 = 0x32
STC3100_REG_RAM20 = 0x34
STC3100_REG_RAM22 = 0x36
STC3100_REG_RAM24 = 0x38
STC3100_REG_RAM26 = 0x3A
STC3100_REG_RAM28 = 0x3C
STC3100_REG_RAM30 = 0x3E


# *******************************************************************************
# * Function Name  : conv
# * Description    : conversion utility
# *  convert a raw 16-bit value from STC3100 registers into user units (mA, mAh, mV, °C)
# *  (optimized routine for efficient operation on 8-bit processors such as ST7/STM8)
# * Input          :s16_value, u16_factor
# * Return         : result = value * factor / 4096
# *******************************************************************************
def conv(val, factor):
    return round((val * factor) / 4096)


class ChargeController(object):
    def __init__(self, i2c, address=0x70):
        self.i2cHelper = I2cHelper(i2c)
        self.addr = address

    def init_device(self):
        # first, check the presence of the STC3100 by reading first byte of dev. ID
        dev_id = ord(self.read_byte(STC3100_REG_ID0))
        if dev_id != 0x10:
            print("NO_DEV")
            return STC3100_ERR
        # read the REG_CTRL to reset the GG_EOC and VTM_EOC bits
        ctr_reg = self.read_byte(STC3100_REG_CTRL)
        print(ctr_reg)
        # write 0x02 into the REG_CTRL to reset the accumulator and counter and clear the PORDET bit,
        self.write_byte(STC3100_REG_CTRL, 0x02)
        # then 0x10 into the REG_MODE register to start the STC3100 in 14-bit resolution mode.
        self.write_byte(STC3100_REG_MODE, 0x10)
        return STC3100_OK

    def get_dict_values(self):
        raw = self.get_raw_values()
        if raw is None or len(raw) != 10:
            return None
        # charge count
        print(raw)
        charge_count_raw = (raw[1] << 8) + raw[0]
        BattChargeCount = conv(charge_count_raw, ChargeCountFactor)  # result in mAh

        # conversion_counter
        BattCounter = (raw[3] << 8) + raw[2]

        # current
        current_val = (raw[5] << 8) + raw[4]
        current_val &= 0x3fff  # mask unused bits
        if current_val >= 0x2000:
            current_val -= 0x4000  # convert to signed value
        BattCurrent = conv(current_val, CurrentFactor)  # result in mA

        # voltage
        voltage_val = (raw[7] << 8) + raw[6]
        voltage_val &= 0x0fff  # mask unused bits
        if voltage_val >= 0x0800:
            voltage_val -= 0x1000  # convert to signed value
        BattVoltage = conv(voltage_val, VoltageFactor)  # result in mV

        # temperature
        temperature_val = (raw[9] << 8) + raw[8]
        temperature_val &= 0x0fff  # mask unused bits
        if temperature_val >= 0x0800:
            temperature_val -= 0x1000  # convert to signed value
        BattTemperature = conv(temperature_val, TemperatureFactor)  # result in 0.1°C
        return {
            "BattChargeCount": BattChargeCount,
            "BattCounter": BattCounter,
            "BattCurrent": BattCurrent,
            "BattVoltage": BattVoltage,
            "BattTemperature": BattTemperature}

    def turn_off(self):
        # rite 0 into the REG_MODE register to put the STC3100 in standby mode
        self.write_byte(STC3100_REG_MODE, 0)

    def read_byte(self, reg):
        return self.i2cHelper.read_byte(self.addr, reg)

    def get_raw_values(self):
        return self.i2cHelper.read_byte_array(self.addr, STC3100_REG_CHARGE_LOW, 10)

    def write_byte(self, reg, val):
        self.i2cHelper.write_byte(self.addr, reg, val)


def run_read():
    i2c = machine.I2C(scl=machine.Pin(22), sda=machine.Pin(21))
    scan_res = i2c.scan()
    print("scan_res: {}".format(scan_res))
    charge = None
    if STC3100_ADDRESS in scan_res:
        charge = ChargeController(i2c)
        if charge.init_device() == STC3100_OK:
            while True:
                print(charge.get_dict_values())
                utime.sleep_ms(1000)
        else:
            print("Charge controller was not inited")
    else:
        print("no STC_3100 in i2c devices")


"""
from BateryCharge import *
run_read()
"""
