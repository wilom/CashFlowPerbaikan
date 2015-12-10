using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dokuku
{
    public class NotaPengeluaranId
    {
        private string _noNota;
        //private NotaPengeluaranId _noNota = null;

        public NotaPengeluaranId(string noNota)
        {
            this._noNota = noNota;
        }

        //public NotaPengeluaranId(NotaPengeluaranId noNota)
        //{
        //    // TODO: Complete member initialization
        //    this._noNota = noNota;
        //}       
        public override bool Equals(object obj)
        {
            if (!(obj is NotaPengeluaranId)) return false;
            var cmp = (NotaPengeluaranId)obj;
            return this._noNota.Equals(cmp._noNota);
        }
        
        public override int GetHashCode()
        {
            return this._noNota.GetHashCode();
        }
    }
}
