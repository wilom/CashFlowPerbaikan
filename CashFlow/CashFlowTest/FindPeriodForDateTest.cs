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
        [TestMethod]
        public void testBuatPeriode()
        {
            DateTime tanggalPeriode = new DateTime(2015, 10, 1);
            PeriodeId periodeId = new PeriodeId("2015101");
            var periode =  new Periode(periodeId, StatusPeriode.Bebas);

            var periodeSnapShot = new PeriodeDto()
            {
                PeriodeId = "2015101",
                IsPeriode = StatusPeriode.Bebas
            };
            Assert.AreEqual(periodeSnapShot, periode.Snap());
        }
        [TestMethod]
        public void testSaveAndFindPeriode()
        {
            var periodeId = new DateTime(2015,10,1);
            var periodeSnapShot = new PeriodeDto()
            {
                PeriodeId = "2015101",
                IsPeriode = StatusPeriode.Bebas
            };

            var repo = new InMemoryRepository();
            var factory = new MockRepository(MockBehavior.Loose);
            
            var periodeCrearte = factory.Create<IPeriod>();
            periodeCrearte.Setup(x => x.Snap()).Returns(periodeSnapShot);
            periodeCrearte.Setup(x => x.GenerateId()).Returns(new PeriodeId(periodeId.ToString()));

            repo.SavePeriod(periodeCrearte.Object);
            var periode = repo.FindPeriodForDate(periodeId);
            
            Assert.AreEqual(periodeSnapShot, periode.Snap());
        }
    }
}
