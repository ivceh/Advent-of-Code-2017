/*
I translated given assembly code into this C++ code (with significant effort):

a = 1;
b = 108100;
c = 125100; //==(b + 17000)
do
{
    f = 1;
    d = 2;
    do
    {
        e = 2;
        do
        {
            if (d*e==b)
                f = 0;
            ++e;
        }
        while (e!=b)
        ++d
    }
    while (d!=b);
    if (f==0)
        ++h;
    if (b==c)
        exit(0);
    b += 17;
}
while(true);
cout << h;

This program actually counts composite numbers from the set {10800 + 17*i for i in {0,1,2,..., 1000}}.
It is very inefficient, so I wrote more efficient program to do the same thing.
*/

#include <iostream>
using namespace std;

bool isprime(int n)
{
    for (int i=2; i*i<=n; ++i)
        if (n%i==0)
            return false;
    return true;
}

int main()
{
    int cnt=0;

    for (int n=108100; n<=125100; n+=17)
        if (!isprime(n))
            ++cnt;

    cout << cnt;

    return 0;
}
