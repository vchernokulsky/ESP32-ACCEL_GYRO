EESchema Schematic File Version 4
EELAYER 30 0
EELAYER END
$Descr A4 11693 8268
encoding utf-8
Sheet 3 4
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
L power:GNDA #PWR?
U 1 1 5EFD7B2C
P 3000 2450
AR Path="/5EFD7B2C" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEA8E/5EFD7B2C" Ref="#PWR042"  Part="1" 
F 0 "#PWR042" H 3000 2200 50  0001 C CNN
F 1 "GNDA" H 3005 2277 50  0001 C CNN
F 2 "" H 3000 2450 50  0001 C CNN
F 3 "" H 3000 2450 50  0001 C CNN
	1    3000 2450
	1    0    0    -1  
$EndComp
$Comp
L power:GNDA #PWR?
U 1 1 5EFD7B32
P 2500 2450
AR Path="/5EFD7B32" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEA8E/5EFD7B32" Ref="#PWR040"  Part="1" 
F 0 "#PWR040" H 2500 2200 50  0001 C CNN
F 1 "GNDA" H 2505 2277 50  0001 C CNN
F 2 "" H 2500 2450 50  0001 C CNN
F 3 "" H 2500 2450 50  0001 C CNN
	1    2500 2450
	1    0    0    -1  
$EndComp
$Comp
L power:GNDA #PWR?
U 1 1 5EFD7B38
P 4500 1950
AR Path="/5EFD7B38" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEA8E/5EFD7B38" Ref="#PWR047"  Part="1" 
F 0 "#PWR047" H 4500 1700 50  0001 C CNN
F 1 "GNDA" H 4505 1777 50  0001 C CNN
F 2 "" H 4500 1950 50  0001 C CNN
F 3 "" H 4500 1950 50  0001 C CNN
	1    4500 1950
	0    1    1    0   
$EndComp
Wire Wire Line
	3000 2150 3000 2050
Connection ~ 3000 2050
Wire Wire Line
	3000 2050 2500 2050
Wire Wire Line
	2500 2350 2500 2450
Wire Wire Line
	3000 2350 3000 2450
$Comp
L power:+3.3V #PWR?
U 1 1 5EFD7B43
P 2500 1650
AR Path="/5EFD7B43" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEA8E/5EFD7B43" Ref="#PWR039"  Part="1" 
F 0 "#PWR039" H 2500 1500 50  0001 C CNN
F 1 "+3.3V" H 2515 1823 50  0000 C CNN
F 2 "" H 2500 1650 50  0001 C CNN
F 3 "" H 2500 1650 50  0001 C CNN
	1    2500 1650
	1    0    0    -1  
$EndComp
Wire Wire Line
	2500 2050 2500 2150
Wire Wire Line
	2500 1950 2500 2050
Connection ~ 2500 2050
$Comp
L Device:R_Small R?
U 1 1 5EFD7B4C
P 8800 2000
AR Path="/5EFD7B4C" Ref="R?"  Part="1" 
AR Path="/5EFBEA8E/5EFD7B4C" Ref="R25"  Part="1" 
F 0 "R25" V 8750 1850 50  0000 C CNN
F 1 "10k" V 8750 2200 50  0000 C CNN
F 2 "Resistor_SMD:R_0603_1608Metric" H 8800 2000 50  0001 C CNN
F 3 "~" H 8800 2000 50  0001 C CNN
F 4 "RC0603FR-0710KL" H 8800 2000 50  0001 C CNN "PartNumber"
	1    8800 2000
	0    1    1    0   
$EndComp
$Comp
L power:+3.3V #PWR?
U 1 1 5EFD7B52
P 8500 2000
AR Path="/5EFD7B52" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEA8E/5EFD7B52" Ref="#PWR054"  Part="1" 
F 0 "#PWR054" H 8500 1850 50  0001 C CNN
F 1 "+3.3V" H 8515 2173 50  0000 C CNN
F 2 "" H 8500 2000 50  0001 C CNN
F 3 "" H 8500 2000 50  0001 C CNN
	1    8500 2000
	1    0    0    -1  
$EndComp
Wire Wire Line
	9200 2000 9100 2000
Wire Wire Line
	8700 2000 8500 2000
$Comp
L Device:C_Small C?
U 1 1 5EFD7B5D
P 9100 2250
AR Path="/5EFD7B5D" Ref="C?"  Part="1" 
AR Path="/5EFBEA8E/5EFD7B5D" Ref="C19"  Part="1" 
F 0 "C19" H 9192 2387 50  0000 L CNN
F 1 "100n" H 9192 2296 50  0000 L CNN
F 2 "Capacitor_SMD:C_0603_1608Metric" H 9100 2250 50  0001 C CNN
F 3 "~" H 9100 2250 50  0001 C CNN
F 4 "CC0603KRX7R9BB104" H 9200 1950 50  0001 L CNN "PartNumber"
F 5 "X7R" H 9192 2205 50  0000 L CNN "Tolerance"
F 6 "10VDC" H 9192 2114 50  0000 L CNN "Ratio"
	1    9100 2250
	1    0    0    -1  
$EndComp
$Comp
L power:GNDA #PWR?
U 1 1 5EFD7B63
P 9100 2450
AR Path="/5EFD7B63" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEA8E/5EFD7B63" Ref="#PWR061"  Part="1" 
F 0 "#PWR061" H 9100 2200 50  0001 C CNN
F 1 "GNDA" H 9105 2277 50  0001 C CNN
F 2 "" H 9100 2450 50  0001 C CNN
F 3 "" H 9100 2450 50  0001 C CNN
	1    9100 2450
	1    0    0    -1  
$EndComp
Wire Wire Line
	9100 2450 9100 2350
Wire Wire Line
	9100 2150 9100 2000
Connection ~ 9100 2000
Wire Wire Line
	9100 2000 8900 2000
$Comp
L Device:R_Small R?
U 1 1 5EFD7B74
P 2500 1850
AR Path="/5EFD7B74" Ref="R?"  Part="1" 
AR Path="/5EFBEA8E/5EFD7B74" Ref="R12"  Part="1" 
F 0 "R12" H 2350 1800 50  0000 C CNN
F 1 "0E" H 2350 1900 50  0000 C CNN
F 2 "Resistor_SMD:R_0603_1608Metric" H 2500 1850 50  0001 C CNN
F 3 "~" H 2500 1850 50  0001 C CNN
F 4 "RC0603FR-070RL" H 2500 1850 50  0001 C CNN "PartNumber"
	1    2500 1850
	-1   0    0    1   
$EndComp
Text Label 9200 2000 0    50   ~ 0
nRST
$Comp
L Device:C_Small C?
U 1 1 5EFD7B80
P 3000 2250
AR Path="/5EFD7B80" Ref="C?"  Part="1" 
AR Path="/5EFBEA8E/5EFD7B80" Ref="C14"  Part="1" 
F 0 "C14" H 3092 2387 50  0000 L CNN
F 1 "100n" H 3092 2296 50  0000 L CNN
F 2 "Capacitor_SMD:C_0603_1608Metric" H 3000 2250 50  0001 C CNN
F 3 "~" H 3000 2250 50  0001 C CNN
F 4 "CC0603KRX7R9BB104" H 3100 1950 50  0001 L CNN "PartNumber"
F 5 "X7R" H 3092 2205 50  0000 L CNN "Tolerance"
F 6 "10VDC" H 3092 2114 50  0000 L CNN "Ratio"
	1    3000 2250
	1    0    0    -1  
