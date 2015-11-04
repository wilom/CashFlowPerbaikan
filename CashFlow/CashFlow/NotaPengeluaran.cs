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
        //IList<AkunPengeluaran> _itemsAkun = new List<AkunPengeluaran>();

        public NotaPengeluaran(DateTime dateTime, string noNota)
        {      
            this._dateTime = dateTime;
            this._noNota = noNota;           
        }

        public Dto.NotaPengeluaranDto Snap()
        {
            return new Dto.NotaPengeluaranDto()
            {
                Tanggal = this._dateTime,
                NoNota = this._noNota,
                //Akun = this._akun,
                //Nominal = this._nominal
            };
        }

        //public void AddAkun(string akun, double nominal)
        //{
        //    var newAkun = new AkunPengeluaran(akun, nominal);
        //    this._itemsAkun.Add(newAkun);
        //}
        //private class AkunPengeluaran 
        //{
        //    private string _akun;
        //    private double _nominal;

        //    public AkunPengeluaran(string akun, double nominal) 
        //    {
        //        this._akun = akun;
        //        this._nominal = nominal;
        //    }

        //    public string Akun
        //    {
        //        get
        //        {
        //            return this._akun;
        //        }
        //    }
        //    public double Nominal
        //    {
        //        get
        //        {
        //            return this._nominal;
        //        }
        //    }
        //}
    }
}
