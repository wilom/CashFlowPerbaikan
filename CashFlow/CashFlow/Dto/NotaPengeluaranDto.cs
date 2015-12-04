using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku.Dto
{
    public class NotaPengeluaranDto
    {
        public DateTime Tanggal { get; set; }

        public string NoNota { get; set; }

        public double TotalNota { get; set; }

        public List<ItemNotaDto> Items { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is NotaPengeluaranDto)) return false;
            var cmp = (NotaPengeluaranDto)obj;
            return this.Tanggal.Equals(cmp.Tanggal) &&
                this.NoNota.Equals(cmp.NoNota) &&
                this.TotalNota.Equals(cmp.TotalNota); 
        }

        public class ItemNotaDto
        {

            public string Akun { get; set; }

            public int Jumlah { get; set; }

            public double Nominal { get; set; }
        }

    }

}