$EndComp
Wire Wire Line
	3000 2050 4700 2050
Wire Wire Line
	2500 1650 2500 1750
$Comp
L BDSM-rescue:ESP32-ESP32 U?
U 1 1 5EFD7B8A
P 4650 1850
AR Path="/5EFD7B8A" Ref="U?"  Part="1" 
AR Path="/5EFBEA8E/5EFD7B8A" Ref="U7"  Part="1" 
F 0 "U7" H 5550 2037 60  0000 C CNN
F 1 "ESP32-WROOM-32D" H 5550 1931 60  0000 C CNN
F 2 "digikey-footprints:ESP32-WROOM-32D" H 5100 1750 60  0001 C CNN
F 3 "" H 5100 1750 60  0000 C CNN
F 4 "ESP32-WROOM-32D" H 4650 1850 50  0001 C CNN "PartNumber"
	1    4650 1850
	1    0    0    -1  
$EndComp
Wire Wire Line
	4500 1950 4700 1950
$Comp
L power:GNDA #PWR?
U 1 1 5EFD7B91
P 5050 4050
AR Path="/5EFD7B91" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEA8E/5EFD7B91" Ref="#PWR049"  Part="1" 
F 0 "#PWR049" H 5050 3800 50  0001 C CNN
F 1 "GNDA" H 5055 3877 50  0001 C CNN
F 2 "" H 5050 4050 50  0001 C CNN
F 3 "" H 5050 4050 50  0001 C CNN
	1    5050 4050
	1    0    0    -1  
$EndComp
$Comp
L power:GNDA #PWR?
U 1 1 5EFD7B97
P 6600 1950
AR Path="/5EFD7B97" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEA8E/5EFD7B97" Ref="#PWR051"  Part="1" 
F 0 "#PWR051" H 6600 1700 50  0001 C CNN
F 1 "GNDA" H 6605 1777 50  0001 C CNN
F 2 "" H 6600 1950 50  0001 C CNN
F 3 "" H 6600 1950 50  0001 C CNN
	1    6600 1950
	0    -1   -1   0   
$EndComp
Wire Wire Line
	5050 3850 5050 4050
Wire Wire Line
	6600 1950 6400 1950
Text Label 4500 2150 2    50   ~ 0
nRST
Wire Wire Line
	4500 2150 4700 2150
Wire Notes Line
	4600 1500 4600 2150
Wire Notes Line
	4600 2150 4750 2150
Text Notes 4450 1450 0    50   ~ 0
CHECK!!!
$Comp
L power:GNDA #PWR?
U 1 1 5EFA629F
P 5550 4050
AR Path="/5EFA629F" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEA8E/5EFA629F" Ref="#PWR050"  Part="1" 
F 0 "#PWR050" H 5550 3800 50  0001 C CNN
F 1 "GNDA" H 5555 3877 50  0001 C CNN
F 2 "" H 5550 4050 50  0001 C CNN
F 3 "" H 5550 4050 50  0001 C CNN
	1    5550 4050
	1    0    0    -1  
$EndComp
Wire Wire Line
	5550 3850 5550 4050
$Comp
L Device:R_Small R?
U 1 1 5EFE0952
P 7500 2300
AR Path="/5EF9D67C/5EFE0952" Ref="R?"  Part="1" 
AR Path="/5EFBEA8E/5EFE0952" Ref="R17"  Part="1" 
F 0 "R17" H 7300 2200 50  0000 L CNN
F 1 "10k" H 7300 2300 50  0000 L CNN
F 2 "Resistor_SMD:R_0603_1608Metric" H 7500 2300 50  0001 C CNN
F 3 "~" H 7500 2300 50  0001 C CNN
F 4 "RC0603FR-0710KL" V 7500 2300 50  0001 C CNN "PartNumber"
F 5 "1%" H 7300 2400 50  0000 L CNN "Tolerance"
	1    7500 2300
	-1   0    0    1   
$EndComp
$Comp
L Device:R_Small R?
U 1 1 5EFE095B
P 8000 2300
AR Path="/5EF9D67C/5EFE095B" Ref="R?"  Part="1" 
AR Path="/5EFBEA8E/5EFE095B" Ref="R18"  Part="1" 
F 0 "R18" H 7800 2200 50  0000 L CNN
F 1 "10k" H 7800 2300 50  0000 L CNN
F 2 "Resistor_SMD:R_0603_1608Metric" H 8000 2300 50  0001 C CNN
F 3 "~" H 8000 2300 50  0001 C CNN
F 4 "RC0603FR-0710KL" V 8000 2300 50  0001 C CNN "PartNumber"
F 5 "1%" H 7800 2400 50  0000 L CNN "Tolerance"
	1    8000 2300
	-1   0    0    1   
$EndComp
Wire Wire Line
	8000 2500 8000 2400
$Comp
L power:+3.3V #PWR?
U 1 1 5EFE0962
P 7500 2100
AR Path="/5EF9D67C/5EFE0962" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEA8E/5EFE0962" Ref="#PWR052"  Part="1" 
F 0 "#PWR052" H 7500 1950 50  0001 C CNN
F 1 "+3.3V" H 7515 2273 50  0000 C CNN
F 2 "" H 7500 2100 50  0001 C CNN
F 3 "" H 7500 2100 50  0001 C CNN
	1    7500 2100
	1    0    0    -1  
$EndComp
$Comp
L power:+3.3V #PWR?
U 1 1 5EFE0968
P 8000 2100
AR Path="/5EF9D67C/5EFE0968" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEA8E/5EFE0968" Ref="#PWR053"  Part="1" 
F 0 "#PWR053" H 8000 1950 50  0001 C CNN
F 1 "+3.3V" H 8015 2273 50  0000 C CNN
F 2 "" H 8000 2100 50  0001 C CNN
F 3 "" H 8000 2100 50  0001 C CNN
	1    8000 2100
	1    0    0    -1  
$EndComp
Wire Wire Line
	7500 2100 7500 2200
Wire Wire Line
	8000 2100 8000 2200
$Comp
L Interface_USB:CH340G U?
U 1 1 5F08D740
P 4500 6050
AR Path="/5F08D740" Ref="U?"  Part="1" 
AR Path="/5EFBE643/5F08D740" Ref="U?"  Part="1" 
AR Path="/5EFBEA8E/5F08D740" Ref="U6"  Part="1" 
F 0 "U6" H 4650 6750 50  0000 C CNN
F 1 "CH340G" H 4750 6650 50  0000 C CNN
F 2 "Package_SO:SOIC-16_3.9x9.9mm_P1.27mm" H 4550 5500 50  0001 L CNN
F 3 "http://www.datasheet5.com/pdf-local-2195953" H 4150 6850 50  0001 C CNN
F 4 "CH340G" H 4500 6050 50  0001 C CNN "PartNumber"
	1    4500 6050
	1    0    0    -1  
