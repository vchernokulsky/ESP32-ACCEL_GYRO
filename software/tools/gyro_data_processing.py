import pandas as pd
import matplotlib.pyplot as plt


def main():
    df = pd.read_csv('data-session-7.csv')
    dev1_gyro_x = df.GyroscopX[df.DeviceId == 1]
    dev2_gyro_x = df.GyroscopX[df.DeviceId == 2]
    dev1_gyro_y = df.GyroscopY[df.DeviceId == 1]
    dev2_gyro_y = df.GyroscopY[df.DeviceId == 2]
    dev1_gyro_z = df.GyroscopZ[df.DeviceId == 1]
    dev2_gyro_z = df.GyroscopZ[df.DeviceId == 2]

    _, plt1 = plt.subplots()
    plt1.plot(range(0, len(dev1_gyro_x)), dev1_gyro_x, label='D1 - axis X')
    plt1.plot(range(0, len(dev2_gyro_x)), dev2_gyro_x, label='D2 - axis X')
    plt1.plot(range(0, len(dev1_gyro_y)), dev1_gyro_y, label='D1 - axis Y')
    plt1.plot(range(0, len(dev2_gyro_y)), dev2_gyro_y, label='D2 - axis Y')
    plt1.plot(range(0, len(dev1_gyro_z)), dev1_gyro_z, label='D1 - axis Z')
    plt1.plot(range(0, len(dev2_gyro_z)), dev2_gyro_z, label='D2 - axis Z')
    plt1.legend()
    plt.show()


if __name__ == '__main__':
    main()
