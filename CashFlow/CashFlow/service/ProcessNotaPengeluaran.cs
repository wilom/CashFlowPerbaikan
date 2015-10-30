using dokuku.CashFlowHead;
using dokuku.exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku
{
    public class ProcessNotaPengeluaran
    {
        public interfaces.IRepository Repository { get; set; }

        public void Process(interfaces.INotaPengeluaran notaPengeluaran)
        {
            var period = this.Repository.FindPeriodForDate(notaPengeluaran.Date);
            var cashFlow = this.Repository.FindCashFlowByPeriod(period.PeriodId);
            if (cashFlow == null)
            {
                
            }
            else 
            {
                var listAkun = notaPengeluaran.ListAkun();
                var listSummary = this.Repository.ListSummaryAkunIn(period,listAkun);
                foreach (var sumakun in listSummary)
                {
                    cashFlow.ChangePengeluaran(sumakun.PeriodId, sumakun.Nominal);
                }
                this.Repository.Save(cashFlow);
            }
        }
    }
}