$EndComp
$Comp
L Device:R_Small R?
U 1 1 5F08D747
P 5200 5650
AR Path="/5F08D747" Ref="R?"  Part="1" 
AR Path="/5EFBE643/5F08D747" Ref="R?"  Part="1" 
AR Path="/5EFBEA8E/5F08D747" Ref="R13"  Part="1" 
F 0 "R13" V 5150 5500 50  0000 C CNN
F 1 "470" V 5150 5850 50  0000 C CNN
F 2 "Resistor_SMD:R_0603_1608Metric" H 5200 5650 50  0001 C CNN
F 3 "~" H 5200 5650 50  0001 C CNN
F 4 "RC0603FR-07470RL" V 5200 5650 50  0001 C CNN "PartNumber"
	1    5200 5650
	0    1    1    0   
$EndComp
$Comp
L Device:C_Small C?
U 1 1 5F08D750
P 3500 5200
AR Path="/5F08D750" Ref="C?"  Part="1" 
AR Path="/5EFBE643/5F08D750" Ref="C?"  Part="1" 
AR Path="/5EFBEA8E/5F08D750" Ref="C16"  Part="1" 
F 0 "C16" H 3592 5337 50  0000 L CNN
F 1 "100n" H 3592 5246 50  0000 L CNN
F 2 "Capacitor_SMD:C_0603_1608Metric" H 3500 5200 50  0001 C CNN
F 3 "~" H 3500 5200 50  0001 C CNN
F 4 "CC0603KRX7R9BB104" H 3600 4900 50  0001 L CNN "PartNumber"
F 5 "X7R" H 3592 5155 50  0000 L CNN "Tolerance"
F 6 "10VDC" H 3592 5064 50  0000 L CNN "Ratio"
	1    3500 5200
	1    0    0    -1  
$EndComp
$Comp
L Device:R_Small R?
U 1 1 5F08D757
P 5200 5750
AR Path="/5F08D757" Ref="R?"  Part="1" 
AR Path="/5EFBE643/5F08D757" Ref="R?"  Part="1" 
AR Path="/5EFBEA8E/5F08D757" Ref="R14"  Part="1" 
F 0 "R14" V 5150 5600 50  0000 C CNN
F 1 "470" V 5150 5950 50  0000 C CNN
F 2 "Resistor_SMD:R_0603_1608Metric" H 5200 5750 50  0001 C CNN
F 3 "~" H 5200 5750 50  0001 C CNN
F 4 "RC0603FR-07470RL" H 5200 5750 50  0001 C CNN "PartNumber"
	1    5200 5750
	0    1    1    0   
$EndComp
$Comp
L Device:C_Small C?
U 1 1 5F08D760
P 4000 5350
AR Path="/5F08D760" Ref="C?"  Part="1" 
AR Path="/5EFBE643/5F08D760" Ref="C?"  Part="1" 
AR Path="/5EFBEA8E/5F08D760" Ref="C18"  Part="1" 
F 0 "C18" H 4092 5487 50  0000 L CNN
F 1 "100n" H 4092 5396 50  0000 L CNN
F 2 "Capacitor_SMD:C_0603_1608Metric" H 4000 5350 50  0001 C CNN
F 3 "~" H 4000 5350 50  0001 C CNN
F 4 "CC0603KRX7R9BB104" H 4100 5050 50  0001 L CNN "PartNumber"
F 5 "X7R" H 4092 5305 50  0000 L CNN "Tolerance"
F 6 "10VDC" H 4092 5214 50  0000 L CNN "Ratio"
	1    4000 5350
	1    0    0    -1  
$EndComp
$Comp
L power:GNDA #PWR?
U 1 1 5F08D766
P 4000 5550
AR Path="/5F08D766" Ref="#PWR?"  Part="1" 
AR Path="/5EFBE643/5F08D766" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEA8E/5F08D766" Ref="#PWR046"  Part="1" 
F 0 "#PWR046" H 4000 5300 50  0001 C CNN
F 1 "GNDA" H 4005 5377 50  0000 C CNN
F 2 "" H 4000 5550 50  0001 C CNN
F 3 "" H 4000 5550 50  0001 C CNN
	1    4000 5550
	1    0    0    -1  
$EndComp
$Comp
L power:GNDA #PWR?
U 1 1 5F08D76C
P 3500 5400
AR Path="/5F08D76C" Ref="#PWR?"  Part="1" 
AR Path="/5EFBE643/5F08D76C" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEA8E/5F08D76C" Ref="#PWR044"  Part="1" 
F 0 "#PWR044" H 3500 5150 50  0001 C CNN
F 1 "GNDA" H 3505 5227 50  0000 C CNN
F 2 "" H 3500 5400 50  0001 C CNN
F 3 "" H 3500 5400 50  0001 C CNN
	1    3500 5400
	1    0    0    -1  
$EndComp
$Comp
L power:GNDA #PWR?
U 1 1 5F08D772
P 3000 5400
AR Path="/5F08D772" Ref="#PWR?"  Part="1" 
AR Path="/5EFBE643/5F08D772" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEA8E/5F08D772" Ref="#PWR043"  Part="1" 
F 0 "#PWR043" H 3000 5150 50  0001 C CNN
F 1 "GNDA" H 3005 5227 50  0000 C CNN
F 2 "" H 3000 5400 50  0001 C CNN
F 3 "" H 3000 5400 50  0001 C CNN
	1    3000 5400
	1    0    0    -1  
$EndComp
Wire Wire Line
	4500 5450 4500 5150
Wire Wire Line
	3000 5000 3000 5100
Wire Wire Line
	3500 5100 3500 5000
Connection ~ 3500 5000
Wire Wire Line
	3500 5000 3000 5000
Wire Wire Line
	4500 5000 3500 5000
Wire Wire Line
	3000 5300 3000 5400
Wire Wire Line
	3500 5300 3500 5400
Wire Wire Line
	4000 5450 4000 5550
Wire Wire Line
	2800 5950 4100 5950
Wire Wire Line
	2800 6050 4100 6050
$Comp
L Device:Crystal_Small Y?
U 1 1 5F08D784
P 3250 6450
AR Path="/5F08D784" Ref="Y?"  Part="1" 
AR Path="/5EFBE643/5F08D784" Ref="Y?"  Part="1" 
AR Path="/5EFBEA8E/5F08D784" Ref="Y1"  Part="1" 
F 0 "Y1" H 3250 6675 50  0000 C CNN
F 1 "12MHz" H 3250 6584 50  0000 C CNN
F 2 "PatternLibs:ABM3" H 3250 6450 50  0001 C CNN
F 3 "~" H 3250 6450 50  0001 C CNN
F 4 "ABM3-12.000MHZ-D2Y-T" H 3250 6450 50  0001 C CNN "PartNumber"
	1    3250 6450
	1    0    0    -1  
$EndComp
$Comp
L Device:C_Small C?
U 1 1 5F08D78D
P 3550 6650
AR Path="/5F08D78D" Ref="C?"  Part="1" 
AR Path="/5EFBE643/5F08D78D" Ref="C?"  Part="1" 
AR Path="/5EFBEA8E/5F08D78D" Ref="C17"  Part="1" 
F 0 "C17" H 3642 6787 50  0000 L CNN
F 1 "27p" H 3642 6696 50  0000 L CNN
F 2 "Capacitor_SMD:C_0603_1608Metric" H 3550 6650 50  0001 C CNN
F 3 "~" H 3550 6650 50  0001 C CNN
F 4 "CC0603JRNPO9BN270" H 3650 6350 50  0001 L CNN "PartNumber"
F 5 "X7R" H 3642 6605 50  0000 L CNN "Tolerance"
F 6 "NP0" H 3642 6514 50  0000 L CNN "Ratio"
	1    3550 6650
	1    0    0    -1  
