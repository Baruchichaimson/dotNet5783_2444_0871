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
    public interface IOrder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<BO.OrderForList> GetList();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BO.Order GetData(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BO.Order UpdateShippingDate(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BO.Order DeliveryUpdate(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BO.OrderTracking OrderTracking(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="productId"></param>
        /// <param name="amount"></param>
        public void UpdateAdmin(int orderId, int productId, int amount);

    }
}
