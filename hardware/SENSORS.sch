EESchema Schematic File Version 4
EELAYER 30 0
EELAYER END
$Descr A4 11693 8268
encoding utf-8
Sheet 4 4
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
L Sensor_Motion:MPU-6050 U?
U 1 1 5EFD21B6
P 4300 3500
AR Path="/5EFD21B6" Ref="U?"  Part="1" 
AR Path="/5EFBEEB7/5EFD21B6" Ref="U8"  Part="1" 
F 0 "U8" H 4550 4100 50  0000 C CNN
F 1 "MPU-6050" H 4600 2900 50  0000 C CNN
F 2 "Sensor_Motion:InvenSense_QFN-24_4x4mm_P0.5mm" H 4300 2700 50  0001 C CNN
F 3 "https://store.invensense.com/datasheets/invensense/MPU-6050_DataSheet_V3%204.pdf" H 4300 3350 50  0001 C CNN
F 4 "MPU-6050" H 4300 3500 50  0001 C CNN "PartNumber"
	1    4300 3500
	1    0    0    -1  
$EndComp
$Comp
L Sensor_Motion:MPU-9250 U?
U 1 1 5EFD21BC
P 7800 3300
AR Path="/5EFD21BC" Ref="U?"  Part="1" 
AR Path="/5EFBEEB7/5EFD21BC" Ref="U9"  Part="1" 
F 0 "U9" H 8100 4100 50  0000 C CNN
F 1 "DNP (MPU-9250 )" H 8100 2500 50  0000 C CNN
F 2 "Sensor_Motion:InvenSense_QFN-24_3x3mm_P0.4mm" H 7800 2300 50  0001 C CNN
F 3 "https://store.invensense.com/datasheets/invensense/MPU9250REV1.0.pdf" H 7800 3150 50  0001 C CNN
F 4 "DNP" H 7800 3300 50  0001 C CNN "PartNumber"
	1    7800 3300
	1    0    0    -1  
$EndComp
$Comp
L Device:C_Small C?
U 1 1 5EFDA998
P 5100 4100
AR Path="/5EFDA998" Ref="C?"  Part="1" 
AR Path="/5EFBE643/5EFDA998" Ref="C?"  Part="1" 
AR Path="/5EFBEEB7/5EFDA998" Ref="C22"  Part="1" 
F 0 "C22" H 5192 4237 50  0000 L CNN
F 1 "100n" H 5192 4146 50  0000 L CNN
F 2 "Capacitor_SMD:C_0603_1608Metric" H 5100 4100 50  0001 C CNN
F 3 "~" H 5100 4100 50  0001 C CNN
F 4 "CC0603KRX7R9BB104" H 5200 3800 50  0001 L CNN "PartNumber"
F 5 "X7R" H 5192 4055 50  0000 L CNN "Tolerance"
F 6 "10VDC" H 5192 3964 50  0000 L CNN "Ratio"
	1    5100 4100
	1    0    0    -1  
$EndComp
$Comp
L power:GNDA #PWR?
U 1 1 5EFDA9A2
P 5100 4300
AR Path="/5EFDA9A2" Ref="#PWR?"  Part="1" 
AR Path="/5EFBE643/5EFDA9A2" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEEB7/5EFDA9A2" Ref="#PWR069"  Part="1" 
F 0 "#PWR069" H 5100 4050 50  0001 C CNN
F 1 "GNDA" H 5105 4127 50  0000 C CNN
F 2 "" H 5100 4300 50  0001 C CNN
F 3 "" H 5100 4300 50  0001 C CNN
	1    5100 4300
	1    0    0    -1  
$EndComp
Wire Wire Line
	5100 4000 5100 3800
Wire Wire Line
	5100 4200 5100 4300
