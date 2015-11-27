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

        //public int CalculateRentangAll()
        //{
            
        //    DateTime intStart = rentang.StartPeriode;
        //    DateTime intEnd = rentang.StartPeriode;
        //    return this._totalRentang = Convert.ToInt32(intStart)+Convert.ToInt32(intEnd);
        //}

        //public IEnumerable<DateTime> Range()
        //{
        //    return Enumerable.Range(0, (_endPeriode - _startPeriode).Days + 1).Select(d => _startPeriode.AddDays(d));
        //}

        //public object StartPeriode(DateTime startPeriode)
        //{
        //    return this._startPeriode = startPeriode;
        //}
        //public object EndPeriode(DateTime endPeriode)
        //{
        //    return this._endPeriode = endPeriode;
        //}
        
        public string PeriodId
        {
            get { throw new NotImplementedException(); }
        }

        public PeriodeId GenerateId()
        {
            throw new NotImplementedException();
        }
    }
}
