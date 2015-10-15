using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dokuku.CashFlow;
using dokuku;
using dokuku.ex;
using dokuku.Dto;

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
        public void TestDate()
        {
           
            DateTime a = new DateTime(2015,10,01);
            int Thn = a.Year;
            int Bln = a.Month;
            int Tgl = a.Day;
            string Tanggalan = ""+Thn+""+Bln+""+Tgl;
            var periodeSnapShot = _periode.Snap();
            var expected = new PeriodeDto() { Id = Tanggalan,IsPeriode=StatusPeriode.Mingguan};
            Assert.AreEqual(expected,periodeSnapShot);

        }
    }
}
