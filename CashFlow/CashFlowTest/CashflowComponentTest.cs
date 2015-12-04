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
            cashFlow.AddPenjualan(new DateTime(2015, 11, 1), 200000.0);            
            repo.Save(cashFlow);
            var repoFind = repo.FindPeriodForDate(new DateTime(2015, 11, 3));
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
         
            Assert.AreEqual(cashflowPenjualanSnapshot, cashFlowSnapshot);
            Assert.AreEqual(1, cashFlowSnapshot.ItemsPenjualan.Count);
            var itemPenjualan = cashFlowSnapshot.ItemsPenjualan[0];
            Assert.AreEqual(new DateTime(2015, 11, 1), itemPenjualan.DateTime);
            Assert.AreEqual(200000.0, itemPenjualan.Nominal);

            //PenjualanLain
            cashFlow.AddPenjualanLain(new DateTime(2015, 11, 1), 200000.0);
            repo.Save(cashFlow);
            var repoFindLain = repo.FindPeriodForDate(new DateTime(2015, 11, 3));
            var cashFlowSnapshotLain = cashFlow.Snap();
            var cashflowPenjualanLainSnapshot = new CashFlowDto()
            {
                TenantId = "ABC",
                PeriodId = periode.Snap(),
                SaldoAwal = 500000.0,
                SaldoAkhir = 900000.0,
                TotalPenjualan = 200000.0,
                TotalPenjualanLain = 200000.0,
                TotalPengeluaran = 0.0,
            };
            
            Assert.AreEqual(cashflowPenjualanLainSnapshot, cashFlowSnapshotLain);
            Assert.AreEqual(1, cashFlowSnapshotLain.ItemsPenjualanLain.Count);
            var itemPenjualanLain = cashFlowSnapshotLain.ItemsPenjualanLain[0];
            Assert.AreEqual(new DateTime(2015, 11, 1), itemPenjualanLain.DateTimeLain);
            Assert.AreEqual(200000.0, itemPenjualanLain.NominalLain);

            //Pengeluaran
            cashFlow.ChangePengeluaran("Ayam",200000,5);
            repo.Save(cashFlow);
            var repoFindPengeluaran = repo.FindPeriodForDate(new DateTime(2015, 11, 3));
            var cashFlowSnapshotPengeluaran = cashFlow.Snap();
            var cashflowPengeluaranSnapshot = new CashFlowDto()
            {
                TenantId = "ABC",
                PeriodId = periode.Snap(),
                SaldoAwal = 500000.0,
                SaldoAkhir = 700000.0,
                TotalPenjualan = 200000.0,
                TotalPenjualanLain = 200000.0,
                TotalPengeluaran = 200000.0,
            };

            Assert.AreEqual(cashflowPengeluaranSnapshot, cashFlowSnapshotPengeluaran);
            Assert.AreEqual(1, cashFlowSnapshotPengeluaran.ItemsPengeluaran.Count);
            var itemPengeluaran = cashFlowSnapshotPengeluaran.ItemsPengeluaran[0];
            Assert.AreEqual("Ayam", itemPengeluaran.Akun);
            Assert.AreEqual(200000.0, itemPengeluaran.Nominal);
            Assert.AreEqual(5, itemPengeluaran.Jumlah);

            //NotaPengeluaran
            
            var noNota = new NotaPengeluaranId("123");
            var notaPengeluaran = new NotaPengeluaran(new DateTime(2015, 11, 1), noNota);
            repo.SaveNota(notaPengeluaran);
            var repoFindNota = repo.FindNotaPengeluaranByID("123");
            //var notaSnapshot = notaPengeluaran.Snap();
            var notaSnap = new NotaPengeluaranDto()
            {
                Tanggal = new DateTime(2015, 11, 1),
                NoNota = noNota,
                TotalNota = 0.0
            };
            Assert.AreEqual(notaSnap, repoFindNota.Snap());

            ////AddAkunNota
            //notaPengeluaran.AddAkun("Ayam", 200000, 5);
            //repo.SaveNota(notaPengeluaran);
            //var repoFindNotaAkun = repo.FindPeriodForDate(new DateTime(2015, 11, 3));
            //var notaAkunSnapshot = notaPengeluaran.Snap();
            //var notaAkunSnap = new NotaPengeluaranDto()
            //{
            //    Tanggal = new DateTime(2015, 10, 26),
            //    NoNota = "123",
            //    TotalNota = 0.0
            //};
            //Assert.AreEqual(notaAkunSnap, notaAkunSnapshot);

        }
    }
}
