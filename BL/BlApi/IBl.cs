using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
 /// <summary>
 /// 
 /// </summary>
    public interface IBl
    {
        /// <summary>
        /// 
        /// </summary>
        public IProduct Product { get; }
        /// <summary>
        /// 
        /// </summary>
        public IOrder Order { get; }
        /// <summary>
        /// 
        /// </summary>
        public ICart Cart { get; }

    }
}
