using dokuku.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku.service
{
    public class InMemoryRepository : IRepository
    {
        public void Save(global::Moq.Mock<CashFlowHead.CashFlow> cashFlowCreate)
        {
            throw new NotImplementedException();
        }       
    }
}