$EndComp
$Comp
L Device:C_Small C?
U 1 1 5F08D796
P 2950 6650
AR Path="/5F08D796" Ref="C?"  Part="1" 
AR Path="/5EFBE643/5F08D796" Ref="C?"  Part="1" 
AR Path="/5EFBEA8E/5F08D796" Ref="C13"  Part="1" 
F 0 "C13" H 3042 6787 50  0000 L CNN
F 1 "27p" H 3042 6696 50  0000 L CNN
F 2 "Capacitor_SMD:C_0603_1608Metric" H 2950 6650 50  0001 C CNN
F 3 "~" H 2950 6650 50  0001 C CNN
F 4 "CC0603JRNPO9BN270" H 3050 6350 50  0001 L CNN "PartNumber"
F 5 "X7R" H 3042 6605 50  0000 L CNN "Tolerance"
F 6 "NP0" H 3042 6514 50  0000 L CNN "Ratio"
	1    2950 6650
	1    0    0    -1  
$EndComp
$Comp
L power:GNDA #PWR?
U 1 1 5F08D79C
P 2950 6850
AR Path="/5F08D79C" Ref="#PWR?"  Part="1" 
AR Path="/5EFBE643/5F08D79C" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEA8E/5F08D79C" Ref="#PWR041"  Part="1" 
F 0 "#PWR041" H 2950 6600 50  0001 C CNN
F 1 "GNDA" H 2955 6677 50  0001 C CNN
F 2 "" H 2950 6850 50  0001 C CNN
F 3 "" H 2950 6850 50  0001 C CNN
	1    2950 6850
	1    0    0    -1  
$EndComp
$Comp
L power:GNDA #PWR?
U 1 1 5F08D7A2
P 3550 6850
AR Path="/5F08D7A2" Ref="#PWR?"  Part="1" 
AR Path="/5EFBE643/5F08D7A2" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEA8E/5F08D7A2" Ref="#PWR045"  Part="1" 
F 0 "#PWR045" H 3550 6600 50  0001 C CNN
F 1 "GNDA" H 3555 6677 50  0001 C CNN
F 2 "" H 3550 6850 50  0001 C CNN
F 3 "" H 3550 6850 50  0001 C CNN
	1    3550 6850
	1    0    0    -1  
$EndComp
Wire Wire Line
	4100 6250 2950 6250
Wire Wire Line
	2950 6250 2950 6450
Wire Wire Line
	4100 6450 3550 6450
Wire Wire Line
	3550 6550 3550 6450
Connection ~ 3550 6450
Wire Wire Line
	3550 6450 3350 6450
Wire Wire Line
	3150 6450 2950 6450
Connection ~ 2950 6450
Wire Wire Line
	2950 6450 2950 6550
Wire Wire Line
	2950 6750 2950 6850
Wire Wire Line
	3550 6750 3550 6850
$Comp
L power:GNDA #PWR?
U 1 1 5F08D7B3
P 4500 6800
AR Path="/5F08D7B3" Ref="#PWR?"  Part="1" 
AR Path="/5EFBE643/5F08D7B3" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEA8E/5F08D7B3" Ref="#PWR048"  Part="1" 
F 0 "#PWR048" H 4500 6550 50  0001 C CNN
F 1 "GNDA" H 4505 6627 50  0001 C CNN
F 2 "" H 4500 6800 50  0001 C CNN
F 3 "" H 4500 6800 50  0001 C CNN
	1    4500 6800
	1    0    0    -1  
$EndComp
Wire Wire Line
	4500 6650 4500 6800
Wire Wire Line
	4900 5650 5100 5650
Wire Wire Line
	4900 5750 5100 5750
$Comp
L Transistor_BJT:BC847 Q?
U 1 1 5F08D7BC
P 6200 6050
AR Path="/5F08D7BC" Ref="Q?"  Part="1" 
AR Path="/5EFBE643/5F08D7BC" Ref="Q?"  Part="1" 
AR Path="/5EFBEA8E/5F08D7BC" Ref="Q1"  Part="1" 
F 0 "Q1" H 6391 6096 50  0000 L CNN
F 1 "SS8050" H 6391 6005 50  0000 L CNN
F 2 "Package_TO_SOT_SMD:SOT-23" H 6400 5975 50  0001 L CIN
F 3 "http://www.infineon.com/dgdl/Infineon-BC847SERIES_BC848SERIES_BC849SERIES_BC850SERIES-DS-v01_01-en.pdf?fileId=db3a304314dca389011541d4630a1657" H 6200 6050 50  0001 L CNN
F 4 "SS8050" H 6200 6050 50  0001 C CNN "PartNumber"
	1    6200 6050
	1    0    0    -1  
$EndComp
$Comp
L Device:R_Small R?
U 1 1 5F08D7C3
P 5700 6050
AR Path="/5F08D7C3" Ref="R?"  Part="1" 
AR Path="/5EFBE643/5F08D7C3" Ref="R?"  Part="1" 
AR Path="/5EFBEA8E/5F08D7C3" Ref="R15"  Part="1" 
F 0 "R15" V 5650 5900 50  0000 C CNN
F 1 "10k" V 5650 6250 50  0000 C CNN
F 2 "Resistor_SMD:R_0603_1608Metric" H 5700 6050 50  0001 C CNN
F 3 "~" H 5700 6050 50  0001 C CNN
F 4 "RC0603FR-0710KL" V 5700 6050 50  0001 C CNN "PartNumber"
	1    5700 6050
	0    1    1    0   
$EndComp
$Comp
L Transistor_BJT:BC847 Q?
U 1 1 5F08D7C9
P 6200 6750
AR Path="/5F08D7C9" Ref="Q?"  Part="1" 
AR Path="/5EFBE643/5F08D7C9" Ref="Q?"  Part="1" 
AR Path="/5EFBEA8E/5F08D7C9" Ref="Q2"  Part="1" 
F 0 "Q2" H 6391 6796 50  0000 L CNN
F 1 "SS8050" H 6391 6705 50  0000 L CNN
F 2 "Package_TO_SOT_SMD:SOT-23" H 6400 6675 50  0001 L CIN
F 3 "http://www.infineon.com/dgdl/Infineon-BC847SERIES_BC848SERIES_BC849SERIES_BC850SERIES-DS-v01_01-en.pdf?fileId=db3a304314dca389011541d4630a1657" H 6200 6750 50  0001 L CNN
F 4 "SS8050" H 6200 6750 50  0001 C CNN "PartNumber"
	1    6200 6750
	1    0    0    1   
$EndComp
$Comp
L Device:R_Small R?
U 1 1 5F08D7D0
P 5700 6750
AR Path="/5F08D7D0" Ref="R?"  Part="1" 
AR Path="/5EFBE643/5F08D7D0" Ref="R?"  Part="1" 
AR Path="/5EFBEA8E/5F08D7D0" Ref="R16"  Part="1" 
F 0 "R16" V 5750 6600 50  0000 C CNN
F 1 "10k" V 5750 6950 50  0000 C CNN
F 2 "Resistor_SMD:R_0603_1608Metric" H 5700 6750 50  0001 C CNN
F 3 "~" H 5700 6750 50  0001 C CNN
F 4 "RC0603FR-0710KL" V 5700 6750 50  0001 C CNN "PartNumber"
	1    5700 6750
	0    1    -1   0   
