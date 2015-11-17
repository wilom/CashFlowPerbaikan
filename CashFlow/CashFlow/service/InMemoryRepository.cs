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
        private Dictionary<CashFlowId, ICashFlow> _cashFlowDb = new Dictionary<CashFlowId, ICashFlow>();
        public IPeriod FindPeriodForDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public ICashFlow FindCashFlowByPeriod(string periodId)
        {
            var key = new CashFlowId(periodId);
            return this._cashFlowDb[key];

        }
        public IList<Dto.SummaryAkunDto> ListSummaryAkunIn(IPeriod period, string[] listAkun)
        {
            throw new NotImplementedException();
           
        }
        public void Save(ICashFlow cashFlow)
        {
            //throw new NotImplementedException();
            _cashFlowDb.Add(cashFlow.GenerateId(), cashFlow);          

        }
              
    }
}
