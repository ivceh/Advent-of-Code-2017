using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

// I used permutations of set {0, 1, 2, ..., 15} stored in one dimensional array of 16 ints.
// Letters 'a', 'b', 'c', ..., 'p' are mapped to ints as 'a'->0, 'b'->1, 'c'->3, ..., 'p'->15
namespace AoC2017_project_cs
{
    class Program
    {
        static char Letter(int n)
        {
            return (char)('a' + n);
        }

        static int IndexOfLetter(char c)
        {
            return c - 'a';
        }

        static int[] IdentityPerm ()
        {
            int[] P = new int[16];

            for (int i = 0; i < 16; ++i)
                P[i] = i;

            return P;
        }

        static int[] Rotate (int[] A, int shift)
        {
            int[] B = new int[16];

            for (int i = 0; i < 16; ++i)
                B[(i + shift) % 16] = A[i];

            return B;
        }

        static void Swap(ref int a, ref int b)
        {
            int t = a;
            a = b;
            b = t;
        }

        static void SolveA()
        {
            int i;

            int[] letters = IdentityPerm();

            StreamReader sr = new StreamReader("../../input.txt");
            string inp = sr.ReadToEnd();
            sr.Close();

            string[] spl = inp.Split(',');

            foreach (string s in spl)
            {
                string[] arguments = s.Substring(1).Split('/');

                switch (s[0])
                {
                    case 's':
                        letters = Rotate(letters, Convert.ToInt32(arguments[0]));
                        break;
                    case 'x':
                        int posa = Convert.ToInt32(arguments[0]);
                        int posb = Convert.ToInt32(arguments[1]);
                        Swap(ref letters[posa], ref letters[posb]);
                        break;
                    case 'p':
                        for (i=0; i<16; ++i)
                        {
                            if (Letter(letters[i]) == arguments[0][0])
                                letters[i] = IndexOfLetter(arguments[1][0]);
                            else if (Letter(letters[i]) == arguments[1][0])
                                letters[i] = IndexOfLetter(arguments[0][0]);
                        }
                        break;
                }
            }

            foreach (var letter in letters)
                Console.Write(Letter(letter));
            Console.WriteLine();
        }

        // returns permutations composition
        static int[] Compose (int[] perm1, int[] perm2)
        {
            int[] P = new int[16];

            for (int i = 0; i < 16; ++i)
                P[i] = perm1[perm2[i]];

            return P;
        }

        static void SolveB()
        {
            int i;

            int[] pos_perm = IdentityPerm();
            int[] letters_perm = IdentityPerm();

            StreamReader sr = new StreamReader("../../input.txt");
            string inp = sr.ReadToEnd();
            sr.Close();

            string[] spl = inp.Split(',');

            foreach (string s in spl)
            {
                string[] arguments = s.Substring(1).Split('/');

                switch (s[0])
                {
                    case 's':
                        pos_perm = Rotate(pos_perm, Convert.ToInt32(arguments[0]));
                        break;
                    case 'x':
                        int posa = Convert.ToInt32(arguments[0]);
                        int posb = Convert.ToInt32(arguments[1]);
                        Swap(ref pos_perm[posa], ref pos_perm[posb]);
                        break;
                    case 'p':
                        for (i = 0; i < 16; ++i)
                        {
                            if (Letter(letters_perm[i]) == arguments[0][0])
                                letters_perm[i] = IndexOfLetter(arguments[1][0]);
                            else if (Letter(letters_perm[i]) == arguments[1][0])
                                letters_perm[i] = IndexOfLetter(arguments[0][0]);
                        }
                        break;
                }
            }

            List<bool> bits = new List<bool>();

            int n = 1000000000;
            while(n>0)
            {
                bits.Add(n % 2 == 1);
                n /= 2;
            }

            int[] total_pos_perm = IdentityPerm();
            int[] total_letter_perm = IdentityPerm();

            // calculating composition  pos_perm^1000000000 ∘ letter_perm^1000000000  in ~log(1000000000) steps
            for (i = bits.Count -1; i>=0; --i)
            {
                total_pos_perm = Compose(total_pos_perm, total_pos_perm);
                total_letter_perm = Compose(total_letter_perm, total_letter_perm);

                if (bits[i])
                {
                    total_pos_perm = Compose(total_pos_perm, pos_perm);
                    total_letter_perm = Compose(total_letter_perm, letters_perm);
                }
            }

            foreach (var letter in Compose(total_letter_perm, total_pos_perm))
                Console.Write(Letter(letter));
            Console.WriteLine();
        }

        static void Main()
        {
            Console.Write("First part: ");
            SolveA();
            Console.Write("Second part: ");
            SolveB();
        }
    }
}