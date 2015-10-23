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
        IList<Sales> _itemsSales = new List<Sales>();
        IList<Pengeluaran> _itemsPengeluaran = new List<Pengeluaran>();
        
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
            public DateTime Tanggal
            {
                get
                {
                    return this._dateTime;
                }
            }

        }

        public void AddSales(DateTime date, double nominal)
        {
            var newSales = new Sales(date, nominal);
            bool checktgl = _itemsSales.Where(x => x.Tanggal == date).Count() == 0 ? true : false;
            
            if (checktgl)
                this._itemsSales.Add(newSales);
            else
                throw new DateAlreadyExistException();
            Calculate();
        }

        private void Calculate()
        {
            this._totalPenjualan = CalculateSales();

            this._totalPengeluaran = CalculatePengeluaran();

            this._saldoAkhir = this._saldoAwal + this._totalPenjualan - this._totalPengeluaran;

        }
        private double CalculateSales()
        {
            return this._itemsSales.Sum(x => x.Nominal);
        }

        //muali pengeluaran-----------
        private class Pengeluaran 
        {
            private string _akun;
            private double _nominal;

            public Pengeluaran(string akun, double nominal)
            {
                this._akun = akun;
                this._nominal = nominal;
            }

            public string Akun 
            {
                get
                {
                return this._akun;
                }
            }
            public double Nominal
            {
                get
                {
                    return this._nominal;
                }
            }

            public void Change(double nominal) 
            {
                this._nominal = nominal;
            }
        }

        public void AddPengeluaran(string akun, double nominal) 
        {
            var newPengeluaran = new Pengeluaran(akun,nominal);
            
            var pengeluaran= this._itemsPengeluaran.Select(x => x.Akun == akun).FirstOrDefault();
            if (pengeluaran == false)
            {
                this._itemsPengeluaran.Add(new Pengeluaran(akun,nominal));
            }
            else
            {
                newPengeluaran.Change(nominal);
            }
            

            Calculate();  
        }
       
        private double CalculatePengeluaran()
        {
            return this._itemsPengeluaran.Sum(x => x.Nominal);
        }
    }

    
}
