using System;
using BlApi;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.ComponentModel;
using BO;
using System.Xml.Schema;

namespace BlImplementation
{
    /// <summary>
    /// 
    /// </summary>
    internal class Order : IOrder
    {
        private DalApi.IDal Dal = new DO.DalList();
        private OrderStatus Status(DO.Order item)
        {
            OrderStatus currentStatus = new OrderStatus();
            if (item.DeliveryrDate == DateTime.MinValue)
            {
                if (item.ShipDate == DateTime.MinValue)
                    currentStatus = OrderStatus.CONFIRMED;
                else
                    currentStatus = OrderStatus.SHIPPED;
            }
            else
                currentStatus = OrderStatus.PROVIDED;
            return currentStatus;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idOrder"></param>
        /// <returns></returns>
        private List<OrderItem> GiveList(DO.Order idOrder)
        {
            List<OrderItem> list = new List<OrderItem>();
            foreach (DO.OrderItem item in Dal.OrderItem.OrderItemsListByOrder(idOrder.Id))
            {
                OrderItem dataItem = new()
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private string GiveOrderDate(DateTime date, string text)
        {
            string tempString = $@"in {date}: the order is {text}";
            return tempString;
        }
        //***************************************************************************************
        //***************************************************************************************
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<OrderForList> GetList()
        {
            List<OrderForList> newList = new List<OrderForList>();
            foreach (DO.Order item in Dal.Order.List())
            {
                int totalamount = 0;
                double totalPrice = 0;
                foreach (DO.OrderItem it in Dal.OrderItem.OrderItemsListByOrder(item.Id))
                {
                    totalPrice += it.Price;
                    totalamount++;
                }

                OrderForList orderForList = new()
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <exception cref="AllreadyExistException"></exception>
        public BO.Order GetData(int id)
        {
            double totalPrice = 0;
            foreach (DO.OrderItem it in Dal.OrderItem.OrderItemsListByOrder(id))
            {
                totalPrice += it.Price * it.Amount;
            }
            if (id > 0)
            {
                try
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
                        Items = GiveList(dataOrder),
                        TotalPrice = totalPrice
                    };
                    return order;
                }
                catch (DO.EntityNotFoundException ex)
                {
                    throw new EntityNotFoundException(ex.Message);
                }
                catch (DO.AllreadyExistException ex)
                {
                    throw new AllreadyExistException(ex.Message);
                }
            }
            throw new EntityNotFoundException("Order not found");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <exception cref="AllreadyExistException"></exception>
        public BO.Order UpdateShippingDate(int id)
        {
            bool exsit = Dal.Order.List().Any(x => x.Id == id);
            if (exsit && Dal.Order.Get(id).ShipDate == DateTime.MinValue)
            {
                try
                {
                    DO.Order updateOrders = Dal.Order.Get(id);
                    updateOrders.ShipDate = DateTime.Now;
                    Dal.Order.Update(updateOrders);
                    BO.Order updorder = GetData(id);
                    updorder.ShipDate = updateOrders.ShipDate;
                    return updorder;
                }
                catch (DO.EntityNotFoundException ex)
                {
                    throw new EntityNotFoundException(ex.Message);
                }
                catch (DO.AllreadyExistException ex)
                {
                    throw new AllreadyExistException(ex.Message);
                }
            }
            throw new EntityNotFoundException("Order not found");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <exception cref="AllreadyExistException"></exception>
        public BO.Order DeliveryUpdate(int id)
        {
            bool exsit = Dal.Order.List().Any(x => x.Id == id);
            try
            {
                if (exsit && Dal.Order.Get(id).ShipDate != DateTime.MinValue && Dal.Order.Get(id).DeliveryrDate == DateTime.MinValue)
                {
                    DO.Order updateOrdersData = Dal.Order.Get(id);
                    updateOrdersData.DeliveryrDate = DateTime.Now;
                    Dal.Order.Update(updateOrdersData);
                    BO.Order updateOrderLogic = GetData(id);
                    updateOrderLogic.ShipDate = updateOrderLogic.ShipDate;
                    return updateOrderLogic;
                }
            }
            catch (DO.EntityNotFoundException ex)
            {
                throw new EntityNotFoundException(ex.Message);
            }
            catch (DO.AllreadyExistException ex)
            {
                throw new AllreadyExistException(ex.Message);
            }
            throw new EntityNotFoundException("Order not found");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <exception cref="AllreadyExistException"></exception>
        public OrderTracking OrderTracking(int id)
        {
            try
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
                OrderTracking tracking = new()
                {
                    ID = id,
                    Status = Status(order),
                    orderDetails = templist
                };
                return tracking;
            }
            catch (DO.EntityNotFoundException ex)
            {
                throw new EntityNotFoundException(ex.Message);
            }
            catch (DO.AllreadyExistException ex)
            {
                throw new AllreadyExistException(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="productId"></param>
        /// <param name="amount"></param>
        /// <exception cref="IncorrectAmountException"></exception>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <exception cref="AllreadyExistException"></exception>
        public void UpdateAdmin(int orderId, int productId, int amount)
        {
            try
            {
                DO.Product product = Dal.Product.Get(productId);
                if (amount > product.Instock)
                    throw new IncorrectAmountException("Not enough amount in stock");
                int id = 0;
                if (Dal.Order.Get(orderId).ShipDate == DateTime.MinValue)
                {
                    foreach (DO.OrderItem orderItems in Dal.OrderItem.OrderItemsListByOrder(orderId))
                    {
                        if (productId == orderItems.ProductID)
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
                            throw new IncorrectAmountException("the amount is not positive");
                    }
                    else
                    {
                        DO.OrderItem item = Dal.OrderItem.Get(id);
                        if (item.Amount + amount >= 0)
                            item.Amount += amount;
                        else
                            throw new IncorrectAmountException("reduce amount to much");
                        if (item.Amount == 0)
                            Dal.OrderItem.Delete(item.Id);
                        else
                            Dal.OrderItem.Update(item);
                    }
                    product.Instock -= amount;
                    Dal.Product.Update(product);
                }
                else
                    throw new EntityDetailsWrongException("the order is allready been sent");
            }
            catch (DO.EntityNotFoundException ex)
            {
                throw new EntityNotFoundException(ex.Message);
            }
            catch (DO.AllreadyExistException ex)
            {
                throw new AllreadyExistException(ex.Message);
            }
        }
    }
}
