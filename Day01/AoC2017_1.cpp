#include <iostream>
#include <string>
using namespace std;

int main()
{
    string s;
    int sum = 0;

    cin >> s;

    for (int i=0; i<s.size(); ++i)
    {
        if (i == s.size() - 1) // if it is the last index in s
        {
            if (s[i] == s[0])
                sum += s[i] - '0'; // get number value for digit from ASCII
        }
        else
        {
            if (s[i] == s[i+1])
                sum += s[i] - '0'; // get number value for digit from ASCII
        }
    }

    cout << sum;

    return 0;
}
