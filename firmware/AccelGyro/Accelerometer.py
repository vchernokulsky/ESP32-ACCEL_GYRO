import utime
from constants import *


class Accelerometer(object):
    def __init__(self, i2c, address=0x68):
        self.iic = i2c
        self.addr = address
        self.rate = 0x20
        self.iic.start()
        self.iic.writeto(self.addr, bytearray([107, 0]))
        self.iic.stop()
        # self.init_device()

    def init_device(self):
        print('* initializing mpu')

        self.identify()

        # disable sleep mode and select clock source
        self.write_byte(MPU6050_RA_PWR_MGMT_1, MPU6050_CLOCK_PLL_XGYRO)

        # enable all sensors
        self.write_byte(MPU6050_RA_PWR_MGMT_2, 0)

        # set sampling rate
        self.write_byte(MPU6050_RA_SMPLRT_DIV, self.rate)

        # enable dlpf
        self.write_byte(MPU6050_RA_CONFIG, 1)

        # explicitly set accel/gyro range
        self.set_accel_range(MPU6050_ACCEL_FS_2)
        self.set_gyro_range(MPU6050_GYRO_FS_250)

    def identify(self):
        print('* identifying i2c device')
        val = self.read_byte(MPU6050_RA_WHO_AM_I)
        if int.from_bytes(val, "little") != MPU6050_ADDRESS_AD0_LOW:
            raise OSError("No mpu6050 at address {}".format(self.addr))

    def write_byte(self, reg, val):
        self.iic.start()
        self.iic.writeto_mem(self.addr, reg, bytearray(val))
        self.iic.stop()

    def read_byte(self, reg):
        self.iic.start()
        buf = self.iic.readfrom_mem(self.addr, reg, 1)
        self.iic.stop()
        return buf

    def set_gyro_range(self, fsr):
        self.set_bitfield(MPU6050_RA_GYRO_CONFIG,
                          MPU6050_GCONFIG_FS_SEL_BIT,
                          MPU6050_GCONFIG_FS_SEL_LENGTH,
                          fsr)

    def set_accel_range(self, fsr):
        self.set_bitfield(MPU6050_RA_ACCEL_CONFIG,
                          MPU6050_ACONFIG_AFS_SEL_BIT,
                          MPU6050_ACONFIG_AFS_SEL_LENGTH,
                          fsr)

    def set_bitfield(self, reg, pos, length, val):
        old = self.read_byte(reg)
        shift = pos - length + 1
        mask = (2**length - 1) << shift
        new = (int.from_bytes(old, "little") & ~mask) | (val << shift)
        self.write_byte(reg, new)

    def get_raw_values(self):
        self.iic.start()
        a = self.iic.readfrom_mem(self.addr, 0x3B, 14)
        self.iic.stop()
        return a

    def get_ints(self):
        b = self.get_raw_values()
        c = []
        for i in b:
            c.append(i)
        return c

    def bytes_toint(self, firstbyte, secondbyte):
        if not firstbyte & 0x80:
            return firstbyte << 8 | secondbyte
        return - (((firstbyte ^ 255) << 8) | (secondbyte ^ 255) + 1)

    def get_values(self):
        pass
        raw_ints = self.get_raw_values()
        vals = {}
        vals["AcX"] = self.bytes_toint(raw_ints[0], raw_ints[1])
        vals["AcY"] = self.bytes_toint(raw_ints[2], raw_ints[3])
        vals["AcZ"] = self.bytes_toint(raw_ints[4], raw_ints[5])
        vals["Tmp"] = self.bytes_toint(raw_ints[6], raw_ints[7]) / 340.00 + 36.53
        vals["GyX"] = self.bytes_toint(raw_ints[8], raw_ints[9])
        vals["GyY"] = self.bytes_toint(raw_ints[10], raw_ints[11])
        vals["GyZ"] = self.bytes_toint(raw_ints[12], raw_ints[13])
        return vals  # returned in range of Int16
        # # -32768 to 32767

    def raw2dict(self, raw_ints):
        vals = {"AcX": self.bytes_toint(raw_ints[0], raw_ints[1]), "AcY": self.bytes_toint(raw_ints[2], raw_ints[3]),
                "AcZ": self.bytes_toint(raw_ints[4], raw_ints[5]), "Tmp": self.bytes_toint(raw_ints[6], raw_ints[7]),
                "GyX": self.bytes_toint(raw_ints[8], raw_ints[9]), "GyY": self.bytes_toint(raw_ints[10], raw_ints[11]),
                "GyZ": self.bytes_toint(raw_ints[12], raw_ints[13])}
        return vals  # returned in range of Int16

    def raw2dict_2(self, raw_ints):
        acc2si = 4.0*9.8/65536
        vel2si = 500.0/65536
        vals = {"AcX": self.bytes_toint(raw_ints[0], raw_ints[1])*acc2si, "AcY": self.bytes_toint(raw_ints[2], raw_ints[3])*acc2si,
                "AcZ": self.bytes_toint(raw_ints[4], raw_ints[5])*acc2si, "Tmp": self.bytes_toint(raw_ints[6], raw_ints[7]),
                "GyX": self.bytes_toint(raw_ints[8], raw_ints[9])*vel2si, "GyY": self.bytes_toint(raw_ints[10], raw_ints[11])*vel2si,
                "GyZ": self.bytes_toint(raw_ints[12], raw_ints[13])*vel2si}
        return vals  # returned in range of Int16

    def val_test(self):  # ONLY FOR TESTING! Also, fast reading sometimes crashes IIC
        from time import sleep
        while 1:
            print(self.get_values())
            sleep(0.05)

    def get_num_values(self, count=1, timeout=0):
        for i in range(count):
            print(self.get_values())
            utime.sleep_ms(timeout)