$EndComp
Wire Wire Line
	5800 6050 6000 6050
Wire Wire Line
	5800 6750 6000 6750
Wire Wire Line
	6300 6250 6300 6350
Wire Wire Line
	5500 6750 5600 6750
Wire Wire Line
	6300 6550 6300 6450
Wire Wire Line
	5500 6050 5600 6050
Wire Wire Line
	6050 6350 6300 6450
Wire Wire Line
	6050 6450 6300 6350
Wire Wire Line
	6050 6350 5500 6350
Wire Wire Line
	5500 6350 5500 6050
Wire Wire Line
	6050 6450 5500 6450
Wire Wire Line
	5500 6450 5500 6750
Wire Wire Line
	6300 5850 6300 5750
Wire Wire Line
	6300 6950 6300 7050
Wire Wire Line
	4900 6350 5500 6350
Connection ~ 5500 6350
Wire Wire Line
	4900 6450 5500 6450
Connection ~ 5500 6450
Text Label 6500 5750 0    50   ~ 0
nRST
Text Label 6500 7050 0    50   ~ 0
GPIO0
Wire Wire Line
	5300 5650 5500 5650
Wire Wire Line
	5300 5750 5500 5750
Text Label 5500 5750 0    50   ~ 0
TXD0
Text Label 5500 5650 0    50   ~ 0
RXD0
Text Label 3150 5950 0    50   ~ 0
D+
Text Label 3150 6050 0    50   ~ 0
D-
$Comp
L Device:CP1_Small C?
U 1 1 5F08D7F8
P 3000 5200
AR Path="/5F08D7F8" Ref="C?"  Part="1" 
AR Path="/5EFBE643/5F08D7F8" Ref="C?"  Part="1" 
AR Path="/5EFBEA8E/5F08D7F8" Ref="C15"  Part="1" 
F 0 "C15" H 3091 5337 50  0000 L CNN
F 1 "10uF" H 3091 5246 50  0000 L CNN
F 2 "Capacitor_Tantalum_SMD:CP_EIA-3528-21_Kemet-B" H 3000 5200 50  0001 C CNN
F 3 "~" H 3000 5200 50  0001 C CNN
F 4 "TAJB106K010RNJ" H 3000 5200 50  0001 C CNN "PartNumber"
F 5 "TANTAL" H 3091 5155 50  0000 L CNN "Tolerance"
F 6 "10VDC" H 3091 5064 50  0000 L CNN "Ratio"
	1    3000 5200
	1    0    0    -1  
$EndComp
Wire Wire Line
	4000 5250 4000 5150
Wire Wire Line
	4400 5450 4400 5150
Wire Wire Line
	4400 5150 4000 5150
Text HLabel 1500 5000 0    50   UnSpc ~ 0
VBUS
Text HLabel 1500 3750 0    50   BiDi ~ 0
I2C-SCL
Text HLabel 1500 4000 0    50   BiDi ~ 0
i2C-SDA
Text HLabel 4500 2450 0    50   Input ~ 0
VBUS-nPG
Text HLabel 4500 2550 0    50   Input ~ 0
CHRG-STAT1
Text HLabel 4500 2250 0    50   Input ~ 0
CHRG-STAT2
Wire Wire Line
	1500 5000 2600 5000
Text HLabel 1500 5500 0    50   BiDi ~ 0
USB-D+
Text HLabel 1500 6500 0    50   BiDi ~ 0
USB-D-
Text HLabel 4500 2650 0    50   Input ~ 0
nM6050-INT
Text HLabel 4500 2750 0    50   Input ~ 0
nM9250-INT
$Comp
L Device:R_Small R?
U 1 1 5EFEDC67
P 8400 3800
AR Path="/5EFEDC67" Ref="R?"  Part="1" 
AR Path="/5EFBEA8E/5EFEDC67" Ref="R19"  Part="1" 
F 0 "R19" V 8350 3650 50  0000 C CNN
F 1 "120" V 8350 3950 50  0000 C CNN
F 2 "Resistor_SMD:R_0603_1608Metric" H 8400 3800 50  0001 C CNN
F 3 "~" H 8400 3800 50  0001 C CNN
F 4 "RC0603FR-07120RL" V 8400 3800 50  0001 C CNN "PartNumber"
	1    8400 3800
	0    1    1    0   
$EndComp
Text HLabel 9550 3500 2    50   UnSpc ~ 0
LED-CHRG-RED
Text HLabel 9550 4500 2    50   UnSpc ~ 0
LED-CHRG-GREEN
Text HLabel 9550 5500 2    50   UnSpc ~ 0
LED-LINK
$Comp
L Transistor_FET:BSS138 Q3
U 1 1 5F00BE77
P 8900 3800
F 0 "Q3" H 9104 3846 50  0000 L CNN
F 1 "BSS138" H 9104 3755 50  0000 L CNN
F 2 "Package_TO_SOT_SMD:SOT-23" H 9100 3725 50  0001 L CIN
F 3 "https://www.fairchildsemi.com/datasheets/BS/BSS138.pdf" H 8900 3800 50  0001 L CNN
F 4 "BSS138LT1G" H 8900 3800 50  0001 C CNN "PartNumber"
	1    8900 3800
	1    0    0    -1  
$EndComp
$Comp
L Device:R_Small R?
U 1 1 5F00CD1E
P 9250 3500
AR Path="/5F00CD1E" Ref="R?"  Part="1" 
AR Path="/5EFBEA8E/5F00CD1E" Ref="R26"  Part="1" 
F 0 "R26" V 9200 3350 50  0000 C CNN
F 1 "270" V 9200 3700 50  0000 C CNN
F 2 "Resistor_SMD:R_0603_1608Metric" H 9250 3500 50  0001 C CNN
F 3 "~" H 9250 3500 50  0001 C CNN
F 4 "RC0603FR-07270RL" V 9250 3500 50  0001 C CNN "PartNumber"
	1    9250 3500
	0    1    1    0   
$EndComp
$Comp
L power:GNDA #PWR?
U 1 1 5F010919
P 9000 4100
AR Path="/5F010919" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEA8E/5F010919" Ref="#PWR058"  Part="1" 
F 0 "#PWR058" H 9000 3850 50  0001 C CNN
F 1 "GNDA" H 9005 3927 50  0001 C CNN
F 2 "" H 9000 4100 50  0001 C CNN
F 3 "" H 9000 4100 50  0001 C CNN
	1    9000 4100
	1    0    0    -1  
$EndComp
Wire Wire Line
	9150 3500 9000 3500
Wire Wire Line
	9000 3500 9000 3600
Wire Wire Line
	9000 4000 9000 4100
Wire Wire Line
	9350 3500 9550 3500
$Comp
L Transistor_FET:BSS138 Q4
U 1 1 5F01F867
P 8900 4800
F 0 "Q4" H 9104 4846 50  0000 L CNN
F 1 "BSS138" H 9104 4755 50  0000 L CNN
F 2 "Package_TO_SOT_SMD:SOT-23" H 9100 4725 50  0001 L CIN
F 3 "https://www.fairchildsemi.com/datasheets/BS/BSS138.pdf" H 8900 4800 50  0001 L CNN
F 4 "BSS138LT1G" H 8900 4800 50  0001 C CNN "PartNumber"
	1    8900 4800
	1    0    0    -1  
