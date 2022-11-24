using System;
using BlApi;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.ComponentModel;

namespace BlImplementation
{
    internal class Order : IOrder
    {
        private DalApi.IDal Dal = new DO.DalList();
        private BO.OrderStatus Status(DO.Order item)
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
        private string GiveOrderDate(DateTime date , string text)
        {

            string tempString = $@"in {date}: the order is {text}";
            return tempString;
        }
        //***************************************************************************************
        //***************************************************************************************
        public List<BO.OrderForList> GetList()
        {
            List<BO.OrderForList> newList = new List<BO.OrderForList>();
            foreach (DO.Order item in Dal.Order.List())
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
                DO.Order updateOrdersData = Dal.Order.Get(id);
                updateOrdersData.ShipDate = DateTime.Now;
                Dal.Order.Update(updateOrdersData);

                BO.Order updateOrderLogic = GetData(id);
                updateOrderLogic.ShipDate = updateOrderLogic.ShipDate;

                return updateOrderLogic;
            }
            throw new Exception("not exsit");
        }
        public BO.OrderTracking OrderTracking(int id)
        {
            DO.Order order = Dal.Order.Get(id);
            
            List<string> templist = new();
            templist.Add(GiveOrderDate(order.OrderDate, "created"));
            if (order.ShipDate != DateTime.MinValue)
            {
                templist.Add(GiveOrderDate(order.ShipDate, "shipped"));
                if (order.DeliveryrDate != DateTime.MinValue)
                    templist.Add(GiveOrderDate(order.DeliveryrDate, "deliverd"));
            }

            BO.OrderTracking tracking = new()
            {
                ID = id,
                Status = Status(order),
                orderDetails = templist
            };
            return tracking;
        }
        public void UpdateAdmin(int orderId, int productId ,int amount)
        {
            DO.Product product = Dal.Product.Get(productId);
            if (amount > product.Instock)
                throw new Exception("the amount is not exsit");
            int id = 0;
            if (Dal.Order.Get(orderId).ShipDate == DateTime.MinValue)
            {
                foreach (DO.OrderItem orderItems in Dal.OrderItem.OrderItemsListByOrder(orderId))
                {
                    if(productId == orderItems.ProductID)
                    {
                       id = orderItems.Id;
                        break;   
                    }
                }
                if (id == 0)
                {
                    if (amount > 0)
                    {
                        DO.OrderItem item = new()
                        {
                            ProductID = productId,
                            OredrID = orderId,
                            Amount = amount,
                            Price = product.Price * amount
                        };
                        Dal.OrderItem.Add(item);
                    }
                    else
                        throw new Exception("the amount is not positive");
                }
                else
                { 
                    DO.OrderItem item = Dal.OrderItem.Get(id);
                    if(item.Amount + amount >= 0)
                        item.Amount += amount;
                    else
                        throw new Exception("reduce to much");
                    if(item.Amount == 0)
                        Dal.OrderItem.Delete(item.Id);
                    else
                        Dal.OrderItem.Update(item);
                }
                product.Instock -= amount;
                Dal.Product.Update(product);
            }
        }
    }
}
