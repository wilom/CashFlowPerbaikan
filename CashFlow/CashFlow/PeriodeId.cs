using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku
{
    public class PeriodeId
    {
        private string _periodeId;
        public PeriodeId(string periodeId)
        {
            this._periodeId = periodeId;
        }
        public override string ToString()
        {
            return this._periodeId;
        }
        public override bool Equals(object obj)
        {
            if (!(obj is PeriodeId)) return false;
            var cmp = (PeriodeId)obj;
            return this._periodeId.Equals(cmp._periodeId);
        }
        public override int GetHashCode()
        {
            return this._periodeId.GetHashCode();
        }

    }
}
