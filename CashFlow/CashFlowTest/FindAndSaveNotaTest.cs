using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using dokuku.service;
using dokuku;
using dokuku.Dto;
using dokuku.interfaces;

namespace UnitTest
{
    [TestClass]
    public class FindAndSaveNotaTest
    {
        MockRepository _factory = new MockRepository(MockBehavior.Loose);
        InMemoryRepository _repo;
        NotaPengeluaranId _noNota = new NotaPengeluaranId("123");
        NotaPengeluaranDto _notaSnap = null;

        [TestInitialize]
        public void Init()
        {
            _repo = new InMemoryRepository();
            _notaSnap = new NotaPengeluaranDto()
            {
                Tanggal = new DateTime(2015, 11, 1),
                NoNota = "123",
                TotalNota = 0.0
            };
            var NotaCreate = _factory.Create<INotaPengeluaran>();
            NotaCreate.Setup(x => x.Snap()).Returns(_notaSnap);
            NotaCreate.Setup(x => x.NoNota()).Returns(new NotaPengeluaranId("123"));

            _repo.SaveNota(NotaCreate.Object);
        }
        [TestMethod]
        public void TestBuatNota()
        {
            var foundNota = _repo.FindNotaPengeluaranByID("123");
            Assert.AreEqual(_notaSnap, foundNota.Snap());

        }
    }
}
