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
                sum += s[i] - '0';
        }
        else
        {
            if (s[i] == s[i+1])
                sum += s[i] - '0';
        }
    }

    cout << sum;

    return 0;
}