$EndComp
$Comp
L Device:R_Small R?
U 1 1 5F01F871
P 9250 4500
AR Path="/5F01F871" Ref="R?"  Part="1" 
AR Path="/5EFBEA8E/5F01F871" Ref="R27"  Part="1" 
F 0 "R27" V 9200 4350 50  0000 C CNN
F 1 "300" V 9200 4700 50  0000 C CNN
F 2 "Resistor_SMD:R_0603_1608Metric" H 9250 4500 50  0001 C CNN
F 3 "~" H 9250 4500 50  0001 C CNN
F 4 "RC0603FR-07300RL" V 9250 4500 50  0001 C CNN "PartNumber"
	1    9250 4500
	0    1    1    0   
$EndComp
$Comp
L power:GNDA #PWR?
U 1 1 5F01F87B
P 9000 5100
AR Path="/5F01F87B" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEA8E/5F01F87B" Ref="#PWR059"  Part="1" 
F 0 "#PWR059" H 9000 4850 50  0001 C CNN
F 1 "GNDA" H 9005 4927 50  0001 C CNN
F 2 "" H 9000 5100 50  0001 C CNN
F 3 "" H 9000 5100 50  0001 C CNN
	1    9000 5100
	1    0    0    -1  
$EndComp
Wire Wire Line
	9150 4500 9000 4500
Wire Wire Line
	9000 4500 9000 4600
Wire Wire Line
	9000 5000 9000 5100
Wire Wire Line
	9550 4500 9350 4500
$Comp
L Transistor_FET:BSS138 Q5
U 1 1 5F02ED90
P 8900 5800
F 0 "Q5" H 9104 5846 50  0000 L CNN
F 1 "BSS138" H 9104 5755 50  0000 L CNN
F 2 "Package_TO_SOT_SMD:SOT-23" H 9100 5725 50  0001 L CIN
F 3 "https://www.fairchildsemi.com/datasheets/BS/BSS138.pdf" H 8900 5800 50  0001 L CNN
F 4 "BSS138LT1G" H 8900 5800 50  0001 C CNN "PartNumber"
	1    8900 5800
	1    0    0    -1  
$EndComp
$Comp
L Device:R_Small R?
U 1 1 5F02ED9A
P 9250 5500
AR Path="/5F02ED9A" Ref="R?"  Part="1" 
AR Path="/5EFBEA8E/5F02ED9A" Ref="R28"  Part="1" 
F 0 "R28" V 9200 5350 50  0000 C CNN
F 1 "649" V 9200 5700 50  0000 C CNN
F 2 "Resistor_SMD:R_0603_1608Metric" H 9250 5500 50  0001 C CNN
F 3 "~" H 9250 5500 50  0001 C CNN
F 4 "RC0603FR-07649RL" V 9250 5500 50  0001 C CNN "PartNumber"
	1    9250 5500
	0    1    1    0   
$EndComp
$Comp
L power:GNDA #PWR?
U 1 1 5F02EDA4
P 9000 6100
AR Path="/5F02EDA4" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEA8E/5F02EDA4" Ref="#PWR060"  Part="1" 
F 0 "#PWR060" H 9000 5850 50  0001 C CNN
F 1 "GNDA" H 9005 5927 50  0001 C CNN
F 2 "" H 9000 6100 50  0001 C CNN
F 3 "" H 9000 6100 50  0001 C CNN
	1    9000 6100
	1    0    0    -1  
$EndComp
Wire Wire Line
	9150 5500 9000 5500
Wire Wire Line
	9000 5500 9000 5600
Wire Wire Line
	9000 6000 9000 6100
Wire Wire Line
	9550 5500 9350 5500
Wire Wire Line
	8500 3800 8600 3800
Wire Wire Line
	8500 4800 8600 4800
Wire Wire Line
	8500 5800 8600 5800
Wire Wire Line
	8300 3800 8100 3800
Wire Wire Line
	8300 4800 8100 4800
Wire Wire Line
	8300 5800 8100 5800
$Comp
L Device:R_Small R?
U 1 1 5F0450D2
P 8600 4000
AR Path="/5EF9D67C/5F0450D2" Ref="R?"  Part="1" 
AR Path="/5EFBEA8E/5F0450D2" Ref="R22"  Part="1" 
F 0 "R22" H 8400 3900 50  0000 L CNN
F 1 "10k" H 8400 4000 50  0000 L CNN
F 2 "Resistor_SMD:R_0603_1608Metric" H 8600 4000 50  0001 C CNN
F 3 "~" H 8600 4000 50  0001 C CNN
F 4 "RC0603FR-0710KL" V 8600 4000 50  0001 C CNN "PartNumber"
F 5 "1%" H 8400 4100 50  0000 L CNN "Tolerance"
	1    8600 4000
	-1   0    0    1   
$EndComp
$Comp
L power:GNDA #PWR?
U 1 1 5F045738
P 8600 4100
AR Path="/5F045738" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEA8E/5F045738" Ref="#PWR055"  Part="1" 
F 0 "#PWR055" H 8600 3850 50  0001 C CNN
F 1 "GNDA" H 8605 3927 50  0001 C CNN
F 2 "" H 8600 4100 50  0001 C CNN
F 3 "" H 8600 4100 50  0001 C CNN
	1    8600 4100
	1    0    0    -1  
$EndComp
Wire Wire Line
	8600 3900 8600 3800
Connection ~ 8600 3800
Wire Wire Line
	8600 3800 8700 3800
$Comp
L Device:R_Small R?
U 1 1 5F0486C4
P 8600 5000
AR Path="/5EF9D67C/5F0486C4" Ref="R?"  Part="1" 
AR Path="/5EFBEA8E/5F0486C4" Ref="R23"  Part="1" 
F 0 "R23" H 8400 4900 50  0000 L CNN
F 1 "10k" H 8400 5000 50  0000 L CNN
F 2 "Resistor_SMD:R_0603_1608Metric" H 8600 5000 50  0001 C CNN
F 3 "~" H 8600 5000 50  0001 C CNN
F 4 "RC0603FR-0710KL" V 8600 5000 50  0001 C CNN "PartNumber"
F 5 "1%" H 8400 5100 50  0000 L CNN "Tolerance"
	1    8600 5000
	-1   0    0    1   
$EndComp
$Comp
L power:GNDA #PWR?
U 1 1 5F0486CA
P 8600 5100
AR Path="/5F0486CA" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEA8E/5F0486CA" Ref="#PWR056"  Part="1" 
F 0 "#PWR056" H 8600 4850 50  0001 C CNN
F 1 "GNDA" H 8605 4927 50  0001 C CNN
F 2 "" H 8600 5100 50  0001 C CNN
F 3 "" H 8600 5100 50  0001 C CNN
	1    8600 5100
	1    0    0    -1  
