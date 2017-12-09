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
            StreamWriter sw = new StreamWriter("../../output.txt"); // here I will write state of each register in each step

            string line;
            string[] spl;
            string a, b;
            int inc, cond;
            int max = 0;

            Dictionary<string, int> dict = new Dictionary<string, int>();

            line = sr.ReadLine();
            while (line != null)
            {
                if (line.Trim() != "")
                {
                    foreach (var kvp in dict)
                        sw.Write(kvp.Key + ":" + kvp.Value + " ");
                    sw.WriteLine();

                    spl = line.Split();
                    a = spl[0];
                    b = spl[4];
                    inc = Convert.ToInt32(spl[2]);
                    cond = Convert.ToInt32(spl[6]);

                    if (!dict.ContainsKey(a))
                        dict.Add(a, 0);
                    if (!dict.ContainsKey(b))
                        dict.Add(b, 0);

                    if (spl[1] == "dec")
                        inc = -inc;

                    switch (spl[5])
                    {
                        case "<":
                            if (dict[b] < cond)
                                dict[a] += inc;
                            break;
                        case ">":
                            if (dict[b] > cond)
                                dict[a] += inc;
                            break;
                        case "<=":
                            if (dict[b] <= cond)
                                dict[a] += inc;
                            break;
                        case ">=":
                            if (dict[b] >= cond)
                                dict[a] += inc;
                            break;
                        case "==":
                            if (dict[b] == cond)
                                dict[a] += inc;
                            break;
                        case "!=":
                            if (dict[b] != cond)
                                dict[a] += inc;
                            break;
                    }
                    if (dict[a] > max)
                        max = dict[a];
                }
                line = sr.ReadLine();
            }

            foreach (var kvp in dict)
                sw.Write(kvp.Key + ":" + kvp.Value + " ");
            sw.WriteLine();
            sr.Close();
            sw.Close();
            Console.WriteLine("First part: " + dict.Values.Max());
            Console.WriteLine("Second part " + max);
        }
    }
}
