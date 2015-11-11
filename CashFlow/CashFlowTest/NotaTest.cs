using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dokuku;
using dokuku.Dto;
using dokuku.exceptions;

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
            _notaPengeluaran.AddAkun("Ayam", 100000.0, 1);
            var notaPengeluaranSnapshot = _notaPengeluaran.Snap();
            var expected = new NotaPengeluaranDto()
            {
                Tanggal = new DateTime(2015, 10, 26),
                NoNota = "123",                
                TotalNota = 100000.0                
            };
            Assert.AreEqual(expected, notaPengeluaranSnapshot);
            Assert.AreEqual(1, notaPengeluaranSnapshot.Items.Count);
            var entry1=notaPengeluaranSnapshot.Items[0];
            Assert.AreEqual("Ayam",entry1.Akun);
            Assert.AreEqual(1, entry1.Jumlah);
            Assert.AreEqual(100000.0, entry1.Nominal);
        }
        
        [TestMethod]
        [ExpectedException(typeof(AcountAllreadyExistException), "Akun Sudah Ada")]
        public void testTambahNotaPengeluaran() 
        {
            _notaPengeluaran.AddAkun("Ayam", 100000.0,1);
            _notaPengeluaran.AddAkun("Ayam", 200000.0,2);            
        }

        [TestMethod]
        public void testChangeNotaPengeluaran()
        {            
            _notaPengeluaran.AddAkun("Ayam", 100000.0, 1);
            _notaPengeluaran.ChangeNotaPengeluaran("Ayam", 100000.0, 1);
            _notaPengeluaran.ChangeNotaPengeluaran("Ayam", 200000.0, 2);
            var notaPengeluaranSnapshot = _notaPengeluaran.Snap();
            var expected = new NotaPengeluaranDto()
            {
                Tanggal = new DateTime(2015, 10, 26),
                NoNota = "123",
                TotalNota = 200000.0
            };
            Assert.AreEqual(expected, notaPengeluaranSnapshot);
            Assert.AreEqual(1, notaPengeluaranSnapshot.Items.Count);
            var entry1 = notaPengeluaranSnapshot.Items[0];
            Assert.AreEqual("Ayam", entry1.Akun);
            Assert.AreEqual(2, entry1.Jumlah);
            Assert.AreEqual(200000.0, entry1.Nominal);
        }
        
        [TestMethod]
        public void testSecenarioNotaPengeluaran()
        {
            _notaPengeluaran.AddAkun("Ayam", 100000.0, 1);
            _notaPengeluaran.AddAkun("Bebek", 200000.0, 1);
            _notaPengeluaran.AddAkun("Pembungkus", 50000, 3);
            var notaPengeluaranSnapshot = _notaPengeluaran.Snap();
            var expected = new NotaPengeluaranDto()
            {
                Tanggal = new DateTime(2015, 10, 26),
                NoNota = "123",
                TotalNota = 350000.0
            };
            Assert.AreEqual(expected, notaPengeluaranSnapshot);
            Assert.AreEqual(3, notaPengeluaranSnapshot.Items.Count);
            var entry1 = notaPengeluaranSnapshot.Items[0];
            var entry2 = notaPengeluaranSnapshot.Items[1];
            var entry3 = notaPengeluaranSnapshot.Items[2];

            Assert.AreEqual("Ayam", entry1.Akun);
            Assert.AreEqual(1, entry1.Jumlah);
            Assert.AreEqual(100000.0, entry1.Nominal);

            Assert.AreEqual("Bebek", entry2.Akun);
            Assert.AreEqual(1, entry2.Jumlah);
            Assert.AreEqual(200000.0, entry2.Nominal);

            Assert.AreEqual("Pembungkus", entry3.Akun);
            Assert.AreEqual(3, entry3.Jumlah);
            Assert.AreEqual(50000.0, entry3.Nominal);
        }
    }
}
