def write_addr_byte(addr):
    return (addr * 2) % 256


def read_addr_byte(addr):
    return (addr * 2) % 256 + 1


class I2cHelper(object):
    def __init__(self, i2c):
        self.iic = i2c

    def read_byte(self, addr, reg):
        self.iic.start()
        buf = self.iic.readfrom_mem(addr, reg, 1)
        self.iic.stop()
        return buf

    def read_byte_array(self, addr, reg, count):
        self.iic.start()
        a = self.iic.readfrom_mem(addr, reg, count)
        self.iic.stop()
        return a

    def write_byte(self, addr, reg, val):
        self.iic.start()
        self.iic.writeto_mem(addr, reg, bytearray(val))
        self.iic.stop()

    def write_byte_ack(self, addr, reg, val):
        self.iic.start()
        self.iic.write(bytearray([write_addr_byte(addr)]))
        self.iic.write(bytearray([reg]))
        self.iic.write(bytearray([val]))
        self.iic.stop()
