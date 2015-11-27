using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku
{
    public class PeriodeId
    {
        //private string _periodeId;
        private DateTime _startPeriode;
        private DateTime _endPeriode;
        /*public PeriodeId(string periodeId)
        {
            this._periodeId = periodeId;
        }*/

        public PeriodeId(DateTime startPeriode, DateTime endPeriode)
        {         
            this._startPeriode = startPeriode;
            this._endPeriode = endPeriode;
        }

        /*public override string ToString()
        {
            return this._periodeId;
        }*/
        public override bool Equals(object obj)
        {
            if (!(obj is PeriodeId)) return false;
            var cmp = (PeriodeId)obj;
            return this._startPeriode.Equals(cmp._startPeriode) && 
                    this._endPeriode.Equals(cmp._endPeriode);
        }
        public override int GetHashCode()
        {
            return this._startPeriode.GetHashCode() + this._endPeriode.GetHashCode();
        }

        public bool IsInPeriod(DateTime date)
        {
            return date >= this._startPeriode && date <= this._endPeriode;
        }
        internal Dto.PeriodeDto Snap()
        {
            return new Dto.PeriodeDto() { StartPeriode = this._startPeriode, EndPeriode = this._endPeriode };
        }

    }
}
