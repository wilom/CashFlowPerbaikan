using dokuku.Dto;
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
                TotalNota = this._totalNota,
                Items = SetToItems()
            };
        }

        private IList<Items> SetToItems()
        {
            //return this._itemsAkun.Select(x => x.SnapAkun());


            IList<Items> items = new List<Items>();
            foreach (var item in this._itemsAkun)
            {
                Items result = new Items()
                {
                    Akun = item.Akun,
                    Jumlah = item.Jumalah,
                    Nominal = item.Nominal
                };
                items.Add(result);
            }
            return items;
        }

        public void AddAkun(string akun, double nominal, int jumlah)
        {
            var newAkun = new AkunPengeluaran(akun, nominal, jumlah);
          
            this._itemsAkun.Add(newAkun);
          
            CalculateNotaPengeluaran();
        }
        private class AkunPengeluaran
        {
            private string _akun;
            private double _nominal;
            private int _jumlah;

            public AkunPengeluaran(string akun, double nominal, int jumlah)
            {
                this._akun = akun;
                this._nominal = nominal;
                this._jumlah = jumlah;
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
            public int Jumalah
            {
                get
                {
                    return this._jumlah;
                }
            }

            public Items SnapAkun()
            {
                return new Items() 
                {
                    Akun = this._akun,
                    Jumlah = this._jumlah,
                    Nominal = this._nominal
                };
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
