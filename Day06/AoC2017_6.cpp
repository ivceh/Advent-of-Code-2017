#include <iostream>
#include <vector>
#include <map>
using namespace std;

int main()
{
    int i, n, max, maxi, x;
    vector <int> V({11, 11, 13, 7, 0, 15, 5, 5, 4, 4, 1, 1, 7, 1, 15, 11});

    n = 0;
    map<vector<int>, int> M; // it will map all arrays already found to number of steps before its first appearance
    while(M.count(V) == 0)
    {
        M[V] = n;
        max = V[0];
        maxi = 0;
        for (i=0; i<16; ++i)
            if (V[i] > max)
            {
                max = V[i];
                maxi = i;
            }
        x = max;
        V[maxi] = 0;
        for (i=0; i<16; ++i)
        {
            V[(maxi+i+1) % 16] += x/16 + (i<(x%16) ? 1 : 0);
        }
        ++n;
    }

    cout << n << " " << n-M[V]; // solutions for A and B

    return 0;
}
