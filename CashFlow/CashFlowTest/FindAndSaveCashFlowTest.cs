using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dokuku.CashFlowHead;
using Moq;
using dokuku.service;
using dokuku.Dto;
using dokuku.interfaces;
using System.Collections.Generic;
using dokuku;

namespace UnitTest
{
    [TestClass]
    public class FindAndSaveCashFlowTest
    {        
        [TestMethod]
        public void testFindAndSaveCashFlowByPeriod()
        {
            var periode = "20151101";         
            
            var cashflowSnapshot = new CashFlowDto()
            {
                TenantId = "ABC",
                PeriodId = "2015101",
                SaldoAwal = 1000000.0,
                SaldoAkhir = 850000.0,
                TotalPenjualan = 0.0,
                TotalPenjualanLain = 0.0,
                TotalPengeluaran = 150000.0,
            };
            var factory = new MockRepository(MockBehavior.Loose);
            var cashFlowCreate = factory.Create<ICashFlow>();
            cashFlowCreate.Setup(x => x.Snap()).Returns(cashflowSnapshot);
            var repo = new InMemoryRepository();
            repo.Save(cashFlowCreate.Object);
            var cashFlow = repo.FindCashFlowByPeriod(periode);           
            Assert.AreEqual(cashflowSnapshot,cashFlow.Snap());                   

        }
    }
}
