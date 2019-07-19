using System;
using Xunit;
using LRUCacheService;
using System.Collections.Generic;
using System.Threading;

namespace LRUCacheService.Tests
{
    public class LRUCacheUnitTest
    {
        [Fact]
        public void LRUTest()
        {
            int cacheCapacity = 3;
            int expirationInSeconds = 5;
            LRUCache<int?> cache = new LRUCache<int?>(cacheCapacity, expirationInSeconds);
            cache.Add(1, 1);
            int? testOne = cache.Get(1);
            Assert.Equal(1, testOne);

            cache.Add(2, 2);
            int? testTwo = cache.Get(2);
            Assert.Equal(2, testTwo);

            cache.Add(3, 3); //list order is now 3 2 1  (LRU at far right)
            int? testThree = cache.Get(3);
            Assert.Equal(3, testThree);    
            
            testOne = cache.Get(1); //list order is now 1 3 2 
            Assert.Equal(1, testOne);

            Assert.Null(cache.Get(4));

            cache.Add(4, 4); //list order is now 4 1 3
            int? testFour = cache.Get(4);
            Assert.Equal(4, testFour);

            testOne = cache.Get(1); //list order is now 1 4 3
            Assert.Equal(1, testOne);

            testTwo = cache.Get(2);
            Assert.Null(testTwo);

            cache.Add(3,3); //list order is now 3 1 4
            cache.Add(2,2); //list order is now 2 3 1

            Assert.Null(cache.Get(4));
            testThree = cache.Get(3); //list order is now 3 2 1
            Assert.Equal(3, testThree);
        }

        [Fact]
        public void LRUExpirationTime()
        {
            int cacheCapacity = 3;
            int expirationInSeconds = 5;
            LRUCache<int?> cache = new LRUCache<int?>(cacheCapacity, expirationInSeconds);
            cache.Add(1,1);
            Thread.Sleep(5000);
            Assert.Null(cache.Get(1)); //expired
            cache.Add(1,1);
            Thread.Sleep(3000);
            cache.Get(1); //updated expiration time
            Thread.Sleep(3000);
            Assert.Equal(1, cache.Get(1));//not yet expired
        }
    }
}