using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku.interfaces
{
    public interface IRepository
    {
        object FindPeriodForDate(DateTime date);

        object FindCashFlowByPeriod(string periodId);

        object ListSummaryAkunIn(IPeriod period, string[] listAkun);

        object Save(ICashFlow cashFlow);

        object ListSummaryAkunIn(object period, object listAkun);

        void Save(object cashFlow);
    }
}
