from machine import Pin, SPI

import os
import sdcard

SCK_PIN = 18
MOSI_PIN = 23
MISO_PIN = 19
CS_PIN = 5


vspi = SPI(2, sck=Pin(SCK_PIN), mosi=Pin(MOSI_PIN), miso=Pin(MISO_PIN))
sd = sdcard.SDCard(vspi, Pin(CS_PIN))
os.mount(sd, '/sd')
os.listdir('/sd')

f = open('/sd/data.txt', 'w')
f.write('some data')
f.close()