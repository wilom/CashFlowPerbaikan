using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku.Dto
{
    public class CashFlowDto
    {
        

        public string TenantId { get; set; }

        public string PeriodId { get; set; }

        public object SaldoAwal { get; set; }

        public double SaldoAkhir { get; set; }

        public double TotalPenjualan { get; set; }

        public double TotalPenjualanLain { get; set; }

        public double TotalPengeluaran { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is CashFlowDto)) return false;
            var cmp = (CashFlowDto)obj;
            return this.TenantId.Equals(cmp.TenantId) &&
                this.PeriodId.Equals(cmp.PeriodId)&&
                this.SaldoAwal.Equals(cmp.SaldoAwal) &&
                this.SaldoAkhir.Equals(cmp.SaldoAkhir) &&
                this.TotalPenjualan.Equals(cmp.TotalPenjualan) &&
                this.TotalPenjualanLain.Equals(cmp.TotalPenjualanLain) &&
                this.TotalPengeluaran.Equals(cmp.TotalPengeluaran);
        }
    }
}
