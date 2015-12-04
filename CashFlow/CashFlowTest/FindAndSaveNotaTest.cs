using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dokuku.service;
using dokuku;
using dokuku.Dto;

namespace UnitTest
{
    [TestClass]
    public class FindAndSaveNotaTest
    {
        [TestMethod]
        public void TestBuatNota()
        {
            var repo = new InMemoryRepository();  
            var noNota = new NotaPengeluaranId("123");
            var notaPengeluaran = new NotaPengeluaran(new DateTime(2015, 11, 1), noNota);
            repo.SaveNota(notaPengeluaran);
            var foundNota = repo.FindNotaPengeluaranByID("123");
            //var notaSnapshot = notaPengeluaran.Snap();
            var notaSnap = new NotaPengeluaranDto()
            {
                Tanggal = new DateTime(2015, 11, 1),
                NoNota = noNota.Snap(),
                TotalNota = 0.0
            };
            Assert.AreEqual(notaSnap, notaPengeluaran.Snap());
        }
    }
}
