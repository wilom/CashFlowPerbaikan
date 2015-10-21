using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dokuku.CashFlowHead;
using dokuku;
using dokuku.ex;
using dokuku.Dto;
using dokuku.exceptions;

namespace UnitTest
{
    [TestClass]
    public class CashFlowTest
    {
        CashFlow _cashFlow = null;
        Periode _periode = null;
       
       
        [TestInitialize]
        public void init() 
        {
            PeriodeId periodid = new PeriodeId("2015101");
            

            _cashFlow = new CashFlow("ABC", periodid, 500000.0);
            _periode = new Periode(periodid,StatusPeriode.Mingguan);

        }
        
        [TestMethod]
        public void TestMembukaCashflow()
        {
            var cashFlowSnapshot = _cashFlow.Snap();
            var expected = new CashFlowDto()
            {
                TenantId = "ABC",
                PeriodId = "2015101",
                SaldoAwal = 500000.0,
                SaldoAkhir = 500000.0,
                TotalPenjualan = 0.0,
                TotalPenjualanLain = 0.0,
                TotalPengeluaran = 0.0
            };
            Assert.AreEqual(expected, cashFlowSnapshot);
        }

        [TestMethod]
        public void TestPeriode() 
        {
            var periodeSnapShot = _periode.Snap();
            var expected = new PeriodeDto()
            {
                Id = "2015101",
                IsPeriode = StatusPeriode.Mingguan
            };
            Assert.AreEqual(expected, periodeSnapShot);
        }

        [TestMethod]
        public void TestTambahPenjualan()
        {
            
            _cashFlow.AddSales(new DateTime(2015, 10, 1), 200000);

            var cashFlowSnapshot = _cashFlow.Snap();
            var expected = new CashFlowDto()
            {
                TenantId = "ABC",
                PeriodId = "2015101",
                SaldoAwal = 500000.0,
                SaldoAkhir = 700000.0,
                TotalPenjualan = 200000.0,
                TotalPenjualanLain = 0.0,
                TotalPengeluaran = 0.0
            };
            Assert.AreEqual(expected, cashFlowSnapshot);
        }

        [TestMethod]

        public void testTambahPenjualanLagi() 
        {
            _cashFlow.AddSales(new DateTime(2015, 10, 2), 200000.0);
            _cashFlow.AddSales(new DateTime(2015, 10, 3), 300000.0);
            var cashFlowSnapshot = _cashFlow.Snap();
            var expected = new CashFlowDto()
            {
                TenantId = "ABC",
                PeriodId = "2015101",
                SaldoAwal = 500000.0,
                SaldoAkhir = 1000000.0,
                TotalPenjualan = 500000.0,
                TotalPenjualanLain = 0.0,
                TotalPengeluaran = 0.0
            };
            Assert.AreEqual(expected, cashFlowSnapshot);
        }

        [TestMethod]
        [ExpectedException(typeof(DateAlreadyExistException),"Tanggal sudah pernah diinput")]
        public void testHanyaBolehSatuSalesDalamSatuHari() 
        {
            _cashFlow.AddSales(new DateTime(2015, 10, 4), 200000.0);
            _cashFlow.AddSales(new DateTime(2015, 10, 4), 200000.0); 
            
        }

        //mulai pengeluaran------------------------------------
        [TestMethod]
        public void testPengeluaranCashFlow() 
        {

            _cashFlow.AddPengeluaran("Ayam", 500000.0);
            _cashFlow.AddSales(new DateTime(2015, 10, 1), 200000.0);
            _cashFlow.AddSales(new DateTime(2015, 10, 2), 100000.0);
            _cashFlow.AddSales(new DateTime(2015, 10, 3), 300000.0);
            _cashFlow.AddSales(new DateTime(2015, 10, 4), 400000.0);
            _cashFlow.AddSales(new DateTime(2015, 10, 5), 200000.0);
            var snapPengeluaran = _cashFlow.Snap();
            var expectedPengeluaran = new CashFlowDto()
            {
                TenantId = "ABC",
                PeriodId = "2015101",
                SaldoAwal = 500000.0,
                SaldoAkhir = 1200000.0,
                TotalPenjualan = 1200000.0,
                TotalPenjualanLain = 0.0,
                TotalPengeluaran = 500000.0
            };
            Assert.AreEqual(expectedPengeluaran, snapPengeluaran);
        }
    }
}
