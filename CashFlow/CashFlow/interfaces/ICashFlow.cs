using dokuku.Dto;
using dokuku.exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku.interfaces
{
    public interface ICashFlow
    {
        void ChangePengeluaran(string akun, double nominal, int jumlah);

        CashFlowDto Snap();

        CashFlowId GenerateId();
    }
}
