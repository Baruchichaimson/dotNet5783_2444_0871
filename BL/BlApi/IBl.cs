using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IBl
    {
        public IProduct BoProduct { get; }
        public IOrder BoOrder { get; }
        public ICart BoCart { get; }

    }
}
