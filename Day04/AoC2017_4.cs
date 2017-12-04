using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AoC2017_4
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("../../input.txt");
            HashSet<string> set = new HashSet<string>();
            bool valid;
            int cnt = 0, cnt2 = 0;
            string s;
            bool part2 = true;

            string line = sr.ReadLine();
            while(!(line is null))
            {
                if (line != "")
                {
                    set.Clear();
                    valid = true;

                    foreach (string word in line.Split())
                    {
                        if (part2)
                        {
                            char[] A = word.ToArray();
                            Array.Sort(A);
                            s = new string(A);
                        }
                        else
                            s = word;

                        if (set.Contains(s))
                        {
                            valid = false;
                            break;
                        }
                        else
                            set.Add(s);
                    }

                    if (valid)
                        ++cnt; // counter for valid rows
                    else
                        ++cnt2; // counter fro non-valid rows
                }

                line = sr.ReadLine();
            }

            Console.WriteLine(cnt.ToString()+" "+cnt2.ToString());
        }
    }
}
