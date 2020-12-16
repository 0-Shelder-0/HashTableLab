using System;
using System.Collections.Generic;
using System.Reflection;
using HashTableLab.Generator;
using HashTableLab.HashTable;

namespace HashTableLab
{
    static class Program
    {
        private static void Main()
        {
            const int number = 1000000;
            var hashTable = new HashTable<User>();
            var generator = new Generator.Generator();
            foreach (var user in generator.Generate(number))
            {
                hashTable.Add(user);
            }
            PrintValues(hashTable);
        }

        private static void PrintValues(IHashTable<User> hashTable)
        {
            var items = typeof(HashTable<User>).GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance)
                                               .GetValue(hashTable) as LinkedList<User>[];
            var min = items.Length;
            var max = 0;
            foreach (var linkedList in items)
            {
                if (linkedList == null)
                {
                    min = 0;
                    continue;
                }
                if (linkedList.Count > max)
                {
                    max = linkedList.Count;
                }
                if (linkedList.Count < min)
                {
                    min = linkedList.Count;
                }
            }
            Console.WriteLine($"Min: {min}");
            Console.WriteLine($"Max: {max}");
            Console.WriteLine($"Average: {hashTable.Capacity / (double) hashTable.Count}");
        }
    }
}
