EESchema Schematic File Version 4
EELAYER 30 0
EELAYER END
$Descr A4 11693 8268
encoding utf-8
Sheet 1 4
Title ""
Date ""
Rev ""
Comp ""
Comment1 ""
Comment2 ""
Comment3 ""
Comment4 ""
$EndDescr
$Comp
L Connector:USB_B_Micro J1
U 1 1 5ECECEDC
P 3600 3850
F 0 "J1" H 3657 4317 50  0000 C CNN
F 1 "614105150721" H 3657 4226 50  0000 C CNN
F 2 "PatternLibs:USB_Micro-B_Wuerth_614105150721_Vertical" H 3750 3800 50  0001 C CNN
F 3 "~" H 3750 3800 50  0001 C CNN
F 4 "614105150721" H 3600 3850 50  0001 C CNN "PartNumber"
	1    3600 3850
	1    0    0    -1  
$EndComp
$Comp
L power:GNDA #PWR01
U 1 1 5ECF805A
P 3500 4350
F 0 "#PWR01" H 3500 4100 50  0001 C CNN
F 1 "GNDA" H 3505 4177 50  0001 C CNN
F 2 "" H 3500 4350 50  0001 C CNN
F 3 "" H 3500 4350 50  0001 C CNN
	1    3500 4350
	1    0    0    -1  
$EndComp
$Comp
L power:GNDA #PWR02
U 1 1 5ECF8460
P 3600 4350
F 0 "#PWR02" H 3600 4100 50  0001 C CNN
F 1 "GNDA" H 3605 4177 50  0001 C CNN
F 2 "" H 3600 4350 50  0001 C CNN
F 3 "" H 3600 4350 50  0001 C CNN
	1    3600 4350
	1    0    0    -1  
$EndComp
Wire Wire Line
	3500 4250 3500 4350
Wire Wire Line
	3600 4250 3600 4350
Text Notes 7300 6850 0    79   ~ 0
Watch Dog!!!!
$Comp
L Device:LED_Small LD2
U 1 1 5EE29177
P 4350 5500
F 0 "LD2" H 4350 5650 50  0000 C CNN
F 1 "BLUE" H 4350 5300 50  0000 C CNN
F 2 "LED_SMD:LED_1206_3216Metric_Pad1.42x1.75mm_HandSolder" V 4350 5500 50  0001 C CNN
F 3 "~" V 4350 5500 50  0001 C CNN
F 4 "APTD3216LQBC" H 4300 5400 50  0000 C CNN "PartNumber"
	1    4350 5500
	-1   0    0    -1  
$EndComp
$Comp
L power:+3.3V #PWR03
U 1 1 5EE38577
P 3800 4700
F 0 "#PWR03" H 3800 4550 50  0001 C CNN
F 1 "+3.3V" H 3815 4873 50  0000 C CNN
F 2 "" H 3800 4700 50  0001 C CNN
F 3 "" H 3800 4700 50  0001 C CNN
	1    3800 4700
	1    0    0    -1  
