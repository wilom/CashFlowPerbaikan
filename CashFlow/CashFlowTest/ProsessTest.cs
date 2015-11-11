using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using dokuku.Dto;
using Moq;
using dokuku.interfaces;
using dokuku;
using dokuku.exceptions;

namespace UnitTest
{
    [TestClass]
    public class ProsessTest
    {
        [TestMethod]
        public void testProsesNotaPengeluaran()
        {
            var transactionDate = new DateTime(2015, 10, 26);
            var periodId = "20151104";
            var listAkun = new string[] { "Ayam" };
            var listSummaryAkun = new List<SummaryAkunDto>() 
            {
            new SummaryAkunDto(){ PeriodId = "20151104",Akun ="Ayam", Nominal=600000.0}
            };
            var factory = new MockRepository(MockBehavior.Loose);
            var mockRepository = factory.Create<IRepository>();
            var mockCashFlow = factory.Create<ICashFlow>();
            var mockPengeluaran = factory.Create<INotaPengeluaran>();
            var mockCurrentPeriod = factory.Create<IPeriod>();
            mockRepository.Setup(t => t.FindPeriodForDate(transactionDate)).Returns(mockCurrentPeriod.Object);
            mockCurrentPeriod.SetupGet(t => t.PeriodId).Returns(periodId);
            mockRepository.Setup(t => t.FindCashFlowByPeriod(periodId)).Returns(mockCashFlow.Object);
            mockPengeluaran.SetupGet(t => t.Date).Returns(transactionDate);
            mockPengeluaran.Setup(t => t.ListAkun()).Returns(listAkun);
            mockRepository.Setup(t => t.ListSummaryAkunIn(mockCurrentPeriod.Object, listAkun)).Returns(listSummaryAkun);
            mockCashFlow.Setup(t => t.ChangePengeluaran(It.IsAny<string>(), It.IsAny<double>()));
            mockRepository.Setup(t => t.Save(mockCashFlow.Object));

            var service = new ProcessNotaPengeluaran();
            service.Repository = mockRepository.Object;
            service.Process(mockPengeluaran.Object);
            factory.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(CashflowNotFoundException), "CashFlow Tidak Ditemukan")]
        public void testCashFlowTidakDitemukanDiReposytory()
        {
            var transactionDate = new DateTime(2015, 10, 26);
            var periodId = "20151104";

            var factory = new MockRepository(MockBehavior.Loose);
            var mockRepository = factory.Create<IRepository>();

            var mockPengeluaran = factory.Create<INotaPengeluaran>();
            var mockCurrentPeriod = factory.Create<IPeriod>();
            mockRepository.Setup(t => t.FindPeriodForDate(transactionDate)).Returns(mockCurrentPeriod.Object);
            mockCurrentPeriod.SetupGet(t => t.PeriodId).Returns(periodId);
            mockPengeluaran.SetupGet(t => t.Date).Returns(transactionDate);

            var service = new ProcessNotaPengeluaran();
            service.Repository = mockRepository.Object;
            service.Process(mockPengeluaran.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(PeriodeNotFoundException), "Periode Tidak Ditemukan")]
        public void testPeriodeTidakDitemukanDiRepository()
        {
            var transactionDate = new DateTime(2015, 10, 26);
            var periodId = "20151104";

            var factory = new MockRepository(MockBehavior.Loose);
            var mockRepository = factory.Create<IRepository>();

            var mockPengeluaran = factory.Create<INotaPengeluaran>();
            var mockCashFlow = factory.Create<ICashFlow>();
            mockRepository.Setup(t => t.FindCashFlowByPeriod(periodId)).Returns(mockCashFlow.Object);
            mockPengeluaran.SetupGet(t => t.Date).Returns(transactionDate);

            var service = new ProcessNotaPengeluaran();
            service.Repository = mockRepository.Object;
            service.Process(mockPengeluaran.Object);
        }
    }
}
