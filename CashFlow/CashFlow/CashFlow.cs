using dokuku.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku.CashFlow
{
    public class CashFlow
    {
        private string _tenanId;
        private PeriodeId _periodId;
        private double _saldoAwal;
        private double _saldoAkhir;
        private double _totalPenjualan;
        private double _totalPenjualanLain;
        private double _totalPengeluaran;
        //private string _idPer;

        public void Calculate() 
        {
            this._saldoAkhir = this._saldoAwal+this._totalPenjualan;
            //this._totalPenjualan = this._totalPenjualanLain;
            //this._totalPengeluaran = 0;
        }
        
        public CashFlow(string tenanId, PeriodeId periodId, double saldoAwal) 
        {
            this._tenanId = tenanId;
            this._periodId = periodId;
            this._saldoAwal = saldoAwal;
            Calculate();
            
        }
       
        public Dto.CashFlowDto Snap()
        {
            return new Dto.CashFlowDto()
            {
                TenantId = this._tenanId,
                PeriodId = this._periodId.ToString(),
                SaldoAwal = this._saldoAwal,
                SaldoAkhir = this._saldoAkhir,
                TotalPenjualan = this._totalPenjualan,
                TotalPenjualanLain = this._totalPenjualanLain,
                TotalPengeluaran = this._totalPengeluaran
            };
        }
    }
}
