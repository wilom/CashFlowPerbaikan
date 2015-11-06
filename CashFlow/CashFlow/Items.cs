using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku
{
    public class Items
    {
        public string Akun { get; set; }
        public int Jumlah { get; set; }
        public double Nominal { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Items)) return false;
            var cmp = (Items)obj;
            return this.Akun.Equals(cmp.Akun) &&
                this.Jumlah.Equals(cmp.Jumlah) &&
                this.Nominal.Equals(cmp.Nominal);
        }
    }
}
