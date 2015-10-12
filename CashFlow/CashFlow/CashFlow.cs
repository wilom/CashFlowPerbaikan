using dokuku.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku.CashFlow
{
    public class CashFlow
    {
        private string p1;
        private Periode periode;
        private double p2;

        public CashFlow(string p1, Periode periode, double p2)
        {
            // TODO: Complete member initialization
            this.p1 = p1;
            this.periode = periode;
            this.p2 = p2;
        }
        public CashFlowDto Snap()
        {
            return new CashFlowDto(); 
        }
    }
}
