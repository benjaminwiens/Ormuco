using System;
using Xunit;
using LRUCacheService;
using System.Collections.Generic;

namespace LRUCacheService.Tests
{
    public class LRUCacheUnitTest
    {
        public class GEOData
        {
            public string _location{get;set;}
            public GEOData(){}
        }

        [Fact]
        public void LRUTest()
        {

            LRUCache<GEOData> cache = new LRUCache<GEOData>(3, 20);

            GEOData one = new GEOData(){
                _location = "one"
            };

            GEOData two = new GEOData(){
                _location = "two"
            };

            GEOData three = new GEOData(){
                _location = "three"
            };

            GEOData four = new GEOData(){
                _location = "four"
            };
            
            cache.Add(1, one);
            GEOData testOne = cache.Get(1);
            Assert.Equal("one", testOne._location);

            cache.Add(2, two);
            GEOData testTwo = cache.Get(1);
            Assert.Equal("two", testTwo._location);

            cache.Add(3, three);
            GEOData testThree = cache.Get(1);
            Assert.Equal("three", testThree._location);

            cache.Add(4, four);
            GEOData testFour = cache.Get(1);
            Assert.Equal("four", testFour._location);

            testOne = cache.Get(1);
            Assert.Null(testOne);
        }
    }
}
