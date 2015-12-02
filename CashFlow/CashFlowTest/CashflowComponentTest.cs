using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dokuku.service;
using dokuku;
using dokuku.ex;
using dokuku.Dto;
using Moq;
using dokuku.interfaces;
using dokuku.CashFlowHead;

namespace UnitTest
{
    [TestClass]
    public class CashflowComponentTest
    {
        [TestMethod]
        public void TestCashflowSucessCase()
        {
            var repo = new InMemoryRepository();           
            var periodeId1 = new PeriodeId(new DateTime(2015, 11, 1), new DateTime(2015, 11, 6));
            var periode = new Periode(periodeId1, StatusPeriode.Mingguan);
            repo.SavePeriod(periode);
            var periodeSnapShot = new PeriodeDto()
            {
                StartPeriode = new DateTime(2015, 11, 1),
                EndPeriode = new DateTime(2015, 11, 6),
                IsPeriode = StatusPeriode.Bebas
            };          
           
            var periodeSave = repo.FindPeriodForDate(new DateTime(2015, 11, 3));
            
            Assert.AreEqual(periodeSnapShot, periodeSave.Snap());

            //cashflow           
            var cashFlow = new CashFlow("ABC", periodeId1, 500000.0);
            repo.Save(cashFlow);

            var cashflowSnapshotDto = new CashFlowDto()
            {
                TenantId = "ABC",
                PeriodId = periode.Snap(),
                SaldoAwal = 500000.0,
                SaldoAkhir = 500000.0,
                TotalPenjualan = 0.0,
                TotalPenjualanLain = 0.0,
                TotalPengeluaran = 0.0,
            };


            var findCashFlow = repo.FindCashFlowByPeriod(periodeId1);
            Assert.AreEqual(cashflowSnapshotDto, findCashFlow.Snap());

            //Penjualan                  
            cashFlow.AddPenjualan(new DateTime(2015, 10, 1), 200000.0);
            //repo.Save(cashFlow);
            var cashFlowSnapshot = cashFlow.Snap();
            var cashflowPenjualanSnapshot = new CashFlowDto()
            {
                TenantId = "ABC",
                PeriodId = periode.Snap(),
                SaldoAwal = 500000.0,
                SaldoAkhir = 700000.0,
                TotalPenjualan = 200000.0,
                TotalPenjualanLain = 0.0,
                TotalPengeluaran = 0.0,                
            };
            Assert.AreEqual(cashFlowSnapshot, cashflowPenjualanSnapshot);
            Assert.AreEqual(1, cashFlowSnapshot.ItemsPenjualan.Count);
            var itemPenjualan = cashFlowSnapshot.ItemsPenjualan[0];
            Assert.AreEqual(new DateTime(2015, 10, 1), itemPenjualan.DateTime);
            Assert.AreEqual(200000.0, itemPenjualan.Nominal);
        }
    }
}
