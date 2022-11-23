using System;
using BlApi;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using BO;

namespace BlImplementation
{
    internal class Order : IOrder
    {
        private DalApi.IDal Dal = new DO.DalList();
        private OrderStatus Status(DO.Order item)
        {
            BO.OrderStatus currentStatus = new BO.OrderStatus();
            if (item.DeliveryrDate == DateTime.MinValue)
            {
                if (item.ShipDate == DateTime.MinValue)
                    currentStatus = BO.OrderStatus.CONFIRMED;
                else
                    currentStatus = BO.OrderStatus.SHIPPED;
            }
            else
                currentStatus = BO.OrderStatus.PROVIDED;
            return currentStatus;
        }
        private List<BO.OrderItem> GiveList(DO.Order idOrder)
        {
            List<BO.OrderItem> list = new List<BO.OrderItem>();
            foreach (DO.OrderItem item in Dal.OrderItem.OrderItemsListByOrder(idOrder.Id))
            {
                BO.OrderItem dataItem = new()
                {
                    ID = item.OredrID,
                    ProductID = item.ProductID,
                    Price = item.Price,
                    Amount = item.Amount,
                    TotalPrice = item.Price * item.Amount,
                    Name = Dal.Product.Get(item.ProductID).Name
                };
                list.Add(dataItem);
            }
            return list;
        }
        //***************************************************************************************
        //***************************************************************************************
        public List<BO.OrderForList> GetList()
        {     
            List<BO.OrderForList> newList = new List<BO.OrderForList>();
            foreach(DO.Order item in Dal.Order.List())
            {
                int totalamount = 0;
                double totalPrice = 0;
                foreach (DO.OrderItem it in Dal.OrderItem.OrderItemsListByOrder(item.Id))
                {
                    totalPrice += it.Price;
                    totalamount++;
                }
                
                BO.OrderForList orderForList = new()
                {
                    ID = item.Id,
                    CustomerName = item.CustomerName,
                    TotalPrice = totalPrice,
                    AmountOfItems = totalamount,
                    Status = Status(item),
                };
                newList.Add(orderForList);
            }
            return newList;
        }
        public BO.Order GetData(int id)
        {
            if (id > 0)
            {         
                DO.Order dataOrder = Dal.Order.Get(id);  
                
                BO.Order order = new()
                {
                    ID = id,
                    CustomerName = dataOrder.CustomerName,
                    CustomerAdress = dataOrder.CustomerAdress,
                    CustomerEmail = dataOrder.CustomerEmail,
                    DeliveryrDate = dataOrder.DeliveryrDate,
                    ShipDate = dataOrder.ShipDate,
                    OrderDate = dataOrder.OrderDate,
                    Status = Status(dataOrder),
                    Items = GiveList(dataOrder)
                };
                return order;
            }
            throw new Exception("id not exist");
        }
        public BO.Order UpdateShippingDate(int id)
        {
            bool exsit = Dal.Order.List().Any(x => x.Id == id);
            if (exsit && Dal.Order.Get(id).ShipDate == DateTime.MinValue)
            {
                DO.Order updateOrders = Dal.Order.Get(id);
                updateOrders.ShipDate = DateTime.Now;
                Dal.Order.Update(updateOrders);

                BO.Order updorder = GetData(id);
                updorder.ShipDate = updateOrders.ShipDate;

                return updorder;
            }
            throw new Exception("not exsit");
        }
        public BO.Order DeliveryUpdate(int id)
        {
            bool exsit = Dal.Order.List().Any(x => x.Id == id);
            if (exsit && Dal.Order.Get(id).ShipDate != DateTime.MinValue && Dal.Order.Get(id).DeliveryrDate == DateTime.MinValue)
            {
                DO.Order updateOrders = Dal.Order.Get(id);
                updateOrders.ShipDate = DateTime.Now;
                Dal.Order.Update(updateOrders);

                BO.Order updorder = GetData(id);
                updorder.ShipDate = updateOrders.ShipDate;

                return updorder;
            }
            throw new Exception("not exsit");
        }
        public BO.OrderTracking OrderTracking(int id)
        {
            BO.OrderTracking tracking = new(); 
            bool exsit = Dal.Order.List().Any(x => x.Id == id);
            if (exsit)
            {

            }
        }
        public BO.Order UpdateAdmin(int id)
        {

        }
    }
}
