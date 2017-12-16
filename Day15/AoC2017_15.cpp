#include <iostream>
using namespace std;

int main()
{
	long long a0, b0, a, b;
	int cnt = 0, i;

	cout << "Generator A starts with: ";
	cin >> a0;
	cout << "Generator B starts with: ";
	cin >> b0;

	a = a0;
	b = b0;
	for(i=0; i<40000000; ++i)
	{
	    a = a*16807 % 2147483647;
	    b = b*48271 % 2147483647;
	    if (a%65536 == b%65536)
            ++cnt;
	}

    cout << "First part: " << cnt << endl;

    cnt = 0;
    a = a0;
    b = b0;
    for(i=0; i<5000000; ++i)
    {
        do
            a = a*16807 % 2147483647;
        while (a%4 != 0);
        do
            b = b*48271 % 2147483647;
        while (b%8 != 0);
	    if (a%65536 == b%65536)
            ++cnt;
    }

    cout << "Second part: " << cnt;

	return 0;
}