$EndComp
Text Notes 3150 3250 0    50   ~ 0
Vertical Micro-USB:\n614105150721\nWE
$Sheet
S 5000 2000 1500 1150
U 5EF9D67C
F0 "Power" 50
F1 "Power.sch" 50
F2 "VBUS" U L 5000 2150 50 
F3 "I2C-SCL" B R 6500 2150 50 
F4 "i2C-SDA" B R 6500 2250 50 
F5 "PWR-BTN" I L 5000 2650 50 
F6 "MCU-PWR-CNTRL" I R 6500 2650 50 
F7 "VBUS-nPG" O R 6500 2750 50 
F8 "CHRG-STAT1" O R 6500 2850 50 
F9 "CHRG-STAT2" O R 6500 2950 50 
$EndSheet
$Sheet
S 5000 3550 1500 2450
U 5EFBEA8E
F0 "ESP32" 50
F1 "ESP32.sch" 50
F2 "VBUS" U L 5000 3650 50 
F3 "I2C-SCL" B R 6500 3650 50 
F4 "i2C-SDA" B R 6500 3750 50 
F5 "VBUS-nPG" O R 6500 4250 50 
F6 "CHRG-STAT1" O R 6500 4350 50 
F7 "CHRG-STAT2" O R 6500 4450 50 
F8 "MCU-PWR-CNTRL" I R 6500 4150 50 
F9 "USB-D+" B L 5000 3850 50 
F10 "USB-D-" B L 5000 3950 50 
F11 "nM6050-INT" I R 6500 4650 50 
F12 "nM9250-INT" I R 6500 4750 50 
F13 "LED-LINK" U L 5000 5500 50 
F14 "LED-CHRG-RED" U L 5000 4800 50 
F15 "LED-CHRG-GREEN" U L 5000 5000 50 
F16 "MPU6050_AD0" U R 6500 4850 50 
F17 "SPI-nCS" O R 6500 4950 50 
F18 "SPI-MISO" I R 6500 5050 50 
$EndSheet
$Sheet
S 8000 2000 1000 1500
U 5EFBEEB7
F0 "SENSORS" 50
F1 "SENSORS.sch" 50
F2 "I2C-SCL" B L 8000 2150 50 
F3 "i2C-SDA" B L 8000 2250 50 
F4 "nM9250-INT" O L 8000 3050 50 
F5 "nM6050-INT" O L 8000 2950 50 
F6 "MPU6050_AD0" U L 8000 3150 50 
F7 "SPI-nCS" I L 8000 3250 50 
F8 "SPI_MISO" O L 8000 3350 50 
$EndSheet
$Comp
L Switch:SW_SPST SW?
U 1 1 5F009D20
P 4700 2650
AR Path="/5EF9D67C/5F009D20" Ref="SW?"  Part="1" 
AR Path="/5F009D20" Ref="SW1"  Part="1" 
F 0 "SW1" H 4700 2885 50  0000 C CNN
F 1 "\"ON/OFF\"" H 4700 2794 50  0000 C CNN
F 2 "digikey-footprints:Switch_Tactile_SMD_6x6mm" H 4700 2650 50  0001 C CNN
F 3 "~" H 4700 2650 50  0001 C CNN
F 4 "5-1437565-0" H 4700 2650 50  0001 C CNN "PartNumber"
	1    4700 2650
	1    0    0    -1  
$EndComp
$Comp
L power:GNDA #PWR?
U 1 1 5F009D26
P 4500 3000
AR Path="/5EF9D67C/5F009D26" Ref="#PWR?"  Part="1" 
AR Path="/5F009D26" Ref="#PWR05"  Part="1" 
F 0 "#PWR05" H 4500 2750 50  0001 C CNN
F 1 "GNDA" H 4505 2827 50  0001 C CNN
F 2 "" H 4500 3000 50  0001 C CNN
F 3 "" H 4500 3000 50  0001 C CNN
	1    4500 3000
	1    0    0    -1  
$EndComp
Wire Wire Line
	4500 3000 4500 2650
Wire Wire Line
	4900 2650 5000 2650
Wire Wire Line
	5000 2150 4000 2150
Wire Wire Line
	3900 3650 4000 3650
Wire Wire Line
	4000 3650 4000 2150
Wire Wire Line
	3900 3850 5000 3850
Wire Wire Line
	5000 3950 3900 3950
Wire Wire Line
	5000 3650 4350 3650
Connection ~ 4000 3650
Wire Wire Line
	6500 2650 6600 2650
Wire Wire Line
	6600 2650 6600 4150
Wire Wire Line
	6600 4150 6500 4150
Wire Wire Line
	6500 2750 6700 2750
Wire Wire Line
	6700 2750 6700 4250
Wire Wire Line
	6700 4250 6500 4250
Wire Wire Line
	6500 2850 6800 2850
Wire Wire Line
	6800 2850 6800 4350
Wire Wire Line
	6800 4350 6500 4350
Wire Wire Line
	6500 2950 6900 2950
Wire Wire Line
	6900 2950 6900 4450
