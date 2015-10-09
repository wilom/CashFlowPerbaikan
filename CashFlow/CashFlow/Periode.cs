using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow
{
    public class Periode
    {
        private string _id;
        private string _cabang;
        private ex.StatusPeriode _statusPeriode;

        public Periode(string id, string cabang, ex.StatusPeriode statusPeriode)
        {
            // TODO: Complete member initialization
            this._id = id;
            this._cabang = cabang;
            this._statusPeriode = statusPeriode;
        }

        public Dto.PeriodeDto SnapShot()
        {
            return new Dto.PeriodeDto()
            {
                Id = this._id,
                Cabang = this._cabang,
                Status = this._statusPeriode,
            };
        
        }
    }
}
