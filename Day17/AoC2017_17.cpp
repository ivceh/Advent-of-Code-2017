#include <iostream>
#include <fstream>
using namespace std;

// Complexity = O(steps)
int number_after_n(int step, int steps, int n)
{
    int pos = 0, i, pos_n, pos_after_n, after_n;

    if (n > 0)
    {
        // Find position after n steps
        for (i=1; i<=n; ++i)
            pos = (pos + step) % i + 1;

        pos_after_n = pos_n = pos;

        /* Go back until you find the number that was after n when
               n came to the list */
        for (i=n; i>=1; --i)
        {
            pos = (pos - 1 - step) % i;
            if (pos < 0)
                pos += i;

            if (pos < pos_after_n)
                --pos_after_n;
            else if (pos == pos_after_n)
            {
                after_n = i - 1;
                break;
            }
        }
        if (i==0)
            after_n = 0;
    }
    else
        pos_n = after_n = 0;

    pos = pos_n;

    // Go forward and find the number after n after all steps
    for (i=n+1; i<=steps; ++i)
    {
        pos = (pos + step) % i + 1;
        if (pos <= pos_n)
            ++pos_n;
        else if (pos == pos_n + 1)
            after_n = i;
    }

    return after_n;
}

int main()
{
	int n;
	cin >> n;

	cout << "First part: " << number_after_n(n, 2017, 2017) << endl;
	cout << "Second part: " << number_after_n(n, 50000000, 0) << endl;

    return 0;
}