$Comp
L Device:C_Small C?
U 1 1 5EFDBCF1
P 3800 2500
AR Path="/5EFDBCF1" Ref="C?"  Part="1" 
AR Path="/5EFBE643/5EFDBCF1" Ref="C?"  Part="1" 
AR Path="/5EFBEEB7/5EFDBCF1" Ref="C20"  Part="1" 
F 0 "C20" H 3892 2637 50  0000 L CNN
F 1 "10n" H 3892 2546 50  0000 L CNN
F 2 "Capacitor_SMD:C_0603_1608Metric" H 3800 2500 50  0001 C CNN
F 3 "~" H 3800 2500 50  0001 C CNN
F 4 "CC0603KRX7R9BB103" H 3900 2200 50  0001 L CNN "PartNumber"
F 5 "X7R" H 3892 2455 50  0000 L CNN "Tolerance"
F 6 "10VDC" H 3892 2364 50  0000 L CNN "Ratio"
	1    3800 2500
	1    0    0    -1  
$EndComp
$Comp
L power:GNDA #PWR?
U 1 1 5EFDBCFB
P 3800 2700
AR Path="/5EFDBCFB" Ref="#PWR?"  Part="1" 
AR Path="/5EFBE643/5EFDBCFB" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEEB7/5EFDBCFB" Ref="#PWR065"  Part="1" 
F 0 "#PWR065" H 3800 2450 50  0001 C CNN
F 1 "GNDA" H 3805 2527 50  0000 C CNN
F 2 "" H 3800 2700 50  0001 C CNN
F 3 "" H 3800 2700 50  0001 C CNN
	1    3800 2700
	1    0    0    -1  
$EndComp
Wire Wire Line
	3800 2400 3800 2300
Wire Wire Line
	3800 2600 3800 2700
Wire Wire Line
	4800 2600 4800 2700
Wire Wire Line
	4800 2400 4800 2300
$Comp
L power:GNDA #PWR?
U 1 1 5EFDA131
P 4800 2700
AR Path="/5EFDA131" Ref="#PWR?"  Part="1" 
AR Path="/5EFBE643/5EFDA131" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEEB7/5EFDA131" Ref="#PWR068"  Part="1" 
F 0 "#PWR068" H 4800 2450 50  0001 C CNN
F 1 "GNDA" H 4805 2527 50  0000 C CNN
F 2 "" H 4800 2700 50  0001 C CNN
F 3 "" H 4800 2700 50  0001 C CNN
	1    4800 2700
	1    0    0    -1  
$EndComp
$Comp
L Device:C_Small C?
U 1 1 5EFDA127
P 4800 2500
AR Path="/5EFDA127" Ref="C?"  Part="1" 
AR Path="/5EFBE643/5EFDA127" Ref="C?"  Part="1" 
AR Path="/5EFBEEB7/5EFDA127" Ref="C21"  Part="1" 
F 0 "C21" H 4892 2637 50  0000 L CNN
F 1 "100n" H 4892 2546 50  0000 L CNN
F 2 "Capacitor_SMD:C_0603_1608Metric" H 4800 2500 50  0001 C CNN
F 3 "~" H 4800 2500 50  0001 C CNN
F 4 "CC0603KRX7R9BB104" H 4900 2200 50  0001 L CNN "PartNumber"
F 5 "X7R" H 4892 2455 50  0000 L CNN "Tolerance"
F 6 "10VDC" H 4892 2364 50  0000 L CNN "Ratio"
	1    4800 2500
	-1   0    0    -1  
$EndComp
$Comp
L power:GNDA #PWR?
U 1 1 5EFDD001
P 4300 4300
AR Path="/5EFDD001" Ref="#PWR?"  Part="1" 
AR Path="/5EFBE643/5EFDD001" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEEB7/5EFDD001" Ref="#PWR066"  Part="1" 
F 0 "#PWR066" H 4300 4050 50  0001 C CNN
F 1 "GNDA" H 4305 4127 50  0000 C CNN
F 2 "" H 4300 4300 50  0001 C CNN
F 3 "" H 4300 4300 50  0001 C CNN
	1    4300 4300
	1    0    0    -1  
$EndComp
Wire Wire Line
	4200 2800 4200 2300
Wire Wire Line
	4200 2300 3800 2300
Wire Wire Line
	4400 2800 4400 2300
Wire Wire Line
	4400 2300 4800 2300
