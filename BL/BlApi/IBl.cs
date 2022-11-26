using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    /// <summary>
    /// The interface is used as a wrapper for our other
    /// interfaces and it will be more comfortable
    /// to work through it later
    /// </summary>
    public interface IBl
    {
        /// <summary>
        /// Creating an attribute from its interface type in order 
        /// to use this instance to receive into it the logical functions
        /// that we used on that entity and transfer it to the display layer
        /// </summary>
        public IProduct Product { get; }
        /// <summary>
        /// Creating an attribute from its interface type in order 
        /// to use this instance to receive into it the logical functions
        /// that we used on that entity and transfer it to the display layer
        /// </summary>
        public IOrder Order { get; }
        /// <summary>
        /// Creating an attribute from its interface type in order 
        /// to use this instance to receive into it the logical functions
        /// that we used on that entity and transfer it to the display layer
        /// </summary>
        public ICart Cart { get; }

    }
}
