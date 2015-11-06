using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dokuku;
using dokuku.Dto;

namespace UnitTest
{
    [TestClass]
    public class NotaTest
    {
        NotaPengeluaran _notaPengeluaran = null;
        [TestInitialize]
        public void init()
        {
            
            _notaPengeluaran = new NotaPengeluaran(new DateTime(2015, 10, 26), "123");

        }
        [TestMethod]

        public void testMembuatNotaPengeluaran()
        {

            var notaPengeluaranSnapshot = _notaPengeluaran.Snap();
            var expected = new NotaPengeluaranDto()
            {
                Tanggal = new DateTime(2015, 10, 26),
                NoNota = "123",               
                TotalNota = 0.0
            };
            Assert.AreEqual(expected, notaPengeluaranSnapshot);
        }

        [TestMethod]

        public void testAddAkunNotaPengeluaran()
        {

            _notaPengeluaran.AddAkun("Ayam", 100000.0,1);
            var notaPengeluaranSnapshot = _notaPengeluaran.Snap();
            var expected = new NotaPengeluaranDto()
            {
                Tanggal = new DateTime(2015, 10, 26),
                NoNota = "123",                
                TotalNota = 100000.0                
            };
            Assert.AreEqual(expected, notaPengeluaranSnapshot);
            Assert.AreEqual(1, notaPengeluaranSnapshot.Items.Count);
            var entry1=notaPengeluaranSnapshot.Items[1];
            Assert.AreEqual("Ayam",entry1.Akun);
            Assert.AreEqual(100000.0, entry1.Nominal);
            Assert.AreEqual(1, entry1.Jumlah);

        }

        [TestMethod]
        public void testTambahNotaPengeluaran() 
        {
            _notaPengeluaran.AddAkun("Ayam", 100000.0,1);
            _notaPengeluaran.AddAkun("Ayam", 200000.0,2);            
        }
    }
}
