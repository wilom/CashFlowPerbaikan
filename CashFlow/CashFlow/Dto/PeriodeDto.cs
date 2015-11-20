using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku.Dto
{
    public class PeriodeDto
    {
        public string PeriodeId { get; set; }
        public ex.StatusPeriode IsPeriode { get; set; }
        public List<ItemRentangDto> ItemsRentang { get; set; }      
        public override bool Equals(object obj)
        {
            if (!(obj is PeriodeDto)) return false;
            var cmp = (PeriodeDto)obj;
            return this.PeriodeId.Equals(cmp.PeriodeId) &&
                   this.IsPeriode.Equals(cmp.IsPeriode);
        }
        
        public class ItemRentangDto
        {
            public DateTime StartPeriode { get; set; }
            public DateTime EndPeriode { get; set; }
        }
    }
}
