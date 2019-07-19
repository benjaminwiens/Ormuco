using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LRUCacheService
{
    public class LRUCache<T>
    {
        public class Node
        {
            public int _key {get;set;}
            public T _data {get;set;}
            public DateTime _timestamp {get;set;}
            public Node(T data, int key, DateTime now)
            {
                _key = key;
                _data = data;
                _timestamp = now;
            }
        }
        private LinkedList<Node> _cacheLRU = new LinkedList<Node>(); //to keep order of nodes
        private Dictionary<int, Node> _cacheLookup = new Dictionary<int, Node>(); //to find nodes quickly
        private static int _cacheCapacity;
        private static int _cacheExpirationInSeconds;
        //private static List<LRUCache<T>> _cacheInstances = new List<LRUCache<T>>(); //contains list of all instances? But need for diff processes

        public LRUCache(int capacity, int cacheExpirationInSeconds)
        {
            _cacheCapacity = capacity;
            _cacheExpirationInSeconds = cacheExpirationInSeconds;
            //_cacheInstances.Add(this);
        }

        public T Get(int key)
        {
            if(!_cacheLookup.ContainsKey(key))
            {
                return default(T);
            }

            //update LRU
            LinkedListNode<Node> found = _cacheLRU.Find(_cacheLookup[key]);
            DateTime lastUsed = found.Value._timestamp; 
            _cacheLRU.Remove(found);

            //check if expired
            DateTime now = DateTime.Now;
            if((now - lastUsed).TotalSeconds > _cacheExpirationInSeconds)
            {
                _cacheLookup.Remove(key); //remove it from the dictionary
                //Task.Run(() => BroadcastRemoveNodeToOtherCaches(key)); //start new thread to broadcast removal of this node to other caches?
                return default(T);
            }
            //Task.Run(() => BroadcastUpdateNodeToOtherCaches(key, now)); //start new thread to broadcast update timestamp and move to front of cache to other caches?
            found.Value._timestamp = now;
            _cacheLRU.AddFirst(found);
            return found.Value._data;
        }

        public void Add(int key, T value)
        {
            DateTime now = DateTime.Now;
            bool isCacheFull = false;
            if(_cacheLookup.ContainsKey(key))
            {
                //update LRU
                LinkedListNode<Node> found = _cacheLRU.Find(_cacheLookup[key]);
                found.Value._timestamp = now;
                _cacheLRU.Remove(found);
                _cacheLRU.AddFirst(found);
                //Task.Run(() => BroadcastUpdateNodeToOtherCaches(key, now));
                return;
            }

            int LRUKey = 0;
            if(_cacheLRU.Count == _cacheCapacity) //cache is full remove LRU
            {
                LRUKey = _cacheLRU.Last.Value._key;
                _cacheLRU.RemoveLast();
                _cacheLookup.Remove(LRUKey); 
                isCacheFull = true;
            }

            Node newNode = new Node(value,key,now); 
            _cacheLRU.AddFirst(newNode);
            _cacheLookup.Add(key,newNode);

            if(isCacheFull)
            {
                //Task.Run(() => BroadcastRemoveAndAddNodeToOtherCaches(LRUKey,newNode));
            } 
            else
            {
                //Task.Run(() => BroadcastAddNodeToOtherCaches(newNode));
            }
        }

        #region Communication
        private void BroadcastRemoveNodeToOtherCaches(int key)
        {
            //do something 
        }

        private void BroadcastUpdateNodeToOtherCaches(int key, DateTime timestamp)
        {
            //do something
        }

        private void BroadcastAddNodeToOtherCaches(Node nodeToAdd)
        {
            //do something
        }

        private void BroadcastRemoveAndAddNodeToOtherCaches(int keyToRemove, Node nodeToAdd)
        {
            //do something
        }

        public void ReceiveRemoveNodeFromOtherCaches(int key)
        {
            LinkedListNode<Node> found = _cacheLRU.Find(_cacheLookup[key]);
            if(found != null)
            {
                _cacheLRU.Remove(found);
                _cacheLookup.Remove(key);
            }
        }

        public void ReceiveUpdateNodeFromOtherCaches(int key, DateTime timestamp)
        {
            LinkedListNode<Node> found = _cacheLRU.Find(_cacheLookup[key]);
            if(found != null)
            {
                found.Value._timestamp = timestamp;
                _cacheLRU.Remove(found);
                _cacheLRU.AddFirst(found);
            }
        }

        public void ReceiveAddNodeFromOtherCaches(Node nodeToAdd)
        {
            _cacheLRU.AddFirst(nodeToAdd);
            _cacheLookup.Add(nodeToAdd._key,nodeToAdd);
        }

        public void ReceiveRemoveAndAddNodeFromOtherCaches(int keyToRemove, Node nodeToAdd)
        {
            _cacheLRU.RemoveLast(); //may need to search by value instead...incase cache order not synced between instances
            _cacheLookup.Remove(keyToRemove);
            _cacheLRU.AddFirst(nodeToAdd);
            _cacheLookup.Add(nodeToAdd._key,nodeToAdd);
        }
        #endregion
    }
}