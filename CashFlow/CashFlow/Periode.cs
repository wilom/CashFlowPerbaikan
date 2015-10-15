using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku
{
    public class Periode
    {
        private PeriodeId _periodid;
        private ex.StatusPeriode _statusPeriode;

        public Periode(PeriodeId periodid, ex.StatusPeriode statusPeriode)
        {
            
            this._periodid = periodid;
            this._statusPeriode = statusPeriode;
        }

        public Dto.PeriodeDto Snap()
        {
            return new Dto.PeriodeDto() 
            { 
                Id = this._periodid.ToString(),
                IsPeriode = this._statusPeriode
            };
        }
    }
}
