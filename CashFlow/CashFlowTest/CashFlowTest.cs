using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dokuku.CashFlow;
using dokuku;
using dokuku.ex;
using dokuku.Dto;

namespace UnitTest
{
    [TestClass]
    public class CashFlowTest
    {
        CashFlow _cashFlow=null;
        [TestInitialize]
        public void init() 
        {
            PeriodeId period = new PeriodeId("20151001");
            
            _cashFlow = new CashFlow("ABC", period, 500000.0, 500000.0, 0.0, 0.0, 0.0);

        }
        [TestMethod]
        public void TestMembukaCashflow()
        {
            var cashFlowSnapshot = _cashFlow.Snap();
            var expected = new CashFlowDto()
            {
                TenantId = "ABC",
                PeriodId = "20151001",
                SaldoAwal = 500000.0,
                SaldoAkhir = 500000.0,
                TotalPenjualan = 0.0,
                TotalPenjualanLain = 0.0,
                TotalPengeluaran = 0.0
            };
            Assert.AreEqual(expected, cashFlowSnapshot);


        }
    }
}
