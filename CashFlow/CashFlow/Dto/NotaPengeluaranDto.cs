using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku.Dto
{
    public class NotaPengeluaranDto
    {


        public string NoNota { get; set; }

        public DateTime TglNota { get; set; }

        public string Cabang { get; set; }

        public double TotalNota { get; set; }

        public IList<ItemsDto> Items { get; set; }
    }
}
