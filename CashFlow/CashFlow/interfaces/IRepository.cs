using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku.interfaces
{
    public interface IRepository
    {
        IPeriod FindPeriodForDate(DateTime date);

        ICashFlow FindCashFlowByPeriod(PeriodeId periodId);

        IList<Dto.SummaryAkunDto> ListSummaryAkunIn(IPeriod period, string[] listAkun);
  
        void Save(ICashFlow cashFlow);


       
    }
}
