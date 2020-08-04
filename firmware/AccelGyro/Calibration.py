import utime
import math

class Calibration(object):

    def __init__(self, acc, led):
        self.led = led
        self.led_val = 0
        self.led_max = 250
        self.led_cnt = self.led_max

        self.gyro_offs = {'x': 0, 'y': 0, 'z': 0}
        self.accel_offs = {'y': 0, 'x': 0, 'z': 0}

        self.acc = acc
        # Accel Range +-2g
        self.accel_scale = 16384.0
        # Accel Range +-4g
        # self.accel_scale = 8192.0
        # Accel Range +-8g
        # self.accel_scale=4096.0
        # Accel Range +-16g
        # self.accel_scale=2048.0

        # Gyro Range +-250 degrees/s
        self.gyro_scale = 131.0
        # Gyro Range +-500 degrees/s
        # self.gro_scale=65.5
        # Gyro Range +-1000 degrees/s
        # self.gro_scale=32.8
        # Gyro Range +-2000 degrees/s
        # self.gro_scale=16.4

    def get_accel_raw(self):
        acc_gyr = self.acc.get_values()
        data = {

            'x': acc_gyr['AcX'],
            'y': acc_gyr['AcY'],
            'z': acc_gyr['AcZ']}
        return data

    def get_gyro_offs(self):
        data_offs = {'x': 0, 'y': 0, 'z': 0}
        for num in range(0, 1000):
            data = self.acc.get_values()
            data_offs['x'] += data['GyX']
            data_offs['y'] += data['GyY']
            data_offs['z'] += data['GyZ']
            self.led_cnt -= 1
            if self.led_cnt <= 0:
                self.led_val ^= 1
                self.led(self.led_val)
                self.led_cnt = self.led_max
        data_offs['x'] /= 100
        data_offs['y'] /= 100
        data_offs['z'] /= 100
        return data_offs

    def get_gyro(self):
        acc_gyr = self.acc.get_values()
        data = {'x': (acc_gyr['GyX'] - self.gyro_offs['x']) / self.gyro_scale,
                'y': (acc_gyr['GyY'] - self.gyro_offs['y']) / self.gyro_scale,
                'z': (acc_gyr['GyZ'] - self.gyro_offs['z']) / self.gyro_scale}
        return data

    def get_accel_offs_avg(self):
        data = {'x': 0.0, 'y': 0.0, 'z': 0.0}
        for i in range(10000):
            acc = self.get_accel_raw()
            data['x'] = (data['x'] + acc['x']) / 2
            data['y'] = (data['y'] + acc['y']) / 2
            data['z'] = (data['z'] + acc['z']) / 2
            self.led_cnt -= 1
            if self.led_cnt <= 0:
                self.led_val ^= 1
                self.led(self.led_val)
                self.led_cnt = self.led_max
        data['z'] = data['z'] - self.accel_scale
        return data

    def get_accel_offs(self):
        data_offs_min = self.get_accel_raw()
        data_offs_max = self.get_accel_raw()
        for num in range(0, 10000):
            data = self.get_accel_raw()
            data_gyro = self.get_gyro()
            if math.fabs(data_gyro['x']) < 2 and math.fabs(data_gyro['y']) < 2 and math.fabs(data_gyro['z']) < 2:
                if data['x'] > data_offs_max['x']:
                    data_offs_max['x'] = data['x']
                if data['y'] > data_offs_max['y']:
                    data_offs_max['y'] = data['y']
                if data['z'] > data_offs_max['z']:
                    data_offs_max['z'] = data['z']
                if data['x'] < data_offs_min['x']:
                    data_offs_min['x'] = data['x']
                if data['y'] < data_offs_min['y']:
                    data_offs_min['y'] = data['y']
                if data['z'] < data_offs_min['z']:
                    data_offs_min['z'] = data['z']
            self.led_cnt -= 1
            if self.led_cnt <= 0:
                self.led_val ^= 1
                self.led(self.led_val)
                self.led_cnt = self.led_max
        data = {
                'x': data_offs_min['x'] + (data_offs_max['x'] - data_offs_min['x']) / 2,
                'y': data_offs_min['y'] + (data_offs_max['y'] - data_offs_min['y']) / 2,
                'z': data_offs_min['z'] + (data_offs_max['z'] - data_offs_min['z']) / 2}
        data['z'] = data['z'] - self.accel_scale
        return data

    def calibration(self):
        print("###############################################")
        print("Please do not move the MPU-6050 some secconds...")
        print("###############################################")
        utime.sleep_ms(5000)
        t1 = utime.ticks_ms()
        self.gyro_offs = self.get_gyro_offs()
        # print("Please twirl the MPU-6050 around a minute")
        # print("###############################################")
        self.accel_offs = self.get_accel_offs_avg()
        t2 = utime.ticks_ms()
        print(str(utime.ticks_diff(t2, t1)))
        # self.accel_offs = self.get_accel_offs_avg()
        print("Done. Please change variables")
        print("\"gyro_offs\" and \"accel_offs\"")
        print("to following values in mpu6050.py file:")
        print("###############################################")
        print("gyro_offs = ", self.gyro_offs)
        print("accel_offs = ", self.accel_offs)
        print("###############################################")
        self.led(0)
        return self.gyro_offs, self.accel_offs
