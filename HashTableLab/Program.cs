using System;
using System.Collections.Generic;
using System.Linq;
using HashTableLab.HashTable;

namespace HashTableLab
{
    static class Program
    {
        private static void Main()
        {
            var h = new HashTable<int>();
            var hash = new HashSet<int>();
            var r = new Random();
            var tests = new List<int>();
            const int number = 1000;

            for (var i = 0; i < number; i++)
            {
                tests.Add(r.Next(0, number));
            }

            foreach (var test in tests)
            {
                h.Add(test);
                hash.Add(test);
            }

            foreach (var i in hash)
            {
                if (!h.Contains(i))
                {
                    Console.WriteLine(false);
                }
            }
        }
    }
}
