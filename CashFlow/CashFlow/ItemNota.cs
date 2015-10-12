using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow
{
    public class ItemNota
    {
        private string _akun;
        private double _nominal;

        public ItemNota(string akun, double nominal)
        {
            // TODO: Complete member initialization
            this._akun = akun;
            this._nominal = nominal;
        }

        public Dto.ItemsDto SnapShot()
        {
            return new Dto.ItemsDto()
            {
                Akun = this._akun,
                Nominal = this._nominal
            };
        }

        public Dto.ItemsDto SetSnapShot(ItemNota item)
        {
            return new Dto.ItemsDto() { Akun = item._akun, Nominal = item._nominal };
        }
    }
}
