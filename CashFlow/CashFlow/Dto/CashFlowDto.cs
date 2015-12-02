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
        public PeriodeDto PeriodId { get; set; }       
        public double SaldoAwal { get; set; }
        public double SaldoAkhir { get; set; }
        public double TotalPenjualan { get; set; }
        public double TotalPenjualanLain { get; set; }
        public double TotalPengeluaran { get; set; }
        public List<ItemsPenjualanDto> ItemsPenjualan { get; set; }
        public List<ItemsPenjualanLainDto> ItemsPenjualanLain { get; set; }
        public List<ItemsPengeluaranDto> ItemsPengeluaran { get; set; }

        //public List<dokuku.CashFlowHead.CashFlow.Penjualan> ListPenjualan  { get; set; }

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

        public class ItemsPenjualanDto
        {
            public DateTime DateTime { get; set; }
            public double Nominal { get; set; }
        }
        public class ItemsPenjualanLainDto
        {
            public DateTime DateTimeLain { get; set; }
            public double NominalLain { get; set; }
        }
        public class ItemsPengeluaranDto
        {
            public string Akun { get; set; }
            public double Nominal { get; set; }
            public int Jumlah { get; set; }
        }
    }
}
