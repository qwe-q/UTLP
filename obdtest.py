import obd
import time


def lps_new(rpm, tps):
    """Based on research paper: https://www.hindawi.com/journals/jr/2020/9450178/#EEq2
    Estimates MPG based on RPM and the Throttle Position Sensor (TPS).
    Returns liters * 10^-4 / second"""
    p00 = 2.685
    p10 = -0.1246
    p01 = 1.243
    return p00 * rpm**2 + p10 * rpm + p01 * rpm * tps


def mpg_classic(maf, speed):
    """Classic MPG. Returns MPG."""
    alpha = 7.718
    return (speed * alpha) / maf


conn = obd.OBD(timeout=30, fast=False, portstr="COM13")

start = time.time()
results = []
f = open("C:/Users/qwe/Desktop/obdtest.csv", 'w')
print("Time, MPG_Classic, LPS_New, MAF, SPD, RPM, TPS", file=f)


while True:
    try:

        data = []
        for cmd in (obd.commands.MAF, obd.commands.SPEED, obd.commands.RPM, obd.commands.THROTTLE_POS):
            data.append(conn.query(cmd))
        mpg_c = mpg_classic(data[0], data[1])
        lps_n = lps_new(data[2], data[3])
        now = time.time() - start
        res = f"{now}, {mpg_c}, {lps_n}, {data[0]}, {data[1]}, {data[2]}, {data[3]}"
        print(res, file=f)
    except Exception as e:
        print(f"Exiting with Exception {e}", file=f)
        f.close()
        break
