using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IOrder
    {
        public List<BO.OrderForList> GetList();
        public BO.Order GetData(int id);
        public BO.Order UpdateShippingDate(int id);
        public BO.Order DeliveryUpdate(int id);
        public BO.OrderTracking OrderTracking(int id);
        public void UpdateAdmin(int orderId, int productId, int amount);

    }
}
