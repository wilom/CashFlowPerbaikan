using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku.CashFlow
{
    
    public class CashFlowEntry
    {
        private string _id;
        private string _cabang;
        private double _saldoAwal;
        private double _sadoAkhir;
        private double _totalSales;
        private double _totalSalesLain;
        private double _totalNota;

        public CashFlowEntry(string id, string cabang, double saldoAwal, 
            double saldoAkhir, double totalSales, double totalSalesLain, double totalNota)
        {
            // TODO: Complete member initialization
            this._id = id;
            this._cabang = cabang;
            this._saldoAwal = saldoAwal;
            this._sadoAkhir = saldoAkhir;
            this._totalSales = totalSales;
            this._totalSalesLain = totalSalesLain;
            this._totalNota = totalNota;
        }

        public Dto.CashFlowEntryDto SnapShot()
        {
            return new Dto.CashFlowEntryDto()
            {
                Id = this._id,
                Cabang = this._cabang,
                SaldoAwal = this._saldoAwal,
                SaldoAkhir = this._sadoAkhir,
                TotalSales = this._totalSales,
                TotalSaleslain = this._totalSalesLain,
                TotalNota = this._totalNota
            };
        }
    }
}
