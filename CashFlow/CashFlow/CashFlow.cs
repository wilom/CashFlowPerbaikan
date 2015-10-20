using dokuku.Dto;
using dokuku.exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku.CashFlowHead
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
        IList<Sales> _items = new List<Sales>();
        
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

        private class Sales
        {
            private DateTime _dateTime;
            private double _nominal;
            public Sales(DateTime date, double nominal)
            {
                this._dateTime = date;
                this._nominal = nominal;
            }

            public double Nominal
            {
                get
                {
                    return this._nominal;
                }
            }

        }

        public void AddSales(DateTime tgl, double nominal)
        {
            throw new DateAlreadyExistException();

            var newSales = new Sales(tgl, nominal);
            this._items.Add(newSales);
            Calculate();
        }

        private void Calculate()
        {
            var totalSales = CalculateSales();
            this._totalPenjualan = totalSales;

            this._saldoAkhir = this._saldoAwal + this._totalPenjualan;

        }
        private double CalculateSales()
        {
            return this._items.Sum(x => x.Nominal);
        }

    }

    
}
