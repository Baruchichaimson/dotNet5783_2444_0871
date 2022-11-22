using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enums;

namespace BlApi
{
    public interface IOrder
    {
        public List<OrderForList> GetList();
        public Order GetData(int id);
        public Order UpdateShippingDate(int id);
        public Order DeliveryUpdate(int id);
        public OrderTracking OrderTracking(int id);
        public Order UpdateAdmin(int id);

    }
}
