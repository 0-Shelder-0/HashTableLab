using System.Collections.Generic;

namespace HashTableLab.HashTable
{
    public interface IHashTable<TKey> : IEnumerable<TKey>
    {
        public int Count { get; }

        bool Add(TKey key);
        bool Remove(TKey key);
        bool Contains(TKey key);
    }
}
