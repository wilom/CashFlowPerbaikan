using dokuku.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku.interfaces
{
    public interface IPeriod
    {
        string PeriodId{ get; }

        PeriodeDto Snap();

        PeriodeId GenerateId();
    }
}