$Comp
L power:+3.3V #PWR?
U 1 1 5EFE3088
P 3800 2200
AR Path="/5EFE3088" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEA8E/5EFE3088" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEEB7/5EFE3088" Ref="#PWR064"  Part="1" 
F 0 "#PWR064" H 3800 2050 50  0001 C CNN
F 1 "+3.3V" H 3815 2373 50  0000 C CNN
F 2 "" H 3800 2200 50  0001 C CNN
F 3 "" H 3800 2200 50  0001 C CNN
	1    3800 2200
	1    0    0    -1  
$EndComp
$Comp
L power:+3.3V #PWR?
U 1 1 5EFE3BD2
P 4800 2200
AR Path="/5EFE3BD2" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEA8E/5EFE3BD2" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEEB7/5EFE3BD2" Ref="#PWR067"  Part="1" 
F 0 "#PWR067" H 4800 2050 50  0001 C CNN
F 1 "+3.3V" H 4815 2373 50  0000 C CNN
F 2 "" H 4800 2200 50  0001 C CNN
F 3 "" H 4800 2200 50  0001 C CNN
	1    4800 2200
	1    0    0    -1  
$EndComp
Wire Wire Line
	4800 2200 4800 2300
Connection ~ 4800 2300
Wire Wire Line
	3800 2200 3800 2300
Connection ~ 3800 2300
Wire Wire Line
	4300 4200 4300 4300
Wire Wire Line
	5100 3800 5000 3800
$Comp
L Device:C_Small C?
U 1 1 5EFEB304
P 5700 4100
AR Path="/5EFEB304" Ref="C?"  Part="1" 
AR Path="/5EFBE643/5EFEB304" Ref="C?"  Part="1" 
AR Path="/5EFBEEB7/5EFEB304" Ref="C23"  Part="1" 
F 0 "C23" H 5792 4237 50  0000 L CNN
F 1 "2.2n" H 5792 4146 50  0000 L CNN
F 2 "Capacitor_SMD:C_0603_1608Metric" H 5700 4100 50  0001 C CNN
F 3 "~" H 5700 4100 50  0001 C CNN
F 4 "GRM188R71C222KA01D" H 5800 3800 50  0001 L CNN "PartNumber"
F 5 "X7R" H 5792 4055 50  0000 L CNN "Tolerance"
F 6 "10VDC" H 5792 3964 50  0000 L CNN "Ratio"
	1    5700 4100
	1    0    0    -1  
$EndComp
$Comp
L power:GNDA #PWR?
U 1 1 5EFEB30A
P 5700 4300
AR Path="/5EFEB30A" Ref="#PWR?"  Part="1" 
AR Path="/5EFBE643/5EFEB30A" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEEB7/5EFEB30A" Ref="#PWR071"  Part="1" 
F 0 "#PWR071" H 5700 4050 50  0001 C CNN
F 1 "GNDA" H 5705 4127 50  0000 C CNN
F 2 "" H 5700 4300 50  0001 C CNN
F 3 "" H 5700 4300 50  0001 C CNN
	1    5700 4300
	1    0    0    -1  
$EndComp
Wire Wire Line
	5700 4200 5700 4300
Wire Wire Line
	5700 4000 5700 3700
Wire Wire Line
	5700 3700 5000 3700
$Comp
L power:GNDA #PWR?
U 1 1 5EFEBF08
P 3500 4300
AR Path="/5EFEBF08" Ref="#PWR?"  Part="1" 
AR Path="/5EFBE643/5EFEBF08" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEEB7/5EFEBF08" Ref="#PWR063"  Part="1" 
F 0 "#PWR063" H 3500 4050 50  0001 C CNN
F 1 "GNDA" H 3505 4127 50  0000 C CNN
F 2 "" H 3500 4300 50  0001 C CNN
F 3 "" H 3500 4300 50  0001 C CNN
	1    3500 4300
	1    0    0    -1  
$EndComp
Wire Wire Line
	3500 4300 3500 3800
Wire Wire Line
	3500 3800 3600 3800
Wire Wire Line
	3600 3700 3500 3700
Wire Wire Line
	3500 3700 3500 3800
