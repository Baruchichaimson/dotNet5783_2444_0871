using System;
using BlApi;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation
{
    internal class Order : IOrder
    {
        private DalApi.IDal Dal = new DO.DalList();
        public List<BO.OrderForList> GetList()
        {

        }
        public BO.Order GetData(int id)
        {

        }
        public BO.Order UpdateShippingDate(int id)
        {

        }
        public BO.Order DeliveryUpdate(int id)
        {

        }
        public BO.OrderTracking OrderTracking(int id)
        {

        }
        public BO.Order UpdateAdmin(int id)
        {

        }
    }
}
