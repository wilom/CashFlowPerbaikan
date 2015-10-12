using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dokuku.CashFlow;
using dokuku;
using dokuku.ex;

namespace CashFlowTest
{
    [TestClass]
    public class CashFlowTest
    {
        CashFlow _cashFlow=null;
        [TestInitialize]
        public void init() 
        { 
            Periode period =new Periode();
            _cashFlow = new CashFlow("ABC", period, 500000.0);

        }
        [TestMethod]
        public void TestMembukaCashflow()
        {
            var cashFlowSnapshot = _cashFlow.Snap();
        }
    }
}