Connection ~ 3500 3800
$Comp
L Device:R_Small R?
U 1 1 5EFEDFDA
P 3050 4000
AR Path="/5EFEDFDA" Ref="R?"  Part="1" 
AR Path="/5EFBEA8E/5EFEDFDA" Ref="R?"  Part="1" 
AR Path="/5EFBEEB7/5EFEDFDA" Ref="R29"  Part="1" 
F 0 "R29" H 2900 3950 50  0000 C CNN
F 1 "10k" H 2900 4050 50  0000 C CNN
F 2 "Resistor_SMD:R_0603_1608Metric" H 3050 4000 50  0001 C CNN
F 3 "~" H 3050 4000 50  0001 C CNN
F 4 "RC0603FR-0710KL" H 3050 4000 50  0001 C CNN "PartNumber"
	1    3050 4000
	-1   0    0    1   
$EndComp
$Comp
L power:GNDA #PWR?
U 1 1 5EFEF2FD
P 3050 4300
AR Path="/5EFEF2FD" Ref="#PWR?"  Part="1" 
AR Path="/5EFBE643/5EFEF2FD" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEEB7/5EFEF2FD" Ref="#PWR062"  Part="1" 
F 0 "#PWR062" H 3050 4050 50  0001 C CNN
F 1 "GNDA" H 3055 4127 50  0000 C CNN
F 2 "" H 3050 4300 50  0001 C CNN
F 3 "" H 3050 4300 50  0001 C CNN
	1    3050 4300
	1    0    0    -1  
$EndComp
Wire Wire Line
	3050 4300 3050 4100
Wire Wire Line
	3050 3900 3050 3400
Wire Wire Line
	3050 3400 3600 3400
Wire Wire Line
	3600 3200 3050 3200
Wire Wire Line
	3600 3300 3050 3300
Text Label 1750 3000 0    50   ~ 0
i2C_SCL
Text Label 3050 3200 0    50   ~ 0
i2C_SDA
$Comp
L power:GNDA #PWR?
U 1 1 5EFF2032
P 7800 4300
AR Path="/5EFF2032" Ref="#PWR?"  Part="1" 
AR Path="/5EFBE643/5EFF2032" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEEB7/5EFF2032" Ref="#PWR077"  Part="1" 
F 0 "#PWR077" H 7800 4050 50  0001 C CNN
F 1 "GNDA" H 7805 4127 50  0000 C CNN
F 2 "" H 7800 4300 50  0001 C CNN
F 3 "" H 7800 4300 50  0001 C CNN
	1    7800 4300
	1    0    0    -1  
$EndComp
Wire Wire Line
	7800 4200 7800 4300
$Comp
L Device:C_Small C?
U 1 1 5EFF4754
P 7300 2100
AR Path="/5EFF4754" Ref="C?"  Part="1" 
AR Path="/5EFBE643/5EFF4754" Ref="C?"  Part="1" 
AR Path="/5EFBEEB7/5EFF4754" Ref="C24"  Part="1" 
F 0 "C24" H 7392 2237 50  0000 L CNN
F 1 "10n" H 7392 2146 50  0000 L CNN
F 2 "Capacitor_SMD:C_0603_1608Metric" H 7300 2100 50  0001 C CNN
F 3 "~" H 7300 2100 50  0001 C CNN
F 4 "CC0603KRX7R9BB103" H 7400 1800 50  0001 L CNN "PartNumber"
F 5 "X7R" H 7392 2055 50  0000 L CNN "Tolerance"
F 6 "10VDC" H 7392 1964 50  0000 L CNN "Ratio"
	1    7300 2100
	1    0    0    -1  
$EndComp
$Comp
L power:GNDA #PWR?
U 1 1 5EFF475A
P 7300 2300
AR Path="/5EFF475A" Ref="#PWR?"  Part="1" 
AR Path="/5EFBE643/5EFF475A" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEEB7/5EFF475A" Ref="#PWR076"  Part="1" 
F 0 "#PWR076" H 7300 2050 50  0001 C CNN
F 1 "GNDA" H 7305 2127 50  0000 C CNN
F 2 "" H 7300 2300 50  0001 C CNN
F 3 "" H 7300 2300 50  0001 C CNN
	1    7300 2300
	1    0    0    -1  