$EndComp
$Comp
L Device:R_Small R?
U 1 1 5F04A88B
P 8600 6000
AR Path="/5EF9D67C/5F04A88B" Ref="R?"  Part="1" 
AR Path="/5EFBEA8E/5F04A88B" Ref="R24"  Part="1" 
F 0 "R24" H 8400 5900 50  0000 L CNN
F 1 "10k" H 8400 6000 50  0000 L CNN
F 2 "Resistor_SMD:R_0603_1608Metric" H 8600 6000 50  0001 C CNN
F 3 "~" H 8600 6000 50  0001 C CNN
F 4 "RC0603FR-0710KL" V 8600 6000 50  0001 C CNN "PartNumber"
F 5 "1%" H 8400 6100 50  0000 L CNN "Tolerance"
	1    8600 6000
	-1   0    0    1   
$EndComp
$Comp
L power:GNDA #PWR?
U 1 1 5F04A891
P 8600 6100
AR Path="/5F04A891" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEA8E/5F04A891" Ref="#PWR057"  Part="1" 
F 0 "#PWR057" H 8600 5850 50  0001 C CNN
F 1 "GNDA" H 8605 5927 50  0001 C CNN
F 2 "" H 8600 6100 50  0001 C CNN
F 3 "" H 8600 6100 50  0001 C CNN
	1    8600 6100
	1    0    0    -1  
$EndComp
Wire Wire Line
	8600 5900 8600 5800
Connection ~ 8600 5800
Wire Wire Line
	8600 5800 8700 5800
Wire Wire Line
	8600 4900 8600 4800
Connection ~ 8600 4800
Wire Wire Line
	8600 4800 8700 4800
$Comp
L Device:R_Small R?
U 1 1 5F050D8D
P 8400 4800
AR Path="/5F050D8D" Ref="R?"  Part="1" 
AR Path="/5EFBEA8E/5F050D8D" Ref="R20"  Part="1" 
F 0 "R20" V 8350 4650 50  0000 C CNN
F 1 "120" V 8350 4950 50  0000 C CNN
F 2 "Resistor_SMD:R_0603_1608Metric" H 8400 4800 50  0001 C CNN
F 3 "~" H 8400 4800 50  0001 C CNN
F 4 "RC0603FR-07120RL" V 8400 4800 50  0001 C CNN "PartNumber"
	1    8400 4800
	0    1    1    0   
$EndComp
$Comp
L Device:R_Small R?
U 1 1 5F05110D
P 8400 5800
AR Path="/5F05110D" Ref="R?"  Part="1" 
AR Path="/5EFBEA8E/5F05110D" Ref="R21"  Part="1" 
F 0 "R21" V 8350 5650 50  0000 C CNN
F 1 "120" V 8350 5950 50  0000 C CNN
F 2 "Resistor_SMD:R_0603_1608Metric" H 8400 5800 50  0001 C CNN
F 3 "~" H 8400 5800 50  0001 C CNN
F 4 "RC0603FR-07120RL" H 8400 5800 50  0001 C CNN "PartNumber"
	1    8400 5800
	0    1    1    0   
$EndComp
$Comp
L BDSM-rescue:USBLC6-2SC6-Power_Protection U5
U 1 1 5EFC47AB
P 2000 6000
F 0 "U5" V 1600 6250 50  0000 L CNN
F 1 "USBLC6-2SC6" V 2400 6200 50  0000 L CNN
F 2 "Package_TO_SOT_SMD:SOT-23-6" H 1250 6400 50  0001 C CNN
F 3 "http://www2.st.com/resource/en/datasheet/CD00050750.pdf" H 2200 6350 50  0001 C CNN
F 4 "USBLC6-2SC6" H 2000 6000 50  0001 C CNN "PartNumber"
	1    2000 6000
	0    1    1    0   
$EndComp
Wire Wire Line
	2500 6000 2600 6000
Wire Wire Line
	2600 6000 2600 5000
$Comp
L power:GNDA #PWR?
U 1 1 5EFD9983
P 1400 6150
AR Path="/5EFD9983" Ref="#PWR?"  Part="1" 
AR Path="/5EFBE643/5EFD9983" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEA8E/5EFD9983" Ref="#PWR038"  Part="1" 
F 0 "#PWR038" H 1400 5900 50  0001 C CNN
F 1 "GNDA" H 1405 5977 50  0001 C CNN
F 2 "" H 1400 6150 50  0001 C CNN
F 3 "" H 1400 6150 50  0001 C CNN
	1    1400 6150
	1    0    0    -1  
$EndComp
Wire Wire Line
	1500 6000 1400 6000
Wire Wire Line
	1400 6000 1400 6150
Wire Wire Line
	2800 5950 2800 5500
Wire Wire Line
	2800 5500 2100 5500
Wire Wire Line
	2800 6050 2800 6500
Wire Wire Line
	2800 6500 2100 6500
Wire Wire Line
	1900 5500 1500 5500
Wire Wire Line
	1900 6500 1500 6500
Text Label 1700 3750 0    50   ~ 0
i2C_SCL
Text Label 1700 4000 0    50   ~ 0
i2C_SDA
Wire Wire Line
	1700 3750 1500 3750
Wire Wire Line
	1700 4000 1500 4000
Text Label 6600 2150 0    50   ~ 0
i2C_SCL
Text Label 6600 2450 0    50   ~ 0
i2C_SDA
Text Label 7500 2500 0    50   ~ 0
i2C_SCL
Text Label 8000 2500 0    50   ~ 0
i2C_SDA
Text Label 6600 3250 0    50   ~ 0
GPIO0
Wire Wire Line
	6600 3250 6400 3250
Text Label 6600 2250 0    50   ~ 0
TXD0
Text Label 6600 2350 0    50   ~ 0
RXD0
Wire Wire Line
	6400 2250 6600 2250
Wire Wire Line
	6400 2350 6600 2350
Wire Wire Line
	6600 2450 6400 2450
Wire Wire Line
	6600 2150 6400 2150
Wire Wire Line
	7500 2400 7500 2500
Wire Wire Line
	4500 3150 4700 3150
Wire Wire Line
	4500 3250 4700 3250
Wire Wire Line
	5150 4050 5150 3850
Wire Wire Line
	4500 3050 4700 3050
Wire Wire Line
	4500 2550 4700 2550
Wire Wire Line
	4500 2450 4700 2450
Wire Wire Line
	4500 2250 4700 2250
Wire Wire Line
	4500 2650 4700 2650
Wire Wire Line
	4500 2750 4700 2750
Wire Wire Line
	5950 4050 5950 3850
Text Label 8100 4800 2    50   ~ 0
LED_G
Text Label 8100 3800 2    50   ~ 0
LED_R
Text Label 8100 5800 2    50   ~ 0
LED_B
Text Label 4500 2950 2    50   ~ 0
LED_G
Text Label 4500 2850 2    50   ~ 0
LED_R
Text Label 4500 3050 2    50   ~ 0
LED_B
Wire Wire Line
	4500 2850 4700 2850
Wire Wire Line
	4500 2950 4700 2950
