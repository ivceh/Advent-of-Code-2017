#include <iostream>
#include <fstream>
#include <vector>
using namespace std;

#define pii pair<int,int>
#define x first
#define y second

enum direction{right = 0, up = 1, left = 2, down = 3};
pii dir_vectors[4] = {pii(1,0), pii(0,-1), pii(-1,0), pii(0,1)};

struct grid
{
    vector<string> A;

    char operator() (int x, int y)
    {
        if (y<0 || y>=(int)A.size())
            return ' ';
        else if (x<0 || x>=(int)A[y].size())
            return ' ';
        else
            return A[y][x];
    }
};

pair<direction,direction> adjacent_directions (direction dir)
{
    return pair<direction,direction> (static_cast<direction>((dir+3)%4),static_cast<direction>((dir+1)%4));
}

int main()
{
	ifstream in;
	string line, str="";
	grid gr;
	int x,y, step=0;
	direction dir;
	bool over;
	char c;

	in.open("input.txt");
	while (getline(in, line))
    {
        if (!line.empty())
        {
            gr.A.push_back(line);
        }
    }
    in.close();
    x = gr.A[0].find('|');
    y = 0;
    dir = down;

    do
    {
        //cout << x <<","<<y<<endl;
        //cin >> c;

        over = false;

        c = gr(x,y);
        if (c == '-' || c == '|' || (c >= 'A' && c <= 'Z'))
        {
            //cout << "*";

            if (c >= 'A' && c <= 'Z')
                str.push_back(c);

            x += dir_vectors[dir].x;
            y += dir_vectors[dir].y;
        }
        else if (c == '+')
        {
            //cout << "+";
            direction newdir = adjacent_directions(dir).first;
            if (gr(x+dir_vectors[newdir].x,y+dir_vectors[newdir].y) != ' ')
            {
                //cout << "1 " << dir_vectors[newdir].x << " " << dir_vectors[newdir].y << endl;
                dir = newdir;
                x += dir_vectors[newdir].x;
                y += dir_vectors[newdir].y;
            }
            else
            {
                //cout << "2 ";
                newdir = adjacent_directions(dir).second;
                if (gr(x+dir_vectors[newdir].x,y+dir_vectors[newdir].y) != ' ')
                {
                    dir = newdir;
                    x += dir_vectors[newdir].x;
                    y += dir_vectors[newdir].y;
                }
                else
                    over = true;
            }
        }
        else if (c == ' ')
            over = true;
        else
        {
            cerr << "Unknown character!";
            return 1;
        }
        ++step;
    }
    while(!over);

    cout << "First part: " << str << endl;
    cout << "Second part: " << step-1;

	return 0;
}
