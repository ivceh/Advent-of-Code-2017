fo = open("input.txt","r")
s = fo.read()

A = [[int(i) for i in l.split()] for l in s.splitlines()]

print(sum(max(l) - min(l) for l in A))
