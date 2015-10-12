using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku
{
    public class SalesLain
    {
        private string _idSalesLain;
        private DateTime _tglSalesLain;
        private double _nominal;
        private string _keterangan;

        public SalesLain(string idSales, DateTime tglSalesLain, double nominal, string keterangan)
        {

            this._idSalesLain = idSales;
            this._tglSalesLain = tglSalesLain;
            this._nominal = nominal;
            this._keterangan = keterangan;
        }


        public Dto.SalesLainDto SnapShot()
        {
            return new Dto.SalesLainDto()
            {
                IdSalesLain = this._idSalesLain,
                TglSalesLain = this._tglSalesLain,
                Nominal = this._nominal,
                Keterangan = this._keterangan

            };
        }

        public void InTotalSalesLain(double nominal) {
            this._nominal = nominal;
        }
        public double GetTotalSalesLain()
        {
            return this._nominal;
        }
    }
}
