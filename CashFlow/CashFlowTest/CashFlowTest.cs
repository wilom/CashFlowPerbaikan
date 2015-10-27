using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dokuku.CashFlowHead;
using dokuku;
using dokuku.ex;
using dokuku.Dto;
using dokuku.exceptions;
using Moq;
using dokuku.interfaces;
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
            PeriodeId periodeId = new PeriodeId("2015101");
            

            _cashFlow = new CashFlow("ABC", periodeId, 500000.0);
            _periode = new Periode(periodeId,StatusPeriode.Mingguan);

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

            _cashFlow.ChangePengeluaran("Ayam", 500000.0);
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

        [TestMethod]
        public void testUbahPengeluaranJikaAccountSudahAda()
        {
            _cashFlow.ChangePengeluaran("Ayam", 400000.0);
            _cashFlow.ChangePengeluaran("Ayam", 600000.0);
            var snapPengeluaran = _cashFlow.Snap();
            var expectedPengeluaran = new CashFlowDto()
            {
                TenantId = "ABC",
                PeriodId = "2015101",
                SaldoAwal = 500000.0,
                SaldoAkhir = -100000.0,
                TotalPenjualan = 0.0,
                TotalPenjualanLain = 0.0,
                TotalPengeluaran = 600000.0
            };
            Assert.AreEqual(expectedPengeluaran, snapPengeluaran);
        }

        [TestMethod]
        public void testSecenario()
        {
            PeriodeId periodid = new PeriodeId("2015101");
            _cashFlow = new CashFlow("ABC", periodid, 1000000.0);
            _cashFlow.AddSales(new DateTime(2015, 10, 1), 1500000.0);
            _cashFlow.AddSales(new DateTime(2015, 10, 2), 2000000.0);
            _cashFlow.ChangePengeluaran("Ayam", 1400000.0);
            _cashFlow.ChangePengeluaran("Ayam", 2400000.0);
            var snapPengeluaran = _cashFlow.Snap();
            var expectedPengeluaran = new CashFlowDto()
            {
                TenantId = "ABC",
                PeriodId = "2015101",
                SaldoAwal = 1000000.0,
                SaldoAkhir = 2100000.0,
                TotalPenjualan = 3500000.0,
                TotalPenjualanLain = 0.0,
                TotalPengeluaran = 2400000.0
            };
            Assert.AreEqual(expectedPengeluaran, snapPengeluaran);
        }

        [TestMethod]
        [ExpectedException(typeof(CashflowNotFoundException), "Find")]
        //[ExpectedException(ExpectedException = typeof(InvalidOperationException))]
        public void testProsesNotaPengeluaran()
        {
            var factory = new MockRepository(MockBehavior.Loose);
            var mockRepository = factory.Create<IRepository>();
            var mockCashFlow = factory.Create<ICashFlow>();
            var mockPengeluaran = factory.Create<INotaPengeluaran>();
            var mockCurrentPeriod = factory.Create<IPeriod>();

            mockRepository.Setup(t => t.FindPeriodForDate(new DateTime(2015, 10, 26))).Returns(mockCurrentPeriod.Object);
            mockRepository.Setup(t => t.FindCashFlowByPeriod(mockCurrentPeriod.ToString())).Returns(mockCashFlow.Object);
            mockCurrentPeriod.Setup(t => t.ToString()).Returns("20151101");


            var service = new ProcessNotaPengeluaran();
            service.Repository = mockRepository.Object;
            service.Process(mockCashFlow.Object);

            factory.Verify();            
        }

    }

}
