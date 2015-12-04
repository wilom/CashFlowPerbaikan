using dokuku.Dto;
using dokuku.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku.interfaces
{
    public interface INotaPengeluaran
    {

        DateTime Date { get; }

        string[] ListAkun();

        NotaPengeluaranDto Snap();

        NotaPengeluaranId NoNota { get; }
    }
}
