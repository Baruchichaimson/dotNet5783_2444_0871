using System;
using BlApi;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;

namespace BlImplementation
{
    internal class Order : IOrder
    {
        private IDal Dal = new DO.DalList();
        public List<BO.OrderForList> GetList()
        {

        }
        public Order GetData(int id)
        {

        }
        public Order UpdateShippingDate(int id)
        {

        }
        public Order DeliveryUpdate(int id)
        {

        }
        public BO.OrderTracking OrderTracking(int id)
        {

        }
        public Order UpdateAdmin(int id)
        {

        }
    }
}
