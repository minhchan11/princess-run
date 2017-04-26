using princessrun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace princessrun.Tests
{
    public class ThingTest
    {
        [Fact]
        public  void GetThingTest()
        {
            var thing = new Thing();

            thing.Name = "Yeh";

            var result = thing.Name;

            Assert.Equal("Yeh", result);
        }
    }
}
