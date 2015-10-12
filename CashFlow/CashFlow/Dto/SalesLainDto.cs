using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku.Dto
{
    public class SalesLainDto
    {
        public string IdSalesLain { get; set; }

        public DateTime TglSalesLain { get; set; }

        public double Nominal { get; set; }

        public string Keterangan { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is SalesLainDto)) return false;
            var cmp = (SalesLainDto)obj;
            return this.IdSalesLain.Equals(cmp.IdSalesLain) &&
                this.TglSalesLain.Equals(cmp.TglSalesLain) &&
                this.Nominal.Equals(cmp.Nominal) &&
                this.Keterangan.Equals(cmp.Keterangan);
        }

    }
}
