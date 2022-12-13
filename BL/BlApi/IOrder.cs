using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    /// <summary>
    /// order entity interface
    /// </summary>
    public interface IOrder
    {
        /// <summary>
        /// the function return the list of all ordrs
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BO.OrderForList?>? GetList();
        /// <summary>
        /// the function recevies an order id and return the order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BO.Order GetData(int id);
        /// <summary>
        /// the function recevies an order id and update a shipping date
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BO.Order UpdateShippingDate(int id);
        /// <summary>
        /// the function receives an order id and update a date of delivery
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BO.Order DeliveryUpdate(int id);
        /// <summary>
        /// the function receives an order id and displays the order status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BO.OrderTracking OrderTracking(int id);
        /// <summary>
        /// the function allowed the manger to update the cart items.
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="productId"></param>
        /// <param name="amount"></param>
        public void UpdateAdmin(int orderId, int productId, int amount);

    }
}
