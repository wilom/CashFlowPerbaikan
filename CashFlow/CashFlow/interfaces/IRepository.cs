using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku.interfaces
{
    public interface IRepository
    {
        object FindPeriodForDate(DateTime dateTime);

        object FindCashFlowByPeriod(string p);
    }
}
