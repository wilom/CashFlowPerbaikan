using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dokuku.CashFlow;

namespace CashFlowTest
{
    [TestClass]
    public class CashFlowTest
    {
        CashFlow _cashFlow=null;
        [TestInitialize]
        public void init() 
        { 
            string id = Guid.NewGuid();
            _cashFlow=new CashFlow(“ABC”,period,500000.0)

        }
        [TestMethod]
        public void TestMembukaCashflow()
        {
            var cashFlowSnapshot = _cashFlow.Snap();

            Assert.Equals(500000.0, cashFlowSnapshot.SaldoAwal);

           
        }
    }
}
