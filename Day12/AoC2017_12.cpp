#include <iostream>
#include <fstream>
#include <string>
#include <sstream>
#include <map>
#include <set>
using namespace std;

map <int, set<int>> M; // M[vertex] = set of its neighbors
set <int> S; // set of verticed visited by DFS algorithm

// DFS algorithm
void search_connected_area(int n)
{
    if(S.count(n)>0)
        return;
    S.insert(n);
    for (int i:M[n])
        search_connected_area(i);
}

int main()
{
	ifstream in;
	string line, str;

	int a,b;

	int groups=0;

	// reading input
	in.open("input.txt");
	while (getline(in, line))
    {
        if (!line.empty())
        {
            istringstream istr(line);
            istr >> a;
            M[a] = set<int>();
            istr.ignore(4);
            while (istr >> b)
            {
                M[a].insert(b);
                istr.ignore(1);
            }
        }
    }
    in.close();

    // solving part 1
    search_connected_area(0);
    ++groups;
    cout << "First part: " << S.size() << endl;

    // solving part 2
    for (auto kvp : M)
    {
        if (S.count(kvp.first)==0)
        {
            search_connected_area(kvp.first);
            ++groups;
        }
    }

    cout << "Second part: " << groups << endl;

	return 0;
}
