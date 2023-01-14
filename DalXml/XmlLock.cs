using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public static class XmlLock
    {
        public static readonly object s_lock = new object();
    }

}
