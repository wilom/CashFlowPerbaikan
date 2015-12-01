using dokuku.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku
{
    public class Periode : IPeriod
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
                return this._periodid.Snap();                        
            
        } 

        public PeriodeId PeriodId
        {
            get { return this._periodid; }
        }
      
    }
}
