using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AoC2017_8
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("../../input.txt");

            string line;
            string[] spl;

            int severity = 0;
            SortedDictionary<int, SortedSet<int>> unallowed_remainders = new SortedDictionary<int, SortedSet<int>>();

            line = sr.ReadLine();
            while (line != null)
            {
                if (line.Trim() != "")
                {
                    spl = line.Split(new string[] { " ", ":" }, StringSplitOptions.RemoveEmptyEntries);
                    int depth = Convert.ToInt32(spl[0]), range = Convert.ToInt32(spl[1]);
                    int mod = 2 * (range - 1);

                    if (depth % mod == 0)
                        severity += depth * range;

                    if (!unallowed_remainders.ContainsKey(mod))
                        unallowed_remainders.Add(mod, new SortedSet<int>());
                    unallowed_remainders[mod].Add((mod - depth % mod) % mod);
                }
                line = sr.ReadLine();
            }
            sr.Close();

            Console.WriteLine("First part: " + severity);

            StreamWriter sw = new StreamWriter("../../output.txt");
            foreach (var s in unallowed_remainders)
            {
                sw.Write(s.Key + ": ");
                foreach (int j in s.Value)
                    sw.Write(j + " ");
                sw.WriteLine();
            }
            sw.Close();

            // searching for the smallest valid delay
            int i = -1;
            bool valid;
            do
            {
                ++i;
                valid = true;
                foreach (var s in unallowed_remainders)
                    if (s.Value.Contains(i % s.Key))
                    {
                        valid = false;
                        break;
                    }
            }
            while (!valid);

            Console.WriteLine("Second part: " + i);
        }
    }
}