$Comp
L Device:CP1_Small C?
U 1 1 5F0F933A
P 2500 2250
AR Path="/5F0F933A" Ref="C?"  Part="1" 
AR Path="/5EFBE643/5F0F933A" Ref="C?"  Part="1" 
AR Path="/5EFBEA8E/5F0F933A" Ref="C12"  Part="1" 
F 0 "C12" H 2591 2387 50  0000 L CNN
F 1 "10uF" H 2591 2296 50  0000 L CNN
F 2 "Capacitor_Tantalum_SMD:CP_EIA-3528-21_Kemet-B" H 2500 2250 50  0001 C CNN
F 3 "~" H 2500 2250 50  0001 C CNN
F 4 "TAJB106K010RNJ" H 2500 2250 50  0001 C CNN "PartNumber"
F 5 "TANTAL" H 2591 2205 50  0000 L CNN "Tolerance"
F 6 "10VDC" H 2591 2114 50  0000 L CNN "Ratio"
	1    2500 2250
	1    0    0    -1  
$EndComp
$Comp
L Connector:TestPoint_Alt TP?
U 1 1 5F00FAF2
P 7500 3000
AR Path="/5EF9D67C/5F00FAF2" Ref="TP?"  Part="1" 
AR Path="/5EFBEA8E/5F00FAF2" Ref="TP13"  Part="1" 
F 0 "TP13" H 7558 3118 50  0000 L CNN
F 1 "I2C-SCL" H 7558 3027 50  0000 L CNN
F 2 "TestPoint:TestPoint_Pad_2.0x2.0mm" H 7700 3000 50  0001 C CNN
F 3 "~" H 7700 3000 50  0001 C CNN
F 4 "PCB" H 7500 3000 50  0001 C CNN "PartNumber"
	1    7500 3000
	1    0    0    -1  
$EndComp
$Comp
L Connector:TestPoint_Alt TP?
U 1 1 5F018189
P 8000 3000
AR Path="/5EF9D67C/5F018189" Ref="TP?"  Part="1" 
AR Path="/5EFBEA8E/5F018189" Ref="TP14"  Part="1" 
F 0 "TP14" H 8058 3118 50  0000 L CNN
F 1 "I2C-SDA" H 8058 3027 50  0000 L CNN
F 2 "TestPoint:TestPoint_Pad_2.0x2.0mm" H 8200 3000 50  0001 C CNN
F 3 "~" H 8200 3000 50  0001 C CNN
F 4 "PCB" H 8000 3000 50  0001 C CNN "PartNumber"
	1    8000 3000
	1    0    0    -1  
$EndComp
Text Label 7500 3150 0    50   ~ 0
i2C_SCL
Text Label 8000 3150 0    50   ~ 0
i2C_SDA
Wire Wire Line
	7500 3000 7500 3150
Wire Wire Line
	8000 3000 8000 3150
$Comp
L Connector:TestPoint_Alt TP?
U 1 1 5F0214BB
P 6700 5750
AR Path="/5EF9D67C/5F0214BB" Ref="TP?"  Part="1" 
AR Path="/5EFBEA8E/5F0214BB" Ref="TP11"  Part="1" 
F 0 "TP11" H 6758 5868 50  0000 L CNN
F 1 "nRESET" H 6758 5777 50  0000 L CNN
F 2 "TestPoint:TestPoint_Pad_1.0x1.0mm" H 6900 5750 50  0001 C CNN
F 3 "~" H 6900 5750 50  0001 C CNN
F 4 "PCB" H 6700 5750 50  0001 C CNN "PartNumber"
	1    6700 5750
	1    0    0    -1  
$EndComp
$Comp
L Connector:TestPoint_Alt TP?
U 1 1 5F022571
P 6750 7050
AR Path="/5EF9D67C/5F022571" Ref="TP?"  Part="1" 
AR Path="/5EFBEA8E/5F022571" Ref="TP12"  Part="1" 
F 0 "TP12" H 6808 7168 50  0000 L CNN
F 1 "BOOT" H 6808 7077 50  0000 L CNN
F 2 "TestPoint:TestPoint_Pad_1.0x1.0mm" H 6950 7050 50  0001 C CNN
F 3 "~" H 6950 7050 50  0001 C CNN
F 4 "PCB" H 6750 7050 50  0001 C CNN "PartNumber"
	1    6750 7050
	1    0    0    -1  
$EndComp
Wire Wire Line
	6300 7050 6750 7050
Wire Wire Line
	6300 5750 6700 5750
Text HLabel 6600 2950 2    50   UnSpc ~ 0
MPU6050_AD0
Wire Wire Line
	6600 2950 6400 2950
Wire Wire Line
	3300 3800 3400 3800
Wire Wire Line
	3600 3800 3700 3800
$Comp
L Device:Jumper_NC_Small JP1
U 1 1 5F026D54
P 3500 3800
F 0 "JP1" H 3500 3900 50  0000 C CNN
F 1 "MPU-9250 - SDA/MOSI" H 3500 3700 50  0000 C CNN
F 2 "Jumper:SolderJumper-2_P1.3mm_Open_TrianglePad1.0x1.5mm" H 3500 3800 50  0001 C CNN
F 3 "~" H 3500 3800 50  0001 C CNN
F 4 "PCB" H 3500 3800 50  0001 C CNN "PartNumber"
	1    3500 3800
	1    0    0    -1  
$EndComp
$Comp
L Device:Jumper_NC_Small JP2
U 1 1 5F028050
P 3500 4100
F 0 "JP2" H 3500 4200 50  0000 C CNN
F 1 "MPU-9250 - SCL/SCK" H 3500 4000 50  0000 C CNN
F 2 "Jumper:SolderJumper-2_P1.3mm_Open_TrianglePad1.0x1.5mm" H 3500 4100 50  0001 C CNN
F 3 "~" H 3500 4100 50  0001 C CNN
F 4 "PCB" H 3500 4100 50  0001 C CNN "PartNumber"
	1    3500 4100
	1    0    0    -1  
$EndComp
Text Label 3300 3800 2    50   ~ 0
i2C_SDA
Text Label 3700 4100 0    50   ~ 0
SPI_CLK
Wire Wire Line
	3700 4100 3600 4100
Text Label 3300 4100 2    50   ~ 0
i2C_SCL
Wire Wire Line
	3300 4100 3400 4100
Text HLabel 5950 4050 3    50   Output ~ 0
SPI-nCS
Text Label 4500 3150 2    50   ~ 0
SPI_CLK
Text HLabel 4500 3250 0    50   Input ~ 0
SPI-MISO
Text Label 3700 3800 0    50   ~ 0
SPI_MOSI
Text Label 5150 4050 3    50   ~ 0
SPI_MOSI
Text Notes 5200 7100 0    50   ~ 0
Replace Q1,Q2 to SS8050.
$Comp
L power:+3.3V #PWR?
U 1 1 5F689D2C
P 3000 4800
AR Path="/5EF9D67C/5F689D2C" Ref="#PWR?"  Part="1" 
AR Path="/5EFBEA8E/5F689D2C" Ref="#PWR0102"  Part="1" 
F 0 "#PWR0102" H 3000 4650 50  0001 C CNN
F 1 "+3.3V" H 3015 4973 50  0000 C CNN
F 2 "" H 3000 4800 50  0001 C CNN
F 3 "" H 3000 4800 50  0001 C CNN
	1    3000 4800
	1    0    0    -1  
$EndComp
Wire Wire Line
	3000 4800 3000 5000
Connection ~ 3000 5000
Wire Wire Line
	4400 5150 4500 5150
Connection ~ 4400 5150
Connection ~ 4500 5150
Wire Wire Line
	4500 5150 4500 5000
$EndSCHEMATC
