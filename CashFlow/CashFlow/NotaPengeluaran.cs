using dokuku.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku
{
    public class NotaPengeluaran
    {
        private string _noNota;
        private DateTime _tglNota;
        private string _cabang;
        private IList<ItemNota> _items = new List<ItemNota>();
        private double _totalNota;

        public NotaPengeluaran(string NoNota, DateTime TglNota, string Cabang, double TotalNota)
        {
            // TODO: Complete member initialization
            this._noNota = NoNota;
            this._tglNota = TglNota;
            this._cabang = Cabang;
            this._totalNota = TotalNota;
        }

        public Dto.NotaPengeluaranDto SnapShot()
        {
            return new Dto.NotaPengeluaranDto()
            {
                NoNota = this._noNota,
                TglNota = this._tglNota,
                Cabang = this._cabang,
                TotalNota = this._totalNota,
                Items = SetItemInNotaPengeluaran(this._items)
            };
        }

        private IList<ItemsDto> SetItemInNotaPengeluaran(IList<ItemNota> list)
        {
            IList<ItemsDto> items = new List<ItemsDto>();
            foreach (ItemNota item in list)
            {
                items.Add(item.SetSnapShot(item));
            }
            return items;
        }
        public void MasukItem(ItemNota items)
        {
            this._items.Add(items);
        }

        public void EditNoNota(string noNota)
        {
            this._noNota = noNota;
        }

        public string GetNoNota()
        {
            return this._noNota;
        }

        public void InTotalNota(double totalNota)
        {
            this._totalNota = totalNota;
        }

        public double  GetTotalNota()
        {
            return this._totalNota;
        }
    }
}
