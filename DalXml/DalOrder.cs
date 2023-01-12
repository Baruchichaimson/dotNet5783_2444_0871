using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal
{


    internal class DalOrder : IOrder
    {
        const string s_orderItem = @"OrderItems";
        const string s_product = @"Products";
        const string s_order = @"Orders";
        const string s_idConfig = @"ConfigId.xml";
        const string orderId = "orderId";
        XElement configId = File.Exists(XMLTools.GetDir() + s_idConfig) ? XElement.Load(XMLTools.GetDir() + s_idConfig) : throw new NullExeption($"{s_idConfig}");
        XElement orders = File.Exists(XMLTools.GetDir() + $"{s_order}.xml") ? XElement.Load(XMLTools.GetDir() + $"{s_order}.xml") : throw new NullExeption($"{s_order}");

        public int Add(Order newOrder)
        {
            newOrder.Id = Convert.ToInt32(configId.Element(orderId)?.Value ?? "-1") ;
            configId.Element(orderId)!.Value = (newOrder.Id + 1).ToString();
            orders.Add(OrderToXElement(newOrder));
            orders.Save(XMLTools.GetDir() + $"{s_order}.xml");

            return newOrder.Id;
        }

        public void Delete(int idToDelete)
        {
            XElement? orderElemnt;
            try
            {
                orderElemnt = (from order in orders.Elements()
                               where Convert.ToInt32(order.Element("id")!.Value) == idToDelete
                               select order).FirstOrDefault();
                orderElemnt?.Remove();
                orders.Save(XMLTools.GetDir() + $"{s_order}.xml");
            }
            catch
            {
                throw new NullExeption($"XMLOrderElemnt");
            } 

        }

        public Order Get(int idToGet)
        {
            return GetElement(element => element?.Id == idToGet);
        }

        public Order GetElement(Func<Order?, bool>? myFunc = null)
        {
            if (myFunc is null)
            {
                throw new NullExeption("XMLcondition");
            }
            XElement orderElemnt;
            orderElemnt = (from order in orders.Elements()
                           where myFunc(XElementToOrder(order))
                           select order).FirstOrDefault() ?? throw new NullExeption("order item");

            return XElementToOrder(orderElemnt);
        }

        public IEnumerable<Order?>? List(Func<Order?, bool>? myFunc = null)
        {
            if (myFunc is null)
            {
                return (from order in orders.Elements()
                        select XElementToNullOrder(order));
            }
            return (from order in orders.Elements()
                    where myFunc(XElementToOrder(order))
                    select XElementToNullOrder(order));
             
        }

        public void Update(Order newEntity)
        {
            XElement? orderElement;
            try
            {
                orderElement = (from order in orders.Elements()
                               where Convert.ToInt32(order.Element("id")!.Value) == newEntity.Id
                               select order).FirstOrDefault();
                orderElement?.Remove();
                orders.Add(OrderToXElement(newEntity));
                orders.Save(XMLTools.GetDir() + $"{s_order}.xml");
            }
            catch
            {
                throw new NullExeption($"XMLOrderElemnt");
            }

        }
        private Order XElementToOrder(XElement element)
        {
            var order = new Order
            {
                Id = (int)element.Element("Id")!,
                CustomerName = (string)element.Element("CustomerName")!,
                CustomerEmail = (string)element.Element("CustomerEmail")!,
                CustomerAdress = (string)element.Element("CustomerAdress")!,
                OrderDate = DateTime.TryParse(element.Element("OrderDate").Value, out var orderDate) ? orderDate : null,
                ShipDate = DateTime.TryParse(element.Element("ShipDate").Value, out var shipDate) ? shipDate : null,
                DeliveryrDate = DateTime.TryParse(element.Element("DeliveryrDate").Value, out var deliveryrDate) ? deliveryrDate : null,
            };
            return order;
        }
        private Order? XElementToNullOrder(XElement element)
        {
            Order? order = new Order
            {
                Id = (int)element.Element("Id")!,
                CustomerName = (string)element.Element("CustomerName")!,
                CustomerEmail = (string)element.Element("CustomerEmail")!,
                CustomerAdress = (string)element.Element("CustomerAdress")!,
                OrderDate = DateTime.TryParse(element.Element("OrderDate").Value, out var orderDate) ? orderDate : null,
                ShipDate = DateTime.TryParse(element.Element("ShipDate").Value, out var shipDate) ? shipDate : null,
                DeliveryrDate = DateTime.TryParse(element.Element("DeliveryrDate").Value, out var deliveryrDate) ? deliveryrDate : null,
            };
            return order;
        }
        private XElement OrderToXElement(Order order)
        {
            var element = new XElement("Order",
                new XElement("Id", order.Id),
                new XElement("CustomerName", order.CustomerName),
                new XElement("CustomerEmail", order.CustomerEmail),
                new XElement("CustomerAdress", order.CustomerAdress),
                new XElement("OrderDate", order.OrderDate),
                new XElement("ShipDate", order.ShipDate),
                new XElement("DeliveryrDate", order.DeliveryrDate)
            );
            return element;
        }


    }
}
