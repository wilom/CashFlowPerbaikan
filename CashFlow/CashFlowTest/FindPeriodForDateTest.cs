using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dokuku.Dto;
using dokuku.ex;
using dokuku.interfaces;
using dokuku.service;
using Moq;
using dokuku;

namespace UnitTest
{
    [TestClass]
    public class FindPeriodForDateTest
    {
        Periode _periode = null;
        PeriodeId _periodeId = null;
        [TestInitialize]
        public void init()
        {
            _periodeId = new PeriodeId(new DateTime(2015, 11, 1), new DateTime(2015, 11, 6));
            _periode = new Periode(_periodeId, StatusPeriode.Bebas);
        }
        [TestMethod]
        public void testBuatPeriode()
        {
            //DateTime tanggalPeriode = new DateTime(2015, 10, 1);
            //PeriodeId periodeId = new PeriodeId("2015101");
            //var periode =  new Periode(periodeId, StatusPeriode.Bebas);

            var periodeSnapShot = new PeriodeDto()
            {
                StartPeriode = new DateTime(2015, 11, 1),
                EndPeriode = new DateTime(2015, 11, 6)
            };
            Assert.AreEqual(periodeSnapShot, _periode.Snap());
        }

        [TestMethod]
        public void testSaveAndFindPeriode()
        {
            //var periodeId = new DateTime(2015,10,1);
            var periodeSnapShot = new PeriodeDto()
            {
                StartPeriode = new DateTime(2015, 11, 1),
                EndPeriode = new DateTime(2015, 11, 6)
            };

            var repo = new InMemoryRepository();
            var factory = new MockRepository(MockBehavior.Loose);           
            var periodeCrearte = factory.Create<IPeriod>();

            periodeCrearte.Setup(x => x.Snap()).Returns(periodeSnapShot);
            periodeCrearte.Setup(x => x.PeriodId).Returns(new PeriodeId(new DateTime(2015, 10, 1), new DateTime(2015, 10, 6)));

            repo.SavePeriod(periodeCrearte.Object);
            var periode = repo.FindPeriodForDate(new DateTime(2015, 10, 3));

            Assert.AreEqual(periodeSnapShot, periode.Snap()); ;
        }

        [TestMethod]
        public void testSavePeriode() 
        {
            //_periode.AddRentang(new DateTime(2015, 10, 01), new DateTime(2015, 10, 30), 29);            
            //var rentangSnapshot = _periode.Snap();
            //var periodeSnapShot = new PeriodeDto()
            //{
            //    StartPeriode = new DateTime(2015, 11, 1),
            //    EndPeriode = new DateTime(2015, 11, 6)            
            //};
            //Assert.AreEqual(1, rentangSnapshot.ItemsRentang.Count);
            //var entry1 = rentangSnapshot.ItemsRentang[0];
            //Assert.AreEqual(new DateTime(2015, 10, 01), entry1.StartPeriode);
            //Assert.AreEqual(new DateTime(2015, 10, 30), entry1.EndPeriode);
            //Assert.AreEqual(29, entry1.TotalRentang);
            //Assert.AreEqual(periodeSnapShot, _periode.Snap());           
        }
    }
}