$EndComp
Wire Wire Line
	7300 2000 7300 1900
Wire Wire Line
	7300 2200 7300 2300
Wire Wire Line
	8300 2200 8300 2300
Wire Wire Line
	8300 2000 8300 1900
$Comp
L power:GNDA #PWR?
U 1 1 5EFF4764
P 8300 2300
AR Path="/5EFF4764" Ref="#PWR?"  Part="1" 
AR Path="/5EFBE643/5EFF4764" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEEB7/5EFF4764" Ref="#PWR079"  Part="1" 
F 0 "#PWR079" H 8300 2050 50  0001 C CNN
F 1 "GNDA" H 8305 2127 50  0000 C CNN
F 2 "" H 8300 2300 50  0001 C CNN
F 3 "" H 8300 2300 50  0001 C CNN
	1    8300 2300
	1    0    0    -1  
$EndComp
$Comp
L Device:C_Small C?
U 1 1 5EFF476D
P 8300 2100
AR Path="/5EFF476D" Ref="C?"  Part="1" 
AR Path="/5EFBE643/5EFF476D" Ref="C?"  Part="1" 
AR Path="/5EFBEEB7/5EFF476D" Ref="C25"  Part="1" 
F 0 "C25" H 8392 2237 50  0000 L CNN
F 1 "100n" H 8392 2146 50  0000 L CNN
F 2 "Capacitor_SMD:C_0603_1608Metric" H 8300 2100 50  0001 C CNN
F 3 "~" H 8300 2100 50  0001 C CNN
F 4 "CC0603KRX7R9BB104" H 8400 1800 50  0001 L CNN "PartNumber"
F 5 "X7R" H 8392 2055 50  0000 L CNN "Tolerance"
F 6 "10VDC" H 8392 1964 50  0000 L CNN "Ratio"
	1    8300 2100
	-1   0    0    -1  
$EndComp
Wire Wire Line
	7700 2400 7700 1900
Wire Wire Line
	7700 1900 7300 1900
Wire Wire Line
	7900 2400 7900 1900
Wire Wire Line
	7900 1900 8300 1900
$Comp
L power:+3.3V #PWR?
U 1 1 5EFF4777
P 7300 1800
AR Path="/5EFF4777" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEA8E/5EFF4777" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEEB7/5EFF4777" Ref="#PWR075"  Part="1" 
F 0 "#PWR075" H 7300 1650 50  0001 C CNN
F 1 "+3.3V" H 7315 1973 50  0000 C CNN
F 2 "" H 7300 1800 50  0001 C CNN
F 3 "" H 7300 1800 50  0001 C CNN
	1    7300 1800
	1    0    0    -1  
$EndComp
$Comp
L power:+3.3V #PWR?
U 1 1 5EFF477D
P 8300 1800
AR Path="/5EFF477D" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEA8E/5EFF477D" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEEB7/5EFF477D" Ref="#PWR078"  Part="1" 
F 0 "#PWR078" H 8300 1650 50  0001 C CNN
F 1 "+3.3V" H 8315 1973 50  0000 C CNN
F 2 "" H 8300 1800 50  0001 C CNN
F 3 "" H 8300 1800 50  0001 C CNN
	1    8300 1800
	1    0    0    -1  
$EndComp
Wire Wire Line
	8300 1800 8300 1900
Connection ~ 8300 1900
Wire Wire Line
	7300 1800 7300 1900
Connection ~ 7300 1900
$Comp
L power:GNDA #PWR?
U 1 1 5EFF58A7
P 8600 4300
AR Path="/5EFF58A7" Ref="#PWR?"  Part="1" 
AR Path="/5EFBE643/5EFF58A7" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEEB7/5EFF58A7" Ref="#PWR080"  Part="1" 
F 0 "#PWR080" H 8600 4050 50  0001 C CNN
F 1 "GNDA" H 8605 4127 50  0000 C CNN
F 2 "" H 8600 4300 50  0001 C CNN
F 3 "" H 8600 4300 50  0001 C CNN
	1    8600 4300
	1    0    0    -1  
