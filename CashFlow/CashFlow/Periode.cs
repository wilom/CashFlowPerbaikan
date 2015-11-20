using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku
{
    public class Periode
    {
        private PeriodeId _periodid;
        private ex.StatusPeriode _statusPeriode;
        //private DateTime _startPeriode;
        //private DateTime _endPeriode;
        List<Rentang> _itemsRentang = new List<Rentang>();

        public Periode(PeriodeId periodid, ex.StatusPeriode statusPeriode)
        {            
            this._periodid = periodid;
            this._statusPeriode = statusPeriode;                
        }

        public Dto.PeriodeDto Snap()
        {
            return new Dto.PeriodeDto() 
            { 
                PeriodeId = this._periodid.ToString(),
                IsPeriode = this._statusPeriode,
                ItemsRentang = SetToItems()
            };
        }
        private List<dokuku.Dto.PeriodeDto.ItemRentangDto> SetToItems()
        {
            return this._itemsRentang.Select(x => x.SnapRentang()).ToList();
        }
        
        public void AddRentang(DateTime startPeriode, DateTime endPeriode)
        {
            var newrentang = new Rentang(startPeriode, endPeriode);
            this._itemsRentang.Add(newrentang);
        }
        public class Rentang 
        {
            private DateTime _startPeriode;
            private DateTime _endPeriode;
            public Rentang(DateTime startPeriode, DateTime endPeriode)
            {
                this._startPeriode = startPeriode;
                this._endPeriode = endPeriode;
            }
            public dokuku.Dto.PeriodeDto.ItemRentangDto SnapRentang()
            {
                return new dokuku.Dto.PeriodeDto.ItemRentangDto()
                {
                    StartPeriode = this._startPeriode,
                    EndPeriode = this._endPeriode
                };
            }            
        }


        //public IEnumerable<DateTime> Range()
        //{
        //    return Enumerable.Range(0, (_endPeriode - _startPeriode).Days + 1).Select(d => _startPeriode.AddDays(d));
        //}

        //public object StartPeriode(DateTime startPeriode)
        //{
        //    return this._startPeriode = startPeriode;
        //}
        //public object EndPeriode(DateTime endPeriode)
        //{
        //    return this._endPeriode = endPeriode;
        //}
    }
}
