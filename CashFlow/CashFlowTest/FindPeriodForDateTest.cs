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
        public void testSaveAndFindPeriode()
        {
            string periodeId = "2015101";
            var periodeSnapShot = new PeriodeDto()
            {
                PeriodeId = "2015101",
                IsPeriode = StatusPeriode.Bebas
            };

            var repo = new InMemoryRepository();
            var factory = new MockRepository(MockBehavior.Loose);
            
            var periodeCrearte = factory.Create<IPeriod>();
            periodeCrearte.Setup(x => x.Snap()).Returns(periodeSnapShot);
            periodeCrearte.Setup(x => x.GenerateId()).Returns(new PeriodeId(periodeId));

            repo.SavePeriod(periodeCrearte.Object);
            var periode = repo.FindCashFlowByPeriod(periodeId);
            
            Assert.AreEqual(periodeSnapShot, periode.Snap());
        }
    }
}
