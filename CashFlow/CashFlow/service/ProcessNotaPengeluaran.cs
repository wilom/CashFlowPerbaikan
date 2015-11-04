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
            if (period == null) 
            {
                throw new PeriodeNotFoundException();
            }
            var cashFlow = this.Repository.FindCashFlowByPeriod(period.PeriodId);
            if (cashFlow == null)
            {
                throw new CashflowNotFoundException();
            }
            else 
            {
                var listAkun = notaPengeluaran.ListAkun();
                var listSummary = this.Repository.ListSummaryAkunIn(period,listAkun);
                foreach (var sumakun in listSummary)
                {
                    cashFlow.ChangePengeluaran(sumakun.Akun, sumakun.Nominal);
                }
                this.Repository.Save(cashFlow);
            }
        }
    }
}
