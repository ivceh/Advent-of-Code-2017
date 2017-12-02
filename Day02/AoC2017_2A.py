fo = open("input.txt","r")
s = fo.read()

A = [list(map(int,l.split("\t"))) for l in filter(None,list(s.split("\n")))]

print(sum([max(l) - min(l) for l in A]))
