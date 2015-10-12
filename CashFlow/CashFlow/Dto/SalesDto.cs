using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku.Dto
{
    public class SalesDto
    {
        public DateTime TglSales { get; set; }

        public double Nominal { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is SalesDto)) return false;
            var cmp = (SalesDto)obj;
            return this.TglSales.Equals(cmp.TglSales) &&
                this.Nominal.Equals(cmp.Nominal);
        }
    }
}
