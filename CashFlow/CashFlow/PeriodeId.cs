using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dokuku
{
    public class PeriodeId
    {
        private string _id;
        public PeriodeId(string id)
        {
            this._id = id;
        }
        public override string ToString()
        {
            return this._id;
        }
    }
}