$EndComp
Wire Wire Line
	8600 4300 8600 3700
Wire Wire Line
	8600 3700 8500 3700
$Comp
L Device:C_Small C?
U 1 1 5EFF8125
P 8900 4100
AR Path="/5EFF8125" Ref="C?"  Part="1" 
AR Path="/5EFBE643/5EFF8125" Ref="C?"  Part="1" 
AR Path="/5EFBEEB7/5EFF8125" Ref="C26"  Part="1" 
F 0 "C26" H 8992 4237 50  0000 L CNN
F 1 "100n" H 8992 4146 50  0000 L CNN
F 2 "Capacitor_SMD:C_0603_1608Metric" H 8900 4100 50  0001 C CNN
F 3 "~" H 8900 4100 50  0001 C CNN
F 4 "CC0603KRX7R9BB104" H 9000 3800 50  0001 L CNN "PartNumber"
F 5 "X7R" H 8992 4055 50  0000 L CNN "Tolerance"
F 6 "10VDC" H 8992 3964 50  0000 L CNN "Ratio"
	1    8900 4100
	1    0    0    -1  
$EndComp
$Comp
L power:GNDA #PWR?
U 1 1 5EFF812B
P 8900 4300
AR Path="/5EFF812B" Ref="#PWR?"  Part="1" 
AR Path="/5EFBE643/5EFF812B" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEEB7/5EFF812B" Ref="#PWR083"  Part="1" 
F 0 "#PWR083" H 8900 4050 50  0001 C CNN
F 1 "GNDA" H 8905 4127 50  0000 C CNN
F 2 "" H 8900 4300 50  0001 C CNN
F 3 "" H 8900 4300 50  0001 C CNN
	1    8900 4300
	1    0    0    -1  
$EndComp
Wire Wire Line
	8900 4000 8900 3500
Wire Wire Line
	8900 4200 8900 4300
Wire Wire Line
	8900 3500 8500 3500
$Comp
L power:+3.3V #PWR?
U 1 1 5EFFB90A
P 8700 3600
AR Path="/5EFFB90A" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEA8E/5EFFB90A" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEEB7/5EFFB90A" Ref="#PWR082"  Part="1" 
F 0 "#PWR082" H 8700 3450 50  0001 C CNN
F 1 "+3.3V" V 8700 3850 50  0000 C CNN
F 2 "" H 8700 3600 50  0001 C CNN
F 3 "" H 8700 3600 50  0001 C CNN
	1    8700 3600
	0    1    1    0   
$EndComp
Wire Wire Line
	8700 3600 8500 3600
$Comp
L power:+3.3V #PWR?
U 1 1 5EFFD8AB
P 5200 2700
AR Path="/5EFFD8AB" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEA8E/5EFFD8AB" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEEB7/5EFFD8AB" Ref="#PWR070"  Part="1" 
F 0 "#PWR070" H 5200 2550 50  0001 C CNN
F 1 "+3.3V" H 5215 2873 50  0000 C CNN
F 2 "" H 5200 2700 50  0001 C CNN
F 3 "" H 5200 2700 50  0001 C CNN
	1    5200 2700
	1    0    0    -1  
$EndComp
Wire Wire Line
	5000 3200 5200 3200
Wire Wire Line
	5200 3200 5200 3000
Wire Wire Line
	5200 2700 5200 2800
$Comp
L power:+3.3V #PWR?
U 1 1 5F005508
P 8700 2500
AR Path="/5F005508" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEA8E/5F005508" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEEB7/5F005508" Ref="#PWR081"  Part="1" 
F 0 "#PWR081" H 8700 2350 50  0001 C CNN
F 1 "+3.3V" H 8715 2673 50  0000 C CNN
F 2 "" H 8700 2500 50  0001 C CNN
F 3 "" H 8700 2500 50  0001 C CNN
	1    8700 2500
	1    0    0    -1  
$EndComp
Wire Wire Line
	8700 2500 8700 2600
