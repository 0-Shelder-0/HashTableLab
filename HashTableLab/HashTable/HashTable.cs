using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HashTableLab.HashTable
{
    public class HashTable<TKey> : IHashTable<TKey>
    {
        private int _capacity;
        private const int MinCapacity = 8;
        private const double ResizingFactor = 0.75;
        private LinkedList<TKey>[] _items;

        public int Count { get; private set; }

        public HashTable()
        {
            Count = 0;
            _capacity = MinCapacity;
            _items = new LinkedList<TKey>[_capacity];
        }

        public bool Add(TKey key)
        {
            if (Count + 1 > (int) (_capacity * ResizingFactor))
            {
                Resize();
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
                if (--Count < _capacity / 2 && _capacity / 2 >= MinCapacity)
                {
                    Rehash();
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
            return key.GetHashCode() % _capacity;
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

        private void Resize()
        {
            Count = 0;
            _capacity *= 2;
            var newItems = new LinkedList<TKey>[_capacity];
            foreach (var linkedList in _items.Where(linkedList => linkedList != null))
            {
                foreach (var key in linkedList)
                {
                    AddKey(newItems, key);
                }
            }
            _items = newItems;
        }

        private void Rehash()
        {
            _capacity /= 2;
            Count = 0;
            var newItems = new LinkedList<TKey>[_capacity];
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
