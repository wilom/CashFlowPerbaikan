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
        //private Periode _periodId;
        private double _saldoAwal;
        private double _saldoAkhir;
        private double _totalPenjualan;
        private double _totalPenjualanLain;
        private double _totalPengeluaran;
        private string _idPer;
        
       
        public CashFlow(string tenanId, string idPer, double saldoAwal, 
            double saldoAkhir, double totalPenjualan, double totalPenjualanLain, double totalPengeluaran)
        {
            // TODO: Complete member initialization
            this._tenanId = tenanId;
            this._idPer = idPer;
            this._saldoAwal = saldoAwal;
            this._saldoAkhir = saldoAkhir;
            this._totalPenjualan = totalPenjualan;
            this._totalPenjualanLain = totalPenjualanLain;
            this._totalPengeluaran = totalPengeluaran;
        }

       
        public Dto.CashFlowDto Snap()
        {
            return new Dto.CashFlowDto()
            {
                TenantId = this._tenanId,
                PeriodId = this._idPer,
                SaldoAwal = this._saldoAwal,
                SaldoAkhir = this._saldoAkhir,
                TotalPenjualan = this._totalPenjualan,
                TotalPenjualanLain = this._totalPenjualanLain,
                TotalPengeluaran = this._totalPengeluaran
            };
        }
    }
}
