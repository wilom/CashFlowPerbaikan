using dokuku.CashFlowHead;
using dokuku.exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku
{
    public class ProcessNotaPengeluaran
    {
        public ProcessNotaPengeluaran()
        {

        }
                
        public interfaces.IRepository Repository { get; set; }

        public void Process(interfaces.ICashFlow cashFlow)
        {
            throw new CashflowNotFoundException();
        }
    }
}
