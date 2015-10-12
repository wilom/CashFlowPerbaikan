using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku.Dto
{
    public class ItemsDto
    {

        public string Akun { get; set; }

        public double Nominal { get; set; }
        public override bool Equals(object obj)
        {
            if (!(obj is ItemsDto)) return false;
            var cmp = (ItemsDto)obj;
            return this.Akun.Equals(cmp.Akun) &&
                this.Nominal.Equals(cmp.Nominal);
        }
    }
}