Wire Wire Line
	8700 2800 8700 3000
Wire Wire Line
	8700 3000 8500 3000
$Comp
L power:GNDA #PWR?
U 1 1 5F00705C
P 7000 4300
AR Path="/5F00705C" Ref="#PWR?"  Part="1" 
AR Path="/5EFBE643/5F00705C" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEEB7/5F00705C" Ref="#PWR074"  Part="1" 
F 0 "#PWR074" H 7000 4050 50  0001 C CNN
F 1 "GNDA" H 7005 4127 50  0000 C CNN
F 2 "" H 7000 4300 50  0001 C CNN
F 3 "" H 7000 4300 50  0001 C CNN
	1    7000 4300
	1    0    0    -1  
$EndComp
Wire Wire Line
	7000 4300 7000 3500
Wire Wire Line
	7000 3500 7100 3500
Wire Wire Line
	7100 3000 6300 3000
Wire Wire Line
	7100 3200 6300 3200
Wire Wire Line
	7100 3100 6900 3100
Wire Wire Line
	7100 3300 6650 3300
Text Label 3050 3400 0    50   ~ 0
MPU6050_AD0
Text Notes 3800 5300 0    50   ~ 0
By default  AD0 is LOW\nI2C address:\n7bit: 1101000b (0x68h)\n\nAD0 is HIGH\nI2C address:\n7bit: 1101001b (0x69h)
$Comp
L Device:R_Small R?
U 1 1 5F031FCA
P 6900 2100
AR Path="/5F031FCA" Ref="R?"  Part="1" 
AR Path="/5EFBEA8E/5F031FCA" Ref="R?"  Part="1" 
AR Path="/5EFBEEB7/5F031FCA" Ref="R32"  Part="1" 
F 0 "R32" H 6750 2050 50  0000 C CNN
F 1 "DNP" H 6750 2150 50  0000 C CNN
F 2 "Resistor_SMD:R_0603_1608Metric" H 6900 2100 50  0001 C CNN
F 3 "~" H 6900 2100 50  0001 C CNN
F 4 "DNP" H 6900 2100 50  0001 C CNN "PartNumber"
	1    6900 2100
	-1   0    0    1   
$EndComp
$Comp
L power:+3.3V #PWR?
U 1 1 5F0327BC
P 6900 1800
AR Path="/5F0327BC" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEA8E/5F0327BC" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEEB7/5F0327BC" Ref="#PWR073"  Part="1" 
F 0 "#PWR073" H 6900 1650 50  0001 C CNN
F 1 "+3.3V" H 6915 1973 50  0000 C CNN
F 2 "" H 6900 1800 50  0001 C CNN
F 3 "" H 6900 1800 50  0001 C CNN
	1    6900 1800
	1    0    0    -1  
$EndComp
Wire Wire Line
	6900 1800 6900 2000
Wire Wire Line
	6900 2200 6900 3100
Connection ~ 6900 3100
Wire Wire Line
	6300 3100 6900 3100
$Comp
L Device:R_Small R?
U 1 1 5F036F84
P 6650 2100
AR Path="/5F036F84" Ref="R?"  Part="1" 
AR Path="/5EFBEA8E/5F036F84" Ref="R?"  Part="1" 
AR Path="/5EFBEEB7/5F036F84" Ref="R31"  Part="1" 
F 0 "R31" H 6500 2050 50  0000 C CNN
F 1 "10k" H 6500 2150 50  0000 C CNN
F 2 "Resistor_SMD:R_0603_1608Metric" H 6650 2100 50  0001 C CNN
F 3 "~" H 6650 2100 50  0001 C CNN
F 4 "RC0603FR-0710KL" H 6650 2100 50  0001 C CNN "PartNumber"
	1    6650 2100
	1    0    0    1   
$EndComp
$Comp
L power:+3.3V #PWR?
U 1 1 5F037A34
P 6650 1800
AR Path="/5F037A34" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEA8E/5F037A34" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEEB7/5F037A34" Ref="#PWR072"  Part="1" 
F 0 "#PWR072" H 6650 1650 50  0001 C CNN
F 1 "+3.3V" H 6665 1973 50  0000 C CNN
F 2 "" H 6650 1800 50  0001 C CNN
F 3 "" H 6650 1800 50  0001 C CNN
	1    6650 1800
	1    0    0    -1  
