using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dokuku.CashFlowHead;
using dokuku;
using dokuku.ex;
using dokuku.Dto;
using dokuku.exceptions;
using Moq;
using dokuku.interfaces;
using System.Collections.Generic;
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

        //mulain penjualan
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

        //mulain penjualan lain
        [TestMethod]
        public void TestTambahPenjualanLain()
        {

            _cashFlow.AddSales(new DateTime(2015, 10, 1), 200000);
            _cashFlow.AddSalesLain(new DateTime(2015, 10, 1), 200000);

            var cashFlowSnapshot = _cashFlow.Snap();
            var expected = new CashFlowDto()
            {
                TenantId = "ABC",
                PeriodId = "2015101",
                SaldoAwal = 500000.0,
                SaldoAkhir = 900000.0,
                TotalPenjualan = 200000.0,
                TotalPenjualanLain = 200000.0,
                TotalPengeluaran = 0.0
            };
            Assert.AreEqual(expected, cashFlowSnapshot);
        }
        public void testTambahPenjualanLainLagi()
        {
            _cashFlow.AddSalesLain(new DateTime(2015, 10, 2), 200000.0);
            _cashFlow.AddSalesLain(new DateTime(2015, 10, 3), 300000.0);
            var cashFlowSnapshot = _cashFlow.Snap();
            var expected = new CashFlowDto()
            {
                TenantId = "ABC",
                PeriodId = "2015101",
                SaldoAwal = 500000.0,
                SaldoAkhir = 1000000.0,
                TotalPenjualan = 0.0,
                TotalPenjualanLain = 500000.0,
                TotalPengeluaran = 0.0
            };
            Assert.AreEqual(expected, cashFlowSnapshot);
        }

        [TestMethod]
        [ExpectedException(typeof(DateAlreadyExistException), "Tanggal sudah pernah diinput")]
        public void testHanyaBolehSatuSalesLainDalamSatuHari()
        {
            _cashFlow.AddSalesLain(new DateTime(2015, 10, 4), 200000.0);
            _cashFlow.AddSalesLain(new DateTime(2015, 10, 4), 200000.0);

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
            _cashFlow.AddSalesLain(new DateTime(2015, 10, 1), 1500000.0);
            _cashFlow.AddSalesLain(new DateTime(2015, 10, 2), 2000000.0);
            _cashFlow.ChangePengeluaran("Ayam", 1400000.0);
            _cashFlow.ChangePengeluaran("Ayam", 2400000.0);
            var snapPengeluaran = _cashFlow.Snap();
            var expectedPengeluaran = new CashFlowDto()
            {
                TenantId = "ABC",
                PeriodId = "2015101",
                SaldoAwal = 1000000.0,
                SaldoAkhir = 5600000.0,
                TotalPenjualan = 3500000.0,
                TotalPenjualanLain = 3500000.0,
                TotalPengeluaran = 2400000.0
            };
            Assert.AreEqual(expectedPengeluaran, snapPengeluaran);
        }

        [TestMethod]
       
        public void testProsesNotaPengeluaran()
        {
            var transactionDate = new DateTime(2015, 10, 26);
            var periodId = "20151104";
            var listAkun = new string[] { "Ayam" };
            var listSummaryAkun = new List<SummaryAkunDto>() 
            {
            new SummaryAkunDto(){ PeriodeId ="Ayam", Nominal=600000.0}
            };
            var factory = new MockRepository(MockBehavior.Loose);
            var mockRepository = factory.Create<IRepository>();
            var mockCashFlow = factory.Create<ICashFlow>();
            var mockPengeluaran = factory.Create<INotaPengeluaran>();
            var mockCurrentPeriod = factory.Create<IPeriod>();
            mockRepository.Setup(t => t.FindPeriodForDate(transactionDate)).Returns(mockCurrentPeriod.Object);
            mockCurrentPeriod.SetupGet(t => t.PeriodId).Returns(periodId);
            mockRepository.Setup(t => t.FindCashFlowByPeriod(periodId)).Returns(mockCashFlow.Object);
            mockPengeluaran.SetupGet(t => t.Date).Returns(transactionDate);
            mockPengeluaran.Setup(t => t.ListAkun()).Returns(listAkun);
            mockRepository.Setup(t => t.ListSummaryAkunIn(mockCurrentPeriod.Object, listAkun)).Returns(listSummaryAkun);
            mockCashFlow.Setup(t => t.ChangePengeluaran(It.IsAny<string>(), It.IsAny<double>()));
            mockRepository.Setup(t => t.Save(mockCashFlow.Object));

            var service = new ProcessNotaPengeluaran();
            service.Repository = mockRepository.Object;
            service.Process(mockPengeluaran.Object);
            factory.VerifyAll();
        }

    }

}
