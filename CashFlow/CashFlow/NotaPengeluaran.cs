using dokuku.exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku
{
    public class NotaPengeluaran
    {
        private DateTime _dateTime;
        private string _noNota;
        //private string _akun;
        //private double _nominal;
        private double _totalNota;
        IList<AkunPengeluaran> _itemsAkun = new List<AkunPengeluaran>();

        public NotaPengeluaran(DateTime dateTime, string noNota)
        {      
            this._dateTime = dateTime;
            this._noNota = noNota;
            CalculateNotaPengeluaran();
        }

        public Dto.NotaPengeluaranDto Snap()
        {
            return new Dto.NotaPengeluaranDto()
            {
                Tanggal = this._dateTime,
                NoNota = this._noNota,                
                TotalNota = this._totalNota
            };
        }

        public void AddAkun(string akun, double nominal)
        {
            var newAkun = new AkunPengeluaran(akun, nominal);
            //bool checkAkun = _itemsAkun.Where(x => x.Akun == akun).Count() == 0 ? true : false;
            //if (checkAkun)
            //{
                this._itemsAkun.Add(newAkun);
            //}
            //else 
            //{
            //    throw new AkunPengeluaranException();
            //}
            CalculateNotaPengeluaran();
        }
        private class AkunPengeluaran
        {
            private string _akun;
            private double _nominal;

            public AkunPengeluaran(string akun, double nominal)
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
        }

        private double CalculateItemNotaPengeluaran() 
        {
            return this._itemsAkun.Sum(x => x.Nominal);
        }

        public void CalculateNotaPengeluaran() 
        {
            this._totalNota = CalculateItemNotaPengeluaran();
        }

    }
}
