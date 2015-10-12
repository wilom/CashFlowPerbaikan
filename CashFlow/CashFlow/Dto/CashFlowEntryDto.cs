using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku.Dto
{
    public class CashFlowEntryDto
    {
        public string Id { get; set; }

        public string Cabang { get; set; }

        public double SaldoAwal { get; set; }

        public double SaldoAkhir { get; set; }

        public double TotalSales { get; set; }

        public double TotalSaleslain { get; set; }

        public double TotalNota { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is CashFlowEntryDto)) return false;
            var cmp = (CashFlowEntryDto)obj;
            return this.Id.Equals(cmp.Id) &&
                this.Cabang.Equals(cmp.Cabang)&&
                this.SaldoAwal.Equals(cmp.SaldoAwal)&&
                this.SaldoAkhir.Equals(cmp.SaldoAkhir)&&
                this.TotalSales.Equals(cmp.TotalSales)&&
                this.TotalSaleslain.Equals(cmp.TotalSaleslain)&&
                this.TotalNota.Equals(cmp.TotalNota);
        }
    }
   
}
