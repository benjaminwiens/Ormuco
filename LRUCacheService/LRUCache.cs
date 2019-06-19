using System;
using System.Timers;
using System.Collections.Generic;

namespace LRUCacheService
{
    public class LRUCache<T>
    {
        private LinkedList<T> _cache = new LinkedList<T>();
        private Dictionary<int, LinkedListNode<T>> _cacheReference = new Dictionary<int, LinkedListNode<T>>();
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
            if(!_cacheReference.ContainsKey(key))
            {
                return null;
            }
            LinkedListNode<T> found = _cache.Find(_cacheReference[key].Value);
            //move node in linked list to front
            _cache.Remove(found);
            _cache.AddFirst(found);

            _cacheTimer.Stop();
            _cacheTimer.Start();
            return found;
        }

        public void Add(LinkedListNode<T> data)
        {
            if(_cache.Count == _cacheCapacity) //cache is full
            {
                int lastKey = _cache.Last.GetHashCode();
                _cache.RemoveLast(); //LRU node
                _cacheReference.Remove(lastKey);
            }
            _cache.AddFirst(data);
            int firstKey = _cache.First.GetHashCode();
            _cacheReference.Add(firstKey,data);
            _cacheTimer.Stop();
            _cacheTimer.Start();
        }

        private void ClearCache(object source, ElapsedEventArgs e)
        {
            _cache.Clear();
            _cacheReference.Clear();
        }
    }
}