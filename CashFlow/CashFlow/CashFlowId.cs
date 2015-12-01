using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku
{
    public class CashFlowId
    {
        private PeriodeId _periodId = null;
        public CashFlowId(PeriodeId periodId)
        {
            this._periodId = periodId;
        }
        public override bool Equals(object obj)
        {
            if (!(obj is CashFlowId)) return false;
            var cmp = (CashFlowId)obj;
            return this._periodId.Equals(cmp._periodId);
        }
        public override int GetHashCode()
        {
            return this._periodId.GetHashCode();
        }

    }
}