$EndComp
Wire Wire Line
	6650 1800 6650 2000
Wire Wire Line
	6650 2200 6650 3300
Connection ~ 6650 3300
Wire Wire Line
	6650 3300 6300 3300
$Comp
L Device:R_Small R?
U 1 1 5F094CAA
P 5200 2900
AR Path="/5F094CAA" Ref="R?"  Part="1" 
AR Path="/5EFBEA8E/5F094CAA" Ref="R?"  Part="1" 
AR Path="/5EFBEEB7/5F094CAA" Ref="R30"  Part="1" 
F 0 "R30" H 5050 2850 50  0000 C CNN
F 1 "10k" H 5050 2950 50  0000 C CNN
F 2 "Resistor_SMD:R_0603_1608Metric" H 5200 2900 50  0001 C CNN
F 3 "~" H 5200 2900 50  0001 C CNN
F 4 "RC0603FR-0710KL" H 5200 2900 50  0001 C CNN "PartNumber"
	1    5200 2900
	-1   0    0    1   
$EndComp
$Comp
L Device:R_Small R?
U 1 1 5F09672F
P 8700 2700
AR Path="/5F09672F" Ref="R?"  Part="1" 
AR Path="/5EFBEA8E/5F09672F" Ref="R?"  Part="1" 
AR Path="/5EFBEEB7/5F09672F" Ref="R33"  Part="1" 
F 0 "R33" H 8550 2650 50  0000 C CNN
F 1 "10k" H 8550 2750 50  0000 C CNN
F 2 "Resistor_SMD:R_0603_1608Metric" H 8700 2700 50  0001 C CNN
F 3 "~" H 8700 2700 50  0001 C CNN
F 4 "RC0603FR-0710KL" H 8700 2700 50  0001 C CNN "PartNumber"
	1    8700 2700
	-1   0    0    1   
$EndComp
Text HLabel 1500 3000 0    50   BiDi ~ 0
I2C-SCL
Text HLabel 1500 3250 0    50   BiDi ~ 0
i2C-SDA
Text Label 1750 3250 0    50   ~ 0
i2C_SDA
Wire Wire Line
	1500 3000 1750 3000
Wire Wire Line
	1500 3250 1750 3250
Text Label 5400 3200 0    50   ~ 0
nM6050-INT
Wire Wire Line
	5400 3200 5200 3200
Connection ~ 5200 3200
Text Label 8900 3000 0    50   ~ 0
nM9250-INT
Wire Wire Line
	8900 3000 8700 3000
Connection ~ 8700 3000
Text Label 1750 3500 0    50   ~ 0
nM6050-INT
Text HLabel 1500 3500 0    50   Output ~ 0
nM6050-INT
Text Label 1750 3700 0    50   ~ 0
nM9250-INT
Wire Wire Line
	1750 3500 1500 3500
Wire Wire Line
	1750 3700 1500 3700
Text HLabel 1500 3700 0    50   Output ~ 0
nM9250-INT
Text HLabel 1500 4000 0    50   UnSpc ~ 0
MPU6050_AD0
Text Label 1750 4000 0    50   ~ 0
MPU6050_AD0
Wire Wire Line
	1750 4000 1500 4000
Text Label 6300 3200 0    50   ~ 0
i2C_SCL
Text Notes 7300 5300 0    50   ~ 0
\nAD0 is HIGH (BY DEFAULT)\nI2C address:\n7bit: 1101001b (0x69h)\n\nAD0 is LOW\nI2C address:\n7bit: 1101000b (0x68h)
Text HLabel 6300 3300 0    50   Input ~ 0
SPI-nCS
Text HLabel 6300 3100 0    50   Output ~ 0
SPI_MISO
Text Label 6300 3000 0    50   ~ 0
i2C_SDA
Text Label 3050 3300 0    50   ~ 0
i2C_SCL
$EndSCHEMATC