Wire Wire Line
	6900 4450 6500 4450
Wire Wire Line
	6500 2150 7000 2150
Wire Wire Line
	6500 2250 7100 2250
Wire Wire Line
	6500 3650 7000 3650
Wire Wire Line
	7000 3650 7000 2150
Connection ~ 7000 2150
Wire Wire Line
	7000 2150 8000 2150
Wire Wire Line
	6500 3750 7100 3750
Wire Wire Line
	7100 3750 7100 2250
Connection ~ 7100 2250
Wire Wire Line
	7100 2250 8000 2250
Wire Wire Line
	8000 3050 7400 3050
Wire Wire Line
	7400 3050 7400 4750
Wire Wire Line
	7400 4750 6500 4750
Wire Wire Line
	8000 2950 7300 2950
Wire Wire Line
	7300 2950 7300 4650
Wire Wire Line
	7300 4650 6500 4650
$Comp
L Device:LED_Dual_ACAC LD1
U 1 1 5EFE86BA
P 4350 4900
F 0 "LD1" H 4350 5150 50  0000 C CNN
F 1 "RED/GREEN" H 4350 4550 50  0000 C CNN
F 2 "PatternLibs:LED_Kingbright_APBD3224ESGC" H 4380 4900 50  0001 C CNN
F 3 "~" H 4380 4900 50  0001 C CNN
F 4 "APBD3224ESGC-F01" H 4350 4650 50  0000 C CNN "PartNumber"
	1    4350 4900
	1    0    0    -1  
$EndComp
Wire Wire Line
	3800 4700 3800 4800
Wire Wire Line
	3800 5000 4050 5000
Wire Wire Line
	4050 4800 3800 4800
Connection ~ 3800 4800
Wire Wire Line
	3800 4800 3800 5000
Wire Wire Line
	4650 4800 5000 4800
Wire Wire Line
	4650 5000 5000 5000
Wire Wire Line
	4250 5500 3800 5500
Wire Wire Line
	3800 5500 3800 5000
Connection ~ 3800 5000
Wire Wire Line
	4450 5500 5000 5500
Text Notes 3000 5000 0    50   Italic 0
100 % - GREEN\n50%  - YELLOW\n25% - RED
$Comp
L Device:D_TVS_ALT D1
U 1 1 5EFF5979
P 4350 4150
F 0 "D1" V 4304 4229 50  0000 L CNN
F 1 "SMBJ5.0CA" V 4395 4229 50  0000 L CNN
F 2 "Diode_SMD:D_SMB" H 4350 4150 50  0001 C CNN
F 3 "~" H 4350 4150 50  0001 C CNN
F 4 "SMBJ5.0CA" V 4350 4150 50  0001 C CNN "PartNumber"
	1    4350 4150
	0    1    1    0   
$EndComp
Wire Wire Line
	4350 4000 4350 3650
Connection ~ 4350 3650
Wire Wire Line
	4350 3650 4000 3650
$Comp
L power:GNDA #PWR04
U 1 1 5EFF986A
P 4350 4350
F 0 "#PWR04" H 4350 4100 50  0001 C CNN
F 1 "GNDA" H 4355 4177 50  0001 C CNN
F 2 "" H 4350 4350 50  0001 C CNN
F 3 "" H 4350 4350 50  0001 C CNN
	1    4350 4350
	1    0    0    -1  
$EndComp
Wire Wire Line
	4350 4350 4350 4300
Wire Wire Line
	6500 4850 7500 4850
Wire Wire Line
	7500 4850 7500 3150
Wire Wire Line
	7500 3150 8000 3150
Wire Wire Line
	8000 3250 7600 3250
Wire Wire Line
	7600 3250 7600 4950
Wire Wire Line
	7600 4950 6500 4950
Wire Wire Line
	6500 5050 7700 5050
Wire Wire Line
	7700 5050 7700 3350
Wire Wire Line
	7700 3350 8000 3350
Text Notes 3000 5500 0    50   Italic 0
WIFI STATUS
$EndSCHEMATC
