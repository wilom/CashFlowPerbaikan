using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dokuku;

namespace UnitTest
{
    [TestClass]
    public class PeriodIdTest
    {
        [TestMethod]
        public void TestPeriodEquality()
        {
            var id1 = new PeriodeId(new DateTime(2015, 11, 1), new DateTime(2015, 11, 6));
            var id2 = new PeriodeId(new DateTime(2015, 11, 1), new DateTime(2015, 11, 6));
            Assert.AreEqual(id1, id2);
        }
        [TestMethod]
        public void TestIsDateInPeriod()
        {
            var id1 = new PeriodeId(new DateTime(2015, 11, 1), new DateTime(2015, 11, 6));
            Assert.IsTrue(id1.IsInPeriod(new DateTime(2015, 11, 2)));
            Assert.IsTrue(id1.IsInPeriod(new DateTime(2015, 11, 3)));
            Assert.IsTrue(id1.IsInPeriod(new DateTime(2015, 11, 4)));
            Assert.IsTrue(id1.IsInPeriod(new DateTime(2015, 11, 5)));            
        }
    }
}
