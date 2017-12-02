fo = open("input.txt","r")
s = fo.read()

A = [list(map(int,l.split("\t"))) for l in filter(None,list(s.split("\n")))]

s = 0
for l in A:
    for i,x in enumerate(l):
        for j,y in enumerate(l):
            if i!=j and x%y == 0:
                s+= x//y

print(s)
