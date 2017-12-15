# d calculates the distance from (0,0) to (x,y) (number of steps between them)
def d(x,y):
    if x>0 and y>0 or x<0 and y<0:
        return max(abs(x), abs(y))
    else:
        return abs(x) + abs(y)



fo = open("input.txt","r")
s = fo.readline()

A = s.strip().split(",")

maxdist = 0

x = y = 0
for move in A:
    if move=="n":
        y += 1
    elif move=="s":
        y -= 1
    elif move=="nw":
        x -= 1
    elif move=="ne":
        x += 1
        y += 1
    elif move=="sw":
        x -= 1
        y -= 1
    elif move=="se":
        x += 1
    else:
        print(move)

    dist = d(x,y)

    if dist>maxdist:
        maxdist = dist

print("first part: "+str(d(x,y)))
print("Second part: "+str(maxdist))
