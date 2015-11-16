using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dokuku.CashFlowHead;
using Moq;
using dokuku.service;

namespace UnitTest
{
    [TestClass]
    public class FindAndSaveCashFlow
    {
        [TestMethod]
        public void testFindAndSaveCashFlowByPeriod()
        {          
            var factory = new MockRepository(MockBehavior.Loose);
            var cashFlowCreate = factory.Create<CashFlow>();
            cashFlowCreate.Setup(x => x.Snap()).Returns(cashflowSnapshot);
            var repo = new InMemoryRepository();
            repo.Save(cashFlowCreate);
            var cashFlow = repo.FindCashFlowByPeriod(periode);
            Assert.AreEquals(cashflowSnapshot,cashFlow.Snap());                   

        }
    }
}
