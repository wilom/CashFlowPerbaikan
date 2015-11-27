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
        PeriodeId _periodeId = null;
        
       
        [TestInitialize]
        public void init() 
        {
            _periodeId = new PeriodeId(new DateTime(2015, 11, 1), new DateTime(2015, 11, 6));
            
            _cashFlow = new CashFlow("ABC", _periodeId, 500000.0);
            _periode = new Periode(_periodeId,StatusPeriode.Mingguan);
        }
        
        [TestMethod]
        public void testMembukaCashflow()
        {
            var cashFlowSnapshot = _cashFlow.Snap();
            var expected = new CashFlowDto()
            {
                TenantId = "ABC",
                PeriodId = _periode.Snap(),
                SaldoAwal = 500000.0,
                SaldoAkhir = 500000.0,
                TotalPenjualan = 0.0,
                TotalPenjualanLain = 0.0,
                TotalPengeluaran = 0.0
            };
            Assert.AreEqual(expected, cashFlowSnapshot);
        }

        [TestMethod]
        public void testPeriode() 
        {
            var periodeSnapShot = _periode.Snap();
            var expected = new PeriodeDto()
            {
                StartPeriode = new DateTime(2015, 11, 1),
                EndPeriode = new DateTime(2015, 11, 6)
            };
            Assert.AreEqual(expected, periodeSnapShot);
        }

        //mulain penjualan
        [TestMethod]
        public void testTambahPenjualan()
        {            
            _cashFlow.AddPenjualan(new DateTime(2015, 10, 1), 200000);

            var cashFlowSnapshot = _cashFlow.Snap();
            var expected = new CashFlowDto()
            {
                TenantId = "ABC",
                PeriodId = _periode.Snap(),
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
            _cashFlow.AddPenjualan(new DateTime(2015, 10, 2), 200000.0);
            _cashFlow.AddPenjualan(new DateTime(2015, 10, 3), 300000.0);
            var cashFlowSnapshot = _cashFlow.Snap();
            var expected = new CashFlowDto()
            {
                TenantId = "ABC",
                PeriodId = _periode.Snap(),
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
            _cashFlow.AddPenjualan(new DateTime(2015, 10, 4), 200000.0);
            _cashFlow.AddPenjualan(new DateTime(2015, 10, 4), 200000.0); 
            
        }

        //mulain penjualan lain
        [TestMethod]
        public void testTambahPenjualanLain()
        {

            _cashFlow.AddPenjualan(new DateTime(2015, 10, 1), 200000);
            _cashFlow.AddPenjualanLain(new DateTime(2015, 10, 1), 200000);

            var cashFlowSnapshot = _cashFlow.Snap();
            var expected = new CashFlowDto()
            {
                TenantId = "ABC",
                PeriodId = _periode.Snap(),
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
            _cashFlow.AddPenjualanLain(new DateTime(2015, 10, 2), 200000.0);
            _cashFlow.AddPenjualanLain(new DateTime(2015, 10, 3), 300000.0);
            var cashFlowSnapshot = _cashFlow.Snap();
            var expected = new CashFlowDto()
            {
                TenantId = "ABC",
                PeriodId = _periode.Snap(),
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
            _cashFlow.AddPenjualanLain(new DateTime(2015, 10, 4), 200000.0);
            _cashFlow.AddPenjualanLain(new DateTime(2015, 10, 4), 200000.0);
        }

        //mulai pengeluaran------------------------------------
        [TestMethod]
        public void testPengeluaranCashFlow() 
        {
            _cashFlow.ChangePengeluaran("Ayam", 500000.0, 2);
            _cashFlow.AddPenjualan(new DateTime(2015, 10, 1), 200000.0);
            _cashFlow.AddPenjualan(new DateTime(2015, 10, 2), 100000.0);
            _cashFlow.AddPenjualan(new DateTime(2015, 10, 3), 300000.0);
            _cashFlow.AddPenjualan(new DateTime(2015, 10, 4), 400000.0);
            _cashFlow.AddPenjualan(new DateTime(2015, 10, 5), 200000.0);
            var snapPengeluaran = _cashFlow.Snap();
            var expectedPengeluaran = new CashFlowDto()
            {
                TenantId = "ABC",
                PeriodId = _periode.Snap(),
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
            _cashFlow.ChangePengeluaran("Ayam", 400000.0,2);
            _cashFlow.ChangePengeluaran("Ayam", 600000.0,3);
            var snapPengeluaran = _cashFlow.Snap();
            var expectedPengeluaran = new CashFlowDto()
            {
                TenantId = "ABC",
                PeriodId = _periode.Snap(),
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
            PeriodeId periodid = _periodeId;
            _cashFlow = new CashFlow("ABC", periodid, 1000000.0);
            _cashFlow.AddPenjualan(new DateTime(2015, 10, 1), 1500000.0);
            _cashFlow.AddPenjualan(new DateTime(2015, 10, 2), 2000000.0);
            _cashFlow.AddPenjualanLain(new DateTime(2015, 10, 1), 1500000.0);
            _cashFlow.AddPenjualanLain(new DateTime(2015, 10, 2), 2000000.0);
            _cashFlow.ChangePengeluaran("Ayam", 1400000.0,7);
            _cashFlow.ChangePengeluaran("Ayam", 2400000.0,12);
            var snapPengeluaran = _cashFlow.Snap();
            var expectedPengeluaran = new CashFlowDto()
            {
                TenantId = "ABC",
                PeriodId = _periode.Snap(),
                SaldoAwal = 1000000.0,
                SaldoAkhir = 5600000.0,
                TotalPenjualan = 3500000.0,
                TotalPenjualanLain = 3500000.0,
                TotalPengeluaran = 2400000.0
            };
            Assert.AreEqual(expectedPengeluaran, snapPengeluaran);
        }

        // mulai intance item penjualan,penjualanLain dan pengeluaran 
        [TestMethod]
        public void testMenginstanceDariSnapshot()
        {
            PeriodeId periodid = _periodeId;
            _cashFlow = new CashFlow("ABC", periodid, 1000000.0);
            _cashFlow.AddPenjualan(new DateTime(2015, 10, 1), 1500000.0);
            _cashFlow.AddPenjualan(new DateTime(2015, 10, 2), 2000000.0);
            _cashFlow.AddPenjualanLain(new DateTime(2015, 10, 1), 1500000.0);
            _cashFlow.AddPenjualanLain(new DateTime(2015, 10, 2), 2000000.0);
            _cashFlow.ChangePengeluaran("Ayam", 1400000.0,7);
            _cashFlow.ChangePengeluaran("Ayam", 2400000.0,14);

            var cashFlowSnapshot = _cashFlow.Snap();            
            var cashflow = new CashFlow(cashFlowSnapshot);
            var loadedSanpshot = cashflow.Snap();

            Assert.AreEqual(cashFlowSnapshot, loadedSanpshot);          

            Assert.AreEqual(2, loadedSanpshot.ItemsPenjualan.Count);
            var in1ItemPenjualanDto = loadedSanpshot.ItemsPenjualan[0];
            var in2ItemPenjualanDto = loadedSanpshot.ItemsPenjualan[1];
            Assert.AreEqual(new DateTime(2015, 10, 1), in1ItemPenjualanDto.DateTime);
            Assert.AreEqual(1500000.0, in1ItemPenjualanDto.Nominal);
            Assert.AreEqual(new DateTime(2015, 10, 2), in2ItemPenjualanDto.DateTime);
            Assert.AreEqual(2000000.0, in2ItemPenjualanDto.Nominal);

            Assert.AreEqual(2, loadedSanpshot.ItemsPenjualanLain.Count);
            var in1ItemPenjualanLainDto = loadedSanpshot.ItemsPenjualanLain[0];
            var in2ItemPenjualanLainDto = loadedSanpshot.ItemsPenjualanLain[1];            
            Assert.AreEqual(new DateTime(2015, 10, 1), in1ItemPenjualanLainDto.DateTimeLain);
            Assert.AreEqual(1500000.0, in1ItemPenjualanLainDto.NominalLain);
            Assert.AreEqual(new DateTime(2015, 10, 2), in2ItemPenjualanLainDto.DateTimeLain);
            Assert.AreEqual(2000000.0, in2ItemPenjualanLainDto.NominalLain);

            Assert.AreEqual(1, loadedSanpshot.ItemsPengeluaran.Count);
            var in1ItemPengeluaranDto = loadedSanpshot.ItemsPengeluaran[0];
            Assert.AreEqual("Ayam", in1ItemPengeluaranDto.Akun);
            Assert.AreEqual(2400000.0, in1ItemPengeluaranDto.Nominal);
            Assert.AreEqual(14, in1ItemPengeluaranDto.Jumlah);
           
        }
       
        [TestMethod]
        public void testCashFlowItemsPenjualan()
        {
            PeriodeId periodid = _periodeId;
            _cashFlow = new CashFlow("ABC", periodid, 1000000.0);
            _cashFlow.AddPenjualan(new DateTime(2015, 10, 1), 1500000.0);         
            var cashFlowSnapshot = _cashFlow.Snap();
            var expected = new CashFlowDto()
            {
                TenantId = "ABC",
                PeriodId = _periode.Snap(),
                SaldoAwal = 1000000.0,
                SaldoAkhir = 2500000.0,
                TotalPenjualan = 1500000.0,
                TotalPenjualanLain = 0.0,
                TotalPengeluaran = 0.0
            };
            Assert.AreEqual(expected, cashFlowSnapshot);
            Assert.AreEqual(1, cashFlowSnapshot.ItemsPenjualan.Count);
            var itemPenjualanDto= cashFlowSnapshot.ItemsPenjualan[0];
            Assert.AreEqual(new DateTime (2015,10,1),itemPenjualanDto.DateTime);            
            Assert.AreEqual(1500000.0, itemPenjualanDto.Nominal);
        }

        [TestMethod]
        public void testCashFlowItemsPenjualanLain()
        {
            PeriodeId periodid = _periodeId;
            _cashFlow = new CashFlow("ABC", periodid, 1000000.0);
            _cashFlow.AddPenjualanLain(new DateTime(2015, 10, 1), 1500000.0);
            var cashFlowSnapshot = _cashFlow.Snap();
            var expected = new CashFlowDto()
            {
                TenantId = "ABC",
                PeriodId = _periode.Snap(),
                SaldoAwal = 1000000.0,
                SaldoAkhir = 2500000.0,
                TotalPenjualan = 0.0,
                TotalPenjualanLain = 1500000.0,
                TotalPengeluaran = 0.0
            };
            Assert.AreEqual(expected, cashFlowSnapshot);
            Assert.AreEqual(1, cashFlowSnapshot.ItemsPenjualanLain.Count);
            var itemPenjualanLainDto = cashFlowSnapshot.ItemsPenjualanLain[0];
            Assert.AreEqual(new DateTime(2015, 10, 1), itemPenjualanLainDto.DateTimeLain);
            Assert.AreEqual(1500000.0, itemPenjualanLainDto.NominalLain);
        }

        [TestMethod]
        public void testCashFlowItemsPengeluaran()
        {
            PeriodeId periodid = _periodeId;
            _cashFlow = new CashFlow("ABC", periodid, 1000000.0);
            _cashFlow.ChangePengeluaran("Ayam",150000.0,7);
            var cashFlowSnapshot = _cashFlow.Snap();
            var expected = new CashFlowDto()
            {
                TenantId = "ABC",
                PeriodId = _periode.Snap(),
                SaldoAwal = 1000000.0,
                SaldoAkhir = 850000.0,
                TotalPenjualan = 0.0,
                TotalPenjualanLain = 0.0,
                TotalPengeluaran = 150000.0,               
            };
            Assert.AreEqual(expected, cashFlowSnapshot);
            Assert.AreEqual(1, cashFlowSnapshot.ItemsPengeluaran.Count);
            var itemPengeluaranDto = cashFlowSnapshot.ItemsPengeluaran[0];
            Assert.AreEqual("Ayam", itemPengeluaranDto.Akun);
            Assert.AreEqual(150000.0, itemPengeluaranDto.Nominal);
            Assert.AreEqual(7, itemPengeluaranDto.Jumlah);
        }        
    }
}
