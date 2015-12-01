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
        PeriodeId _periodeId = new PeriodeId(new DateTime(2015, 11, 1), new DateTime(2015, 11, 6));
        InMemoryRepository _repo = new InMemoryRepository();
        MockRepository _factory = new MockRepository(MockBehavior.Loose);
        CashFlowDto _cashflowSnapshot = null;
         [TestInitialize]
        public void init()
        {           
            _cashflowSnapshot = new CashFlowDto()
            {
                TenantId = "ABC",
                PeriodId = new PeriodeDto(),
                SaldoAwal = 1000000.0,
                SaldoAkhir = 850000.0,
                TotalPenjualan = 0.0,
                TotalPenjualanLain = 0.0,
                TotalPengeluaran = 150000.0,
            };
           
            var cashFlowCreate = _factory.Create<ICashFlow>();
            cashFlowCreate.Setup(x => x.Snap()).Returns(_cashflowSnapshot);
            cashFlowCreate.Setup(x => x.GenerateId()).Returns(new CashFlowId(_periodeId));
           
            _repo.Save(cashFlowCreate.Object);
        }

        [TestMethod]
         public void testFindAndSaveCashFlowByPeriod()
        {
            
            var cashFlow = _repo.FindCashFlowByPeriod(_periodeId);
            Assert.AreEqual(_cashflowSnapshot, cashFlow.Snap());
        }

        [TestMethod]
        public void testCaseIdSudahAdaDiDBdanSimpanTerakhir()
        {             
            var cashflowSnapshot2 = new CashFlowDto()
            {
                TenantId = "ABC",
                PeriodId = new PeriodeDto(),
                SaldoAwal = 1000000.0,
                SaldoAkhir = 950000.0,
                TotalPenjualan = 0.0,
                TotalPenjualanLain = 0.0,
                TotalPengeluaran = 50000.0,
            };
           
            var cashFlowCreate2 = _factory.Create<ICashFlow>();
            cashFlowCreate2.Setup(x => x.Snap()).Returns(cashflowSnapshot2);
            cashFlowCreate2.Setup(x => x.GenerateId()).Returns(new CashFlowId(_periodeId));
           
            _repo.Save(cashFlowCreate2.Object);
            var cashFlow = _repo.FindCashFlowByPeriod(_periodeId);
            //Assert.AreEqual(cashFlow.Snap().SaldoAkhir, 950000.0);
            Assert.AreEqual(cashflowSnapshot2, cashFlow.Snap());
        }       
    }
}
