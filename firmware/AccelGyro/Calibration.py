import utime
import math


class Calibration(object):

    def __init__(self, acc):
        self.gyro_offs = {'x': 0, 'y': 0, 'z': 0}
        self.accel_offs = {'y': 0, 'x': 0, 'z': 0}

        self.acc = acc

        # Gyro Range +-250 degrees/s
        self.gyro_scale = 131
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
        for num in range(0, 100):
            data = self.acc.get_values()
            data_offs['x'] += data['GyX']
            data_offs['y'] += data['GyY']
            data_offs['z'] += data['GyZ']
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

        data = {
            'x': data_offs_min['x'] + (data_offs_max['x'] - data_offs_min['x']) / 2,
            'y': data_offs_min['y'] + (data_offs_max['y'] - data_offs_min['y']) / 2,
            'z': data_offs_min['z'] + (data_offs_max['z'] - data_offs_min['z']) / 2}
        return data

    def calibration(self):
        print("###############################################")
        print("Please do not move the MPU-6050 some secconds...")
        print("###############################################")
        utime.sleep_ms(5000)
        self.gyro_offs = self.get_gyro_offs()
        print("Please twirl the MPU-6050 around a minute")
        print("###############################################")
        self.accel_offs = self.get_accel_offs()
        print("Done. Please change variables")
        print("\"gyro_offs\" and \"accel_offs\"")
        print("to following values in mpu6050.py file:")
        print("###############################################")
        print("gyro_offs = ", self.gyro_offs)
        print("accel_offs = ", self.accel_offs)
        print("###############################################")