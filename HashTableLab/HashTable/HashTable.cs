using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HashTableLab.HashTable
{
    public class HashTable<TKey> : IHashTable<TKey>
    {
        private const int MinCapacity = 8;
        private const double ResizingFactor = 0.75;
        private LinkedList<TKey>[] _items;

        public int Count { get; private set; }
        public int Capacity { get; private set; }

        public HashTable()
        {
            Count = 0;
            Capacity = MinCapacity;
            _items = new LinkedList<TKey>[Capacity];
        }

        public bool Add(TKey key)
        {
            if (Count + 1 > (int) (Capacity * ResizingFactor))
            {
                Resize(Capacity * 2);
            }
            return AddKey(_items, key);
        }

        public bool Remove(TKey key)
        {
            var hashCode = GetHashCode(key);
            if (_items[hashCode] == null)
            {
                return false;
            }
            if (_items[hashCode].Contains(key))
            {
                _items[hashCode].Remove(key);
                if (_items[hashCode].Count == 0)
                {
                    _items[hashCode] = null;
                }
                if (--Count < Capacity / 2 && Capacity / 2 >= MinCapacity)
                {
                    Resize(Capacity / 2);
                }
                return true;
            }
            return false;
        }

        public bool Contains(TKey key)
        {
            var hashCode = GetHashCode(key);
            return _items[hashCode] != null && _items[hashCode].Contains(key);
        }

        public IEnumerator<TKey> GetEnumerator()
        {
            return _items.Where(linkedList => linkedList != null)
                         .SelectMany(linkedList => linkedList)
                         .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private int GetHashCode(TKey key)
        {
            return key.GetHashCode() % Capacity;
        }

        private bool AddKey(LinkedList<TKey>[] keys, TKey key)
        {
            var hashCode = GetHashCode(key);
            if (keys[hashCode] == null)
            {
                keys[hashCode] = new LinkedList<TKey>();
            }
            if (!keys[hashCode].Contains(key))
            {
                keys[hashCode].AddLast(key);
                Count++;
                return true;
            }
            return false;
        }

        private void Resize(int size)
        {
            Count = 0;
            Capacity = size;
            var newItems = new LinkedList<TKey>[Capacity];
            foreach (var linkedList in _items.Where(linkedList => linkedList != null))
            {
                foreach (var key in linkedList)
                {
                    AddKey(newItems, key);
                }
            }
            _items = newItems;
        }
    }
}
