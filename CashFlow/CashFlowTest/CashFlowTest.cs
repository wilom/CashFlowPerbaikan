﻿using System;
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
        public void testMembukaCashflow()
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
        public void testPeriode() 
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
        public void testTambahPenjualan()
        {            
            _cashFlow.AddPenjualan(new DateTime(2015, 10, 1), 200000);

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
            _cashFlow.AddPenjualan(new DateTime(2015, 10, 2), 200000.0);
            _cashFlow.AddPenjualan(new DateTime(2015, 10, 3), 300000.0);
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
            _cashFlow.AddPenjualanLain(new DateTime(2015, 10, 2), 200000.0);
            _cashFlow.AddPenjualanLain(new DateTime(2015, 10, 3), 300000.0);
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
            _cashFlow.ChangePengeluaran("Ayam", 400000.0,2);
            _cashFlow.ChangePengeluaran("Ayam", 600000.0,3);
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
        public void testMenginstanceDariSnapshot()
        {
            PeriodeId periodid = new PeriodeId("2015101");
            _cashFlow = new CashFlow("ABC", periodid, 1000000.0);
            _cashFlow.AddPenjualan(new DateTime(2015, 10, 1), 1500000.0);
            _cashFlow.AddPenjualan(new DateTime(2015, 10, 2), 2000000.0);
            _cashFlow.AddPenjualanLain(new DateTime(2015, 10, 1), 1500000.0);
            _cashFlow.AddPenjualanLain(new DateTime(2015, 10, 2), 2000000.0);
            _cashFlow.ChangePengeluaran("Ayam", 1400000.0,1);
            _cashFlow.ChangePengeluaran("Ayam", 2400000.0,1);
            var snapshot = new CashFlowDto()
            {
                TenantId = "ABC",
                PeriodId = "2015101",
                SaldoAwal = 1000000.0,
                SaldoAkhir = 5600000.0,
                TotalPenjualan = 3500000.0,
                TotalPenjualanLain = 3500000.0,
                TotalPengeluaran = 2400000.0
            };
            var cashflow = new CashFlow(snapshot);
            var loadedSanpshot = cashflow.Snap();
            Assert.AreEqual(snapshot, loadedSanpshot);
        }

        [TestMethod]
        public void testCashFlowItems()
        {  
            NotaPengeluaran notaPengeluaran = new NotaPengeluaran(new DateTime(2015, 10, 26), "123");
            PeriodeId periodid = new PeriodeId("2015101");
            _cashFlow = new CashFlow("ABC", periodid, 1000000.0);
            _cashFlow.AddPenjualan(new DateTime(2015, 10, 1), 1500000.0);
            _cashFlow.AddPenjualan(new DateTime(2015, 10, 2), 2000000.0);
            notaPengeluaran.AddAkun("Ayam", 100000.0, 1);
            var cashFlowSnapshot = _cashFlow.Snap();
            var snapshot = new CashFlowDto()
            {
                TenantId = "ABC",
                PeriodId = "2015101",
                SaldoAwal = 1000000.0,
                SaldoAkhir = 4400000.0,
                TotalPenjualan = 3500000.0,
                TotalPenjualanLain = 0.0,
                TotalPengeluaran = 100000.0
            };
            Assert.AreEqual(snapshot, cashFlowSnapshot);
            Assert.AreEqual(1, snapshot.Items.Count);
            var itemDto=snapshot.Items[0];
            Assert.AreEqual("Ayam",itemDto.Akun);
            Assert.AreEqual(1, itemDto.Jumlah);
            Assert.AreEqual(100000.0, itemDto.Nominal);
        }
    }

}
