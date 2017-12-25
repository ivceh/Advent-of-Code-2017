#include <iostream>
#include <fstream>
#include <set>
using namespace std;

#define pii pair<int,int>
#define x first
#define y second

enum direction
{
    up = 0,
    left = 1,
    down = 2,
    right = 3
};

direction left_direction (direction d)
{
    return (direction)((d+1)%4);
}

direction right_direction (direction d)
{
    return (direction)((d+3)%4);
}

pii direction_vector[4] = {pii(0,-1),pii(-1,0),pii(0,1),pii(1,0)};

int main()
{
	ifstream in;
	string line, str;
	int i=0, j, cnt=0;

	set <pii> infected;

	direction currdir = up;
	int x=0, y=0;

	in.open("input.txt");
	while (getline(in, line))
    {
        if (!line.empty())
        {
            for (j=0; j<(int)line.size(); ++j)
                if (line[j] == '#')
                    infected.insert(pii(j - (line.size()-1)/2, i - (line.size()-1)/2));
            ++i;
        }
    }
    in.close();

    for (i=0; i<10000; ++i)
    {
        if (infected.count(pii(x,y))>0)
        {
            currdir = right_direction(currdir);
            infected.erase(pii(x,y));
        }
        else
        {
            currdir = left_direction(currdir);
            infected.insert(pii(x,y));
            ++cnt;
        }
        pii dirvec = direction_vector[currdir];
        x += dirvec.x;
        y += dirvec.y;
    }

    cout << cnt;

	return 0;
}
