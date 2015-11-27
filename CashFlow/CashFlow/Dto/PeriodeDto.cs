using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku.Dto
{
    public class PeriodeDto
    {
        //public string PeriodeId { get; set; }
        public ex.StatusPeriode IsPeriode { get; set; }
        public DateTime StartPeriode { get; set; }
        public DateTime EndPeriode { get; set; }   
        public override bool Equals(object obj)
        {
            if (!(obj is PeriodeDto)) return false;
            var cmp = (PeriodeDto)obj;
            return this.StartPeriode.Equals(cmp.StartPeriode) &&
                   this.EndPeriode.Equals(cmp.EndPeriode);
        }     
                
        
    }
}
