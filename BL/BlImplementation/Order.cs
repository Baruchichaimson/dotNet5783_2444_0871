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
    /// class for Order Management
    /// </summary>
    internal class Order : IOrder
    {
        private DalApi.IDal Dal = new DO.DalList();
        /// <summary>
        /// A helper function that returns an order status
        /// </summary>
        /// <param name="item">order from data layer</param>
        /// <returns>order status</returns>
        private OrderStatus Status(DO.Order item)
        {
            OrderStatus currentStatus = new();
            if (item.DeliveryrDate is null)
            {
                if (item.ShipDate is null)
                    currentStatus = OrderStatus.CONFIRMED;
                else
                    currentStatus = OrderStatus.SHIPPED;
            }
            else
                currentStatus = OrderStatus.PROVIDED;
            return currentStatus;
        }
        /// <summary>
        /// A helper function that converts a list of orderitem details from the data layer to a
        /// list of orderitem details from the logical layer
        /// </summary>
        /// <param name="idOrder">order id from data layer</param>
        /// <returns>A list of orderItem of the logical layer</returns>
        private List<OrderItem> GiveList(DO.Order idOrder)
        {
            try
            {
                List<OrderItem> list = new List<OrderItem>();
                foreach (DO.OrderItem? item in Dal.OrderItem.List(element => element?.OredrID == idOrder.Id) ?? throw new NullExeption("order item list"))
                {
                    OrderItem dataItem = new()
                    {
                        ID = (int)item?.OredrID!,
                        ProductID = (int)item?.ProductID!,
                        Price = (double)item?.Price!,
                        Amount = (int)item?.Amount!,
                        TotalPrice = (double)item?.Price! * (int)item?.Amount!,
                        Name = Dal.Product.Get((int)item?.ProductID!).Name
                    };
                    list.Add(dataItem);
                }
                return list;
            }
            catch (DO.EntityNotFoundException ex)
            {
                throw new BO.EntityNotFoundException(ex);
            }
            catch (DO.NullExeption ex)
            {
                throw new BO.NullExeptionForDO(ex);
            }
           
        }
        /// <summary>
        /// Helper function that returns a string with the order details
        /// </summary>
        /// <param name="date">date</param>
        /// <param name="text">Description</param>
        /// <returns></returns>
        private string GiveOrderDate(DateTime? date, string text)
        {
            string tempString = $@"in {date}: the order is {text}";
            return tempString;
        }
        // **********************************************************************************************************************
        // **********************************************************************************************************************
        /// <summary>
        /// A function that converts a list of order from the data layer
        /// to a list of order  from the logical layer
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OrderForList?>? GetList()
        {
            IEnumerable<DO.Order?>? list = Dal.Order.List(element => element is not null);
            return list?.Select(element => 
            {
                int totalAmount = 0;
                double totalPrice = 0;
                foreach (DO.OrderItem it in Dal.OrderItem.List(x => x is not null && x?.Id == element?.Id) ?? throw new NullExeption("order item list"))
                {
                    totalPrice += it.Price * it.Amount;
                    totalAmount++;
                }
                return new OrderForList
                {
                    ID = (int)element?.Id!,
                    CustomerName = (string)element?.CustomerName!,
                    TotalPrice = totalPrice,
                    AmountOfItems = totalAmount,
                    Status = Status(element!.Value),
                };
            });
        }
        /// <summary>
        /// A function that returns an order by ID number
        /// </summary>
        /// <param name="id"> order id</param>
        /// <returns> Order  </returns>
        /// <exception cref="EntityNotFoundException"> trow an Exception when the order was not found in the database</exception>
        public BO.Order GetData(int id)
        {
            double totalPrice = 0;
            foreach (DO.OrderItem it in Dal.OrderItem.List(element => element is not null && element?.Id == id) ?? throw new NullExeption("order list"))
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
                    throw new BO.EntityNotFoundException(ex);
                }
                catch (DO.NullExeption ex)
                {
                    throw new BO.NullExeptionForDO(ex);
                }
            }
            throw new BO.IdNotExsitException("Order id not valid");
        }
        /// <summary>
        /// A function that updates shipping time
        /// </summary>
        /// <param name="id"> order id</param>
        /// <returns>Updated order </returns>
        /// <exception cref="EntityDetailsWrongException">Throws an exception when the order was allredy shiped</exception>
        public BO.Order UpdateShippingDate(int id)
        {
            try
            {
                if (Dal.Order.Get(id).ShipDate is null)
            {
               
                    DO.Order updateOrders = Dal.Order.Get(id);
                    updateOrders.ShipDate = DateTime.Now;
                    Dal.Order.Update(updateOrders);
                    BO.Order updorder = GetData(id);
                    updorder.ShipDate = updateOrders.ShipDate;
                    return updorder;
                }
               
            }
            catch (DO.EntityNotFoundException ex)
            {
                throw new BO.EntityNotFoundException(ex);
            }
            catch (DO.NullExeption ex)
            {
                throw new BO.NullExeptionForDO(ex);
            }
            throw new BO.EntityDetailsWrongException("Order allredy shiped");
        }
        /// <summary>
        /// A function that updates Delivery time
        /// </summary>
        /// <param name="id"> order id</param>
        /// <returns>Updated order </returns>
        /// <exception cref="EntityDetailsWrongException">Throws an exception when the order deliverd or not shiped </exception>
        public BO.Order DeliveryUpdate(int id)
        {
            try
            {
                if (Dal.Order.Get(id).ShipDate is not null && Dal.Order.Get(id).DeliveryrDate is null)
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
                throw new BO.EntityNotFoundException(ex);
            }
            catch (DO.NullExeption ex)
            {
                throw new BO.NullExeptionForDO(ex);
            }
            throw new BO.EntityDetailsWrongException("delivery time cannot be updated");
        }
        /// <summary>
        /// A function that returns an order tracking
        /// </summary>
        /// <param name="id"> order id</param>
        /// <returns>Updated orderTracking </returns>
        /// <exception cref="EntityNotFoundException">Throws an exception when the order was not found in the database</exception>
        public OrderTracking OrderTracking(int id)
        {
            try
            {
                DO.Order order = Dal.Order.Get(id);
                List<string?> templist = new();
                templist.Add(GiveOrderDate(order.OrderDate, "created"));
                if (order.ShipDate is not null)
                {
                    templist.Add(GiveOrderDate(order.ShipDate, "shipped"));
                    if (order.DeliveryrDate is not null)
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
                throw new BO.EntityNotFoundException(ex);
            }
            catch (DO.AllreadyExistException ex)
            {
                throw new BO.AllreadyExistException(ex);
            }
            catch (DO.NullExeption ex)
            {
                throw new BO.NullExeptionForDO(ex);
            }
        }
        /// <summary>
        /// A function for the store manager to update the quantity of a product in an order that has not been shipped
        /// </summary>
        /// <param name="orderId">order id for data base</param>
        /// <param name="productId">product id for data base</param>
        /// <param name="amount">how much more\less to update the order item</param>
        /// <exception cref="IncorrectAmountException">throwing exception when the amount is Invalid</exception>
        /// <exception cref="EntityNotFoundException">Throws an exception when the order was not found in the database </exception>
        /// <exception cref="AllreadyExistException">Throws an exception when adding order when is Allready Exist in the database </exception>
        public void UpdateAdmin(int orderId, int productId, int amount)
        {
            try
            {
                DO.Product product = Dal.Product.Get(productId);
                if (amount > product.Instock)
                    throw new BO.IncorrectAmountException("Not enough amount in stock");
                int id = 0;
                if (Dal.Order.Get(orderId).ShipDate is null)
                {
                    foreach (var orderItems in Dal.OrderItem.List(element => element?.OredrID == orderId)?? throw new NullExeption("order item list"))
                    {
                        if (productId == orderItems?.ProductID)
                        {
                            id = orderItems?.Id ?? throw new NullExeption("order item");
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
                            throw new BO.IncorrectAmountException("the amount is not positive");
                    }
                    else
                    {
                        DO.OrderItem item = Dal.OrderItem.Get(id);
                        if (item.Amount + amount >= 0)
                            item.Amount += amount;
                        else
                            throw new BO.IncorrectAmountException("reduce amount to much");
                        if (item.Amount == 0)
                            Dal.OrderItem.Delete(item.Id);
                        else
                            Dal.OrderItem.Update(item);
                    }
                    product.Instock -= amount;
                    Dal.Product.Update(product);
                }
                else
                    throw new BO.EntityDetailsWrongException("the order is allready been sent");
            }
            catch (DO.EntityNotFoundException ex)
            {
                throw new BO.EntityNotFoundException(ex);
            }
            catch (DO.AllreadyExistException ex)
            {
                throw new BO.AllreadyExistException(ex);
            }
            catch (DO.NullExeption ex)
            {
                throw new BO.NullExeptionForDO(ex);
            }
        }
    }
}
