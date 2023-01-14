using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    /// <summary>
    /// XmlLock is a static class that contains a read-only object 's_lock' used for thread synchronization
    /// </summary>
    public static class XmlLock
    {
        public static readonly object s_lock = new object();
    }

}
