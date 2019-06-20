using System;
using System.Timers;
using System.Collections.Generic;

namespace LRUCacheService
{
    public class LRUCache<T>
    {
        private LinkedList<T> _cacheLRU = new LinkedList<T>();
        private Dictionary<int, T> _cacheLookup = new Dictionary<int, T>();
        private Dictionary<T, int> _cacheReverseLookup = new Dictionary<T, int>(); //assumes values are unique
        private Timer _cacheTimer;
        private static int _cacheCapacity;
        private static int _cacheExpirationInMilliseconds;

        public LRUCache(int capacity, int cacheExpirationInSeconds)
        {
            _cacheCapacity = capacity;
            _cacheExpirationInMilliseconds = cacheExpirationInSeconds * 1000;
            _cacheTimer = new Timer(_cacheExpirationInMilliseconds);
            _cacheTimer.Elapsed += new ElapsedEventHandler(ClearCache);
            _cacheTimer.Start();
        }

        public LinkedListNode<T> Get(int key)
        {
            if(!_cacheLookup.ContainsKey(key))
            {
                return null;
            }

            //update LRU
            LinkedListNode<T> found = _cacheLRU.Find(_cacheLookup[key]);
            _cacheLRU.Remove(found);
            _cacheLRU.AddFirst(found);

            _cacheTimer.Stop();
            _cacheTimer.Start();
            return found;
        }

        public void Add(int key, T value)
        {
            if(!_cacheLookup.ContainsKey(key) || !_cacheReverseLookup.ContainsKey(value)) //already in cache
            {
                //update LRU
                LinkedListNode<T> found = _cacheLRU.Find(_cacheLookup[key]);
                _cacheLRU.Remove(found);
                _cacheLRU.AddFirst(found);

                _cacheTimer.Stop();
                _cacheTimer.Start();
                return;
            }

            if(_cacheLRU.Count == _cacheCapacity) //cache is full 
            {
                T LRUValue = _cacheLRU.Last.Value;
                int LRUKey = _cacheReverseLookup[LRUValue]; 

                //remove LRU nodes from all three data structures
                _cacheLRU.RemoveLast(); //LRU node
                _cacheLookup.Remove(LRUKey);
                _cacheReverseLookup.Remove(LRUValue);
            }

            //add new node to all three data structures
            _cacheLRU.AddFirst(value);
            _cacheLookup.Add(key,value);
            _cacheReverseLookup.Add(value,key);

            _cacheTimer.Stop();
            _cacheTimer.Start();
        }

        private void ClearCache(object source, ElapsedEventArgs e)
        {
            _cacheLRU.Clear();
            _cacheLookup.Clear();
        }
    }
}