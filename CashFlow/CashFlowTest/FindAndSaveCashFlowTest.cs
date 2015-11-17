using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dokuku.CashFlowHead;
using Moq;
using dokuku.service;
using dokuku.Dto;
using dokuku.interfaces;
using System.Collections.Generic;
using dokuku;
using dokuku.exceptions;

namespace UnitTest
{
    [TestClass]
    public class FindAndSaveCashFlowTest
    {
        [TestInitialize]
        public void init()
        {
            var periodeId = "20151101";  
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
            cashFlowCreate.Setup(x => x.GenerateId()).Returns(new CashFlowId(periodeId));
            var repo = new InMemoryRepository();
            repo.Save(cashFlowCreate.Object);
            var cashFlow = repo.FindCashFlowByPeriod(periodeId);
            Assert.AreEqual(cashFlow.Snap().SaldoAkhir, 850000.0);
        }
        [TestMethod]
        public void testFindAndSaveCashFlowByPeriod()
        {
            var periodeId = "20151101";         
            
            var cashflowSnapshot = new CashFlowDto()
            {
                TenantId = "ABC",
                PeriodId = "2015101",
                SaldoAwal = 1000000.0,
                SaldoAkhir = 950000.0,
                TotalPenjualan = 0.0,
                TotalPenjualanLain = 0.0,
                TotalPengeluaran = 50000.0,
            };
            var factory = new MockRepository(MockBehavior.Loose);
            var cashFlowCreate = factory.Create<ICashFlow>();
            cashFlowCreate.Setup(x => x.Snap()).Returns(cashflowSnapshot);
            cashFlowCreate.Setup(x => x.GenerateId()).Returns(new CashFlowId(periodeId));
            var repo = new InMemoryRepository();
            repo.Save(cashFlowCreate.Object);
            var cashFlow = repo.FindCashFlowByPeriod(periodeId);
            Assert.AreEqual(cashFlow.Snap().SaldoAkhir, 950000.0);
        }

        //[TestMethod]
        //public void testCaseIdSudahAdaDiDB()
        //{
        //    var periodeId = "20151101";

        //    var cashflowSnapshot = new CashFlowDto()
        //    {
        //        TenantId = "ABC",
        //        PeriodId = "2015101",
        //        SaldoAwal = 1000000.0,
        //        SaldoAkhir = 850000.0,
        //        TotalPenjualan = 0.0,
        //        TotalPenjualanLain = 0.0,
        //        TotalPengeluaran = 150000.0,
        //    };
        //    var factory = new MockRepository(MockBehavior.Loose);
        //    var cashFlowCreate = factory.Create<ICashFlow>();
        //    cashFlowCreate.Setup(x => x.Snap()).Returns(cashflowSnapshot);
        //    cashFlowCreate.Setup(x => x.GenerateId()).Returns(new CashFlowId(periodeId));
        //    var repo = new InMemoryRepository();
        //    repo.Save(cashFlowCreate.Object);
        //    var cashFlow = repo.FindCashFlowByPeriod(periodeId);
        //    Assert.AreEqual(cashflowSnapshot, cashFlow.Snap());
        //}
    }
}
