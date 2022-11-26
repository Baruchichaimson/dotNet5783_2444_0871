using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICart
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public BO.Cart AddProduct(Cart cart, int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="id"></param>
        /// <param name="newAmount"></param>
        /// <returns></returns>
        public BO.Cart UpdateProductAmount(Cart cart, int id, int newAmount);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cart"></param>
        public void OrderConfirmation(Cart cart);
    }
}
