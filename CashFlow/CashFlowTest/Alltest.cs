using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dokuku.ex;
using dokuku.Dto;
using dokuku;

namespace CashFlowTest
{
    [TestClass]
    public class Alltest
    {
        NotaPengeluaran _notaPengeluaran;
        Periode _periode;
        Sales _sales;
        SalesLain _salesLain;

        [TestInitialize]
        public void init()
        {
            _notaPengeluaran = new NotaPengeluaran("NT0001", new DateTime(2015, 1, 1), "Bahan", 20000.0);
            _sales = new Sales(new DateTime(2015, 1, 1), 20000.0);
            _periode = new Periode("P0001", "Kranggan", StatusPeriode.Open);
            _salesLain = new SalesLain("SL0001", new DateTime(2015, 1, 1), 20000.0, "Kardus");
        }

        //testperiode---
       
        [TestMethod]
        public void BuatPeriodeTest()
        {
            PeriodeDto snapshot = _periode.SnapShot();

            var expect = new PeriodeDto() { Id = "P0001", Cabang = "Kranggan", Status = StatusPeriode.Open};
            Assert.AreEqual(expect, snapshot);
        }
        //endperiode---
        
        //testsales---
        [TestMethod]
        public void BuatSales()
        {
            SalesDto snapshot = _sales.SnapShot();

            var expect = new SalesDto() { TglSales = new DateTime(2015, 1, 1), Nominal = 20000.0 };
            Assert.AreEqual(expect, snapshot);
        }
        //endsales---
        
        //testSalesLain---
        [TestMethod]
        public void BuatSalesLain()
        {           
            SalesLainDto snapshot = _salesLain.SnapShot();

            var expect = new SalesLainDto() { IdSalesLain = "SL0001", TglSalesLain = new DateTime(2015, 1, 1), Nominal = 20000.0, Keterangan = "Kardus" };
            Assert.AreEqual(expect, snapshot);                      
        }
        //endSalsesLain---

        //notaPengeluaran---
        
        [TestMethod]
        public void IsiItem()
        {
            var items = new ItemNota("Ayam", 20000.0);
            ItemsDto snapshot = items.SnapShot();
            Assert.IsNotNull(snapshot);
        }
        [TestMethod]
        public void EditNoNota()
        {
            string noNota = "123";
            _notaPengeluaran.EditNoNota(noNota);
            Assert.AreEqual(noNota, _notaPengeluaran.GetNoNota());
        }
        [TestMethod]
        public void CreateDTONotaPengeluaran()
        {
            NotaPengeluaranDto snapshot = _notaPengeluaran.SnapShot();
            Assert.IsNotNull(snapshot.Items);
        }
        //endNotaPengeluaran---

        //cashFlow---
        [TestMethod]
        public void BuatCashFlow()
        {
            string id = _periode.GetId();
            string cabang = _periode.GetCabang();
            double totalSales = _sales.GetTotalSales();
            double totalSalesLain = _salesLain.GetTotalSalesLain();
            double totalNota = _notaPengeluaran.GetTotalNota();

            var cashFlowEntry = new CashFlowEntry(id, cabang, 20000.0, 30000.0, totalSales, totalSalesLain, totalNota);
            CashFlowEntryDto snapshot = cashFlowEntry.SnapShot();

            var expect = new CashFlowEntryDto()
            {
                Id = id,
                Cabang = cabang,
                SaldoAwal = 20000.0,
                SaldoAkhir = 30000.0,
                TotalSales = totalSales,
                TotalSaleslain = totalSalesLain,
                TotalNota = totalNota
            };
            Assert.AreEqual(expect, snapshot);
        }
        //endCashflow---
    }
}
