import machine
import utime


LED_PIN = 27

START_STATE = 0
CALIBRATION_STATE = 1
CALIBRATED_STATE = 2
NETWORK_CONNECTION_STATE = 3
NETWORK_CONNECTED_STATE = 4
GETTING_SERVER_IP_STATE = 5
GOT_SERVER_IP_STATE = 6

## Documentation for a class.
# устанавливает разные режимы мигания в зависимости от текущего состояния.
# калибровка - быстро мигает
# подключение к wifi - очень быстро мигает
# чтение бродкаста для получение адреса сервера - очень медленно мигает (5с)
# синхронизация с сервером (отправка информации об устройсте, получение порта для отправки данных)
#       - медленно мигает (3с)
class LedBlinker(object):
    def __init__(self):
        self.led = machine.Pin(LED_PIN, machine.Pin.OUT)
        self.led_val = 1
        self.state = START_STATE
        self.led_divider = 1
        self.led_cnt = 1
        self.set_state(START_STATE)

    def set_state(self, state):
        self.state = state
        if state == START_STATE:
            self.led_val = 1
            self.led(self.led_val)
            utime.sleep_ms(1000)
        elif state == CALIBRATION_STATE:
            self.led_val = 0
            self.led_divider = 50
            self.led_cnt = self.led_divider
            self.led(self.led_val)
            utime.sleep_ms(1000)
        elif state == CALIBRATED_STATE:
            self.led_val = 0
            self.led(self.led_val)
            utime.sleep_ms(1000)
        elif state == NETWORK_CONNECTION_STATE:
            self.led_val = 1
            self.led_divider = 100
            self.led_cnt = self.led_divider
            self.led(self.led_val)
            utime.sleep_ms(1000)
        elif state == NETWORK_CONNECTED_STATE:
            self.led_val = 0
            self.led(self.led_val)
            utime.sleep_ms(1000)
        elif state == GETTING_SERVER_IP_STATE:
            self.led_val = 1
            self.led_divider = 1
            self.led_cnt = self.led_divider
            self.led(self.led_val)
            utime.sleep_ms(1000)
        elif state == GOT_SERVER_IP_STATE:
            self.led_val = 0
            self.led(self.led_val)
            utime.sleep_ms(1000)


    def led_blink(self):
        self.led_cnt -= 1
        if self.led_cnt <= 0:
            self.led_val ^= 1
            self.led(self.led_val)
            self.led_cnt = self.led_divider


