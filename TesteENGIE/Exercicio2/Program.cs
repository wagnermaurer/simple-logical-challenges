using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercicio2
{
    /*     
        SAMPLE INPUT
            3 1269 1160 1663
            3 1 1 1
            5 226 223 225 224 227 229 228 226 225 227
            5 216 210 204 212 220 214 222 208 216 210
            5 -1 0 -1 -2 1 0 -1 1 0 -1
            5 79950 79936 79942 79962 79954 79972 79960 79968 79924 79932

        SAMPLE OUTPUT
            383 777 886
            Impossible
            111 112 113 114 115
            101 103 107 109 113
            -1 -1 0 0 1
            39953 39971 39979 39983 39989
     */

    public class Program
    {
        public static bool DoAssign(int index, List<int> values, List<bool> taken, List<int> results)
        {
            if (index == values.Count())
                return true;

            int pivotLoc = ((results.Count() - 1) * results.Count()) / 2;

            for (int i = 2; i < values.Count(); i++)
            {
                if (index == 2)
                {
                    double Avd = (values[0] + values[1] - values[i]) / 2.0;
                    if (Avd - (int)Avd > 0.00000001)
                        continue;

                    results.Add((int)Avd);
                    results.Add(values[0] - (int)Avd);
                    results.Add(values[1] - (int)Avd);
                    taken[i] = true;
                }
                else if (index == pivotLoc)
                {
                    if (taken[i] == true)
                        continue;

                    results.Add(values[i] - results[0]);
                    taken[i] = true;
                }
                else
                {
                    pivotLoc = ((results.Count() - 2) * (results.Count() - 1)) / 2;
                    if (taken[i] == true)
                        continue;
                    if (values[i] - results.Last() != results[index % pivotLoc])
                        continue;

                    taken[i] = true;
                }

                if (DoAssign(index + 1, values, taken, results))
                    return true;

                taken[i] = false;

                if (index == 2)
                    results.Clear();

                if (index == pivotLoc)
                    results.Remove(results.Last());
            }

            return false;
        }

        public static void Main()
        {
            try
            {
                Console.WriteLine("INPUT:");
                var input = Console.ReadLine();

                var data = new List<int>();
                var results = new List<int>();
                var taken = new List<bool>();

                var n = Convert.ToInt32(input[0]);
                var limit = (n * (n - 1)) / 2;
                var values = input.Substring(2)
                                  .Split(" ", StringSplitOptions.None);

                for (int i = 0; i < values.Count(); i++)
                    data.Add(int.Parse(values[i]));
                for (int i = 0; i <= limit; i++)
                    taken.Add(false);

                data.Sort();
                DoAssign(2, data, taken, results);
                results.Sort();

                if (results.Count() == 0)
                    Console.WriteLine("Impossible.");
                else
                {
                    for (int i = 0; i < results.Count(); i++)
                        Console.Write(results[i] + " ");
                    Console.WriteLine("\n");
                }
            }
            catch (Exception ex)
            {
                //generic error handling
                Console.WriteLine($"An error has occurred: \n {ex}");
                Console.ReadLine();
            }

        }
    }
}
