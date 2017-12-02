fo = open("input.txt","r")
s = fo.read()

A = [[int(i) for i in l.split()] for l in s.splitlines()]

s = 0
for l in A:
    for i,x in enumerate(l):
        for j,y in enumerate(l):
            if i!=j and x%y == 0:
                s+= x//y

print(s)
