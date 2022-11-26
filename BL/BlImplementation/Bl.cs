using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation
{
    /// <summary>
    /// 
    /// </summary>
    sealed public class Bl : IBl
    {
        /// <summary>
        /// 
        /// </summary>
        public IProduct Product => new Product();
        /// <summary>
        /// 
        /// </summary>
        public IOrder Order => new Order();
        /// <summary>
        /// 
        /// </summary>
        public ICart Cart => new Cart();

    }
}
