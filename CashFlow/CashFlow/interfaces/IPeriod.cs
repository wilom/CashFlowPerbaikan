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
        PeriodeId PeriodId{ get; }

        PeriodeDto Snap();        
        
    }
}
