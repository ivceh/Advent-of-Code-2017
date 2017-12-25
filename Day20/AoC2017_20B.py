import re
import math

def is_integer(x):
    return math.isclose(x,int(x))

def linear_eq (k, l):
    if k==0:
        if l==0:
            return True
        else:
            return []
    else:
        return [-l/k]

def square_eq (a, b, c):
    if a == 0:
        return linear_eq(b, c)
    else:
        D = b*b-4*a*c
        if D<0:
            return []
        elif D==0:
            return [-b/(2*a)]
        else:
            sqD = math.sqrt(D)
            return [(-b-sqD)/(2*a), (-b+sqD)/(2*a)]

def nonnegative_integer_quadratic_equation_solution (a, b, c):
    sol = square_eq(a, b, c)
    if isinstance (sol, bool):
        return sol
    else:
        assert(isinstance(sol,list))
        return [int(el) for el in sol if is_integer(el) and int(el) >= 0]
            
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

def first_collision (part1, part2):
    solx = nonnegative_integer_quadratic_equation_solution(part1.ax - part2.ax, 2*(part1.vx - part2.vx) + (part1.ax - part2.ax), 2*(part1.px - part2.px))
    soly = nonnegative_integer_quadratic_equation_solution(part1.ay - part2.ay, 2*(part1.vy - part2.vy) + (part1.ay - part2.ay), 2*(part1.py - part2.py))
    solz = nonnegative_integer_quadratic_equation_solution(part1.az - part2.az, 2*(part1.vz - part2.vz) + (part1.az - part2.az), 2*(part1.pz - part2.pz))

    currsol = True
    for sol in (solx, soly, solz):
        if isinstance (currsol, bool):
            if currsol:
                currsol = sol
            else:
                return False
        else:
            assert (isinstance(currsol, list))
            if isinstance (sol, bool):
                if sol:
                    continue
                else:
                    return False
            else:
                assert (isinstance(sol, list))
                currsol = [el for el in currsol if el in sol]
                
    if isinstance(currsol, bool):
        if currsol:
            return 0
        else:
            return False
    else:
        assert isinstance(currsol, list)
        if currsol:
            return min(currsol)
        else:
            return False

#main program
maxttc = (0,0,0)
maxindex = -1
particles = []
with open('input.txt') as f:
    for line in f:
        matchObj = re.match( r'p=<(-?\d+),(\s*)(-?\d+),(\s*)(-?\d+)>,(\s*)' +
                             r'v=<(-?\d+),(\s*)(-?\d+),(\s*)(-?\d+)>,(\s*)' +
                             r'a=<(-?\d+),(\s*)(-?\d+),(\s*)(-?\d+)>(\s*)', line, re.M|re.I)
        li = [int(matchObj.group(2*i-1)) for i in range(1,10)]
        particles.append(particle(li[0], li[1], li[2],  li[3], li[4], li[5],  li[6], li[7], li[8]))

D_collisions = dict()
for i,part1 in enumerate(particles):
    for j,part2 in enumerate(particles):
        if i<j:
            sol = first_collision(part1, part2)
            if not isinstance(sol, bool):
                if sol in D_collisions:
                    D_collisions[sol].add((i,j))
                else:
                    D_collisions[sol] = {(i,j)}

destroyed = set()

for key in sorted(D_collisions.keys()):
    destroyed_in_this_step = set()

    for collision in D_collisions[key]:
        if not ((collision[0] in destroyed) or (collision[1] in destroyed)):
            destroyed_in_this_step.add(collision[0])
            destroyed_in_this_step.add(collision[1])
    for el in destroyed_in_this_step:
        destroyed.add(el)

print(len(particles) - len(destroyed))
