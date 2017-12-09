#include <iostream>
#include <fstream>
using namespace std;

int main()
{
	ifstream in;
	string line, str;
	int depth, sum, cnt=0;
	bool garbage, cancelling;

	in.open("input.txt");
	(getline(in, line));
    depth = sum = 0;
    garbage = cancelling = false;
    for (char c: line)
    {
        if (garbage)
        {
            if (cancelling)
                cancelling = false;
            else if (c=='>')
                garbage = false;
            else if (c=='!')
                cancelling = true;
            else
                ++cnt;
        }
        else
        {
            if (c=='{')
            {
                ++depth;
                sum += depth;
            }
            else if (c=='}')
                --depth;
            else if (c=='<')
                garbage = true;
        }
    }
    in.close();

    cout << sum << " " << cnt;

	return 0;
}
