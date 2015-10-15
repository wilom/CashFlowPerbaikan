using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku.Dto
{
    public class PeriodeDto
    {
        public string Id { get; set; }

        public ex.StatusPeriode IsPeriode { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is PeriodeDto)) return false;
            var cmp = (PeriodeDto)obj;
            return this.Id.Equals(cmp.Id) &&
                   this.IsPeriode.Equals(cmp.IsPeriode);

        }
    }
}
