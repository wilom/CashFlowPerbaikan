using dokuku.exceptions;
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
        private Dictionary<PeriodeId, IPeriod> _periodeDb = new Dictionary<PeriodeId, IPeriod>();
        private Dictionary<NotaPengeluaranId, INotaPengeluaran> _notaDb = new Dictionary<NotaPengeluaranId, INotaPengeluaran>(); 
        public IPeriod FindPeriodForDate(DateTime date)
        {
            var key = this._periodeDb.Keys.Where(x => x.IsInPeriod(date)).FirstOrDefault();
            if (key == null) return null;
            return this._periodeDb[key];
        }

        public ICashFlow FindCashFlowByPeriod(PeriodeId periodId)
        {            
                var key = new CashFlowId(periodId);
                return this._cashFlowDb[key];          
        }

        public INotaPengeluaran FindNotaPengeluaranByID(string noNota)
        {
            var key = new NotaPengeluaranId(noNota);
            return this._notaDb[key];
        }
        public IList<Dto.SummaryAkunDto> ListSummaryAkunIn(IPeriod period, string[] listAkun)
        {
            throw new NotImplementedException();
           
        }
        public void Save(ICashFlow cashFlow)
        {
            if (!this._cashFlowDb.ContainsKey(cashFlow.GenerateId()))
                this._cashFlowDb.Add(cashFlow.GenerateId(), cashFlow);
            this._cashFlowDb[cashFlow.GenerateId()] = cashFlow; 
        }

        public void SavePeriod(IPeriod period)
        {
            if (!this._periodeDb.ContainsKey(period.PeriodId))
                this._periodeDb.Add(period.PeriodId, period);
            this._periodeDb[period.PeriodId] = period;
        }

        public void SaveNota(INotaPengeluaran notaPengleuaran)
        {
            if (!this._notaDb.ContainsKey(notaPengleuaran.NoNota()))
                this._notaDb.Add(notaPengleuaran.NoNota(), notaPengleuaran);
            this._notaDb[notaPengleuaran.NoNota()] = notaPengleuaran;
        }
                
       
    }
}
