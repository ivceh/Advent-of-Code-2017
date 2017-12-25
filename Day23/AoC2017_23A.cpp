#include <iostream>
#include <string>
#include <fstream>
#include <sstream>
#include <vector>
using namespace std;

int registers[8] = {0,0,0,0,0,0,0,0};

int get_value (string arg)
{
    if(arg[0]>='a' && arg[0]<='h')
        return registers[arg[0] - 'a'];
    else
    {
        istringstream istr(arg);
        int n;
        istr >> n;
        return n;
    }
}

void set_value (string arg, int value)
{
    registers[arg[0] - 'a'] = value;
}

int main()
{
	ifstream in;
	string line, inst, arg1, arg2;
	vector <string> instructions;
	int pcnt=0, mulcnt=0;

	in.open("input.txt");
	while (getline(in, line))
        if (!line.empty())
            instructions.push_back(line);
    in.close();

    while(pcnt>=0 && pcnt<instructions.size())
    {
        istringstream istr(instructions[pcnt]);
        istr >> inst >> arg1 >> arg2;

        if (inst=="set")
            set_value(arg1, get_value(arg2));
        else if (inst=="sub")
            set_value(arg1, get_value(arg1)-get_value(arg2));
        else if (inst=="mul")
        {
            ++mulcnt;
            set_value(arg1, get_value(arg1)*get_value(arg2));
        }
        else if (inst=="jnz")
        {
            if(get_value(arg1)!=0)
                pcnt += get_value(arg2) - 1;
        }
        else
            cerr << "Unknown instruction!";
        ++pcnt;
    }

    cout << mulcnt;

	return 0;
}
