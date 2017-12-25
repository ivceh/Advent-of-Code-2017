import re

def signum(x):
    return (x > 0) - (x < 0)

def tuple_to_compare_coordinate (p, v, a):
    if a!=0:
        return (abs(a), v*signum(a), p*signum(a))
    elif v!=0:
        return (0, abs(v), p*signum(v))
    else:
        return (0, 0, abs(p))

class particle:
    def __init__ (self,  px, py, pz,  vx, vy, vz,  ax, ay, az):
        self.px = px
        self.py = py
        self.pz = pz
        self.vx = vx
        self.vy = vy
        self.vz = vz
        self.ax = ax
        self.ay = ay
        self.az = az

    def tuple_to_compare (self):
        (tx1, tx2, tx3) = tuple_to_compare_coordinate (self.px, self.vx, self.ax)
        (ty1, ty2, ty3) = tuple_to_compare_coordinate (self.py, self.vy, self.ay)
        (tz1, tz2, tz3) = tuple_to_compare_coordinate (self.pz, self.vz, self.az)
        return (tx1+ty1+tz1, tx2+ty2+tz2, tx3+ty3+tz3)

maxttc = (0,0,0)
maxindex = -1
with open('input.txt') as f:
    for i, line in enumerate(f):
        matchObj = re.match( r'p=<(-?\d+),(-?\d+),(-?\d+)>, ' +
                             r'v=<(-?\d+),(-?\d+),(-?\d+)>, ' +
                             r'a=<(-?\d+),(-?\d+),(-?\d+)>', line, re.M|re.I)
        li = [int(matchObj.group(i)) for i in range(1,10)]
        current_particle = particle(li[0], li[1], li[2],  li[3], li[4], li[5],  li[6], li[7], li[8])
        currttc = current_particle.tuple_to_compare()

        if maxindex == -1 or currttc < maxttc:
            maxttc = currttc
            maxindex = i

print(maxindex)
