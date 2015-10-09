using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CashFlow.ex;
using CashFlow;
using CashFlow.Dto;

namespace CashFlowTest
{
    [TestClass]
    public class Alltest
    {
        [TestMethod]
        public void BuatPeriodeTest()
        {
            var periode = new Periode("P0001", "Kranggan", StatusPeriode.Open);
            PeriodeDto snapshot = periode.SnapShot();

            var expect = new PeriodeDto() { Id = "P0001", Cabang = "Kranggan", Status = StatusPeriode.Open};
            Assert.AreEqual(expect, snapshot);
        }
    }
}
