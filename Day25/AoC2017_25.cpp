#include <iostream>
using namespace std;
#include <cstdio>
#include <map>
#include <fstream>

char state;
map<pair<char,bool>, tuple<bool, int, char> > transformations; //get<0>(), get<1>()...
map<int,int> tape;
int currpos = 0;

int main()
{
	ifstream in;
	string line, str;
	int steps, ones=0, t;

	char current_reading_state;
	bool current_reading_value;
	bool new_value;
	int step;
	char new_state;

	in.open("input.txt");

	getline(in, line);
	sscanf(line.c_str(), "Begin in state %c", &state);
	getline(in, line);
	sscanf(line.c_str(), "Perform a diagnostic checksum after %d steps.", &steps);

	// reading input
	while (getline(in, line))
    {
        if (!line.empty())
        {
            sscanf(line.c_str(), "In state %c:", &current_reading_state);
            getline(in, line);
            while (!line.empty())
            {
                sscanf(line.c_str(), " If the current value is %d:", &t);
                if (t==0)
                    current_reading_value = false;
                else if (t==1)
                    current_reading_value = true;
                else
                    cerr << "Value must be 0 or 1!"<<endl;
                getline(in, line);
                sscanf(line.c_str(), " - Write the value %d.", &t);
                if (t==0)
                    new_value = false;
                else if (t==1)
                    new_value = true;
                else
                    cerr << "Value must be 0 or 1!"<<endl;
                getline(in, line);
                if (line=="    - Move one slot to the left.")
                    step = -1;
                else
                    step = 1;
                getline(in, line);
                sscanf(line.c_str(), " - Continue with state %c.", &new_state);

                transformations[pair<char,bool>(current_reading_state, current_reading_value)] =
                    make_tuple(new_value, step, new_state);

                getline(in, line);
            }
        }
    }
    in.close();

    for (int i=0; i<steps; ++i)
    {
        if (i%1000000==0)
            cout << (double)i/steps*100 << "%" << endl;
        auto key = pair<char,bool> (state, tape[currpos]);
        auto value = transformations[key];
        tape[currpos] = get<0>(value);
        currpos += get<1>(value);
        state = get<2>(value);
    }

    for (auto kvp : tape)
        if (kvp.second == true)
            ++ones;

    cout << "First part: " << ones << endl;
    cout << "Second part:" << endl;
    cout << "" << endl;
    cout << "           ~                  ~" << endl;
    cout << "     *                   *                *       *" << endl;
    cout << "                  *               *" << endl;
    cout << "  ~       *                *         ~    *          " << endl;
    cout << "              *       ~        *              *   ~" << endl;
    cout << "                  )         (         )              *" << endl;
    cout << "    *    ~     ) (_)   (   (_)   )   (_) (  *" << endl;
    cout << "           *  (_) # ) (_) ) # ( (_) ( # (_)       *" << endl;
    cout << "              _#.-#(_)-#-(_)#(_)-#-(_)#-.#_    " << endl;
    cout << "  *         .' #  # #  #  # # #  #  # #  # `.   ~     *" << endl;
    cout << "           :   #    #  #  #   #  #  #    #   :   " << endl;
    cout << "    ~      :.       #     #   #     #       .:      *" << endl;
    cout << "        *  | `-.__                     __.-' | *" << endl;
    cout << "           |      `````\"\"\"\"\"\"\"\"\"\"\"`````      |         *" << endl;
    cout << "     *     |         |_||\\ |~)|~)\\ /         |    " << endl;
    cout << "           |         | ||~\\|~ |~  |          |       ~" << endl;
    cout << "   ~   *   |                                 | * " << endl;
    cout << "           |      /~|_||~)|[~~|~|V||\\ [~     |         *" << endl;
    cout << "   *    _.-|      \\_| ||~\\|_] | | ||~\\_|     |-._  " << endl;
    cout << "      .'   '.      ~            ~           .'   `.  *" << endl;
    cout << "      :      `-.__                     __.-'      :" << endl;
    cout << "       `.         `````\"\"\"\"\"\"\"\"\"\"\"`````         .'" << endl;
    cout << "         `-.._                             _..-'" << endl;
    cout << "              `````\"\"\"\"-----------\"\"\"\"`````" << endl;
    cout << "" << endl;
    cout << "Copyed from http://www.chris.com/ascii/index.php?art=events/birthday" << endl;

	return 0;
}
