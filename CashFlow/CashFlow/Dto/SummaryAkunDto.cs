using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku.Dto
{
    public class SummaryAkunDto
    {
        public string PeriodId { get; set; }

        public string Akun { get; set; }

        public double Nominal { get; set; }

        public int Jumlah { get; set; }
       
    }
}
