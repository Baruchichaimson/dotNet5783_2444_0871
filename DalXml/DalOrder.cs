using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal;

internal class DalOrder : IOrder
{
    const string s_order = @"Orders";
    const string s_idConfig = @"ConfigId.xml";
    const string orderId = @"orderId";
    XElement orders = File.Exists(XMLTools.GetDir() + $"{s_order}.xml") ? XElement.Load(XMLTools.GetDir() + $"{s_order}.xml") : throw new NullExeption($"{s_order}");

    /// <summary>
    /// This method adds a new order to the XML file by first loading the order id configuration file, increasing the id, saving it, and then converting the order object to an XElement and adding it to the orders list and saving the list to the XML file.
    /// </summary>
    /// <param name="newOrder">The new order to be added</param>
    /// <returns>The id of the added order</returns>
    /// <exception cref="NullExeption">Thrown when the order id configuration file is not found</exception>
    public int Add(Order newOrder)
    {
        XElement configId = File.Exists(XMLTools.GetDir() + s_idConfig) ? XElement.Load(XMLTools.GetDir() + s_idConfig) : throw new NullExeption($"{s_idConfig}");


        lock (XmlLock.s_lock)
        {
            newOrder.Id = Convert.ToInt32(configId.Element(orderId)?.Value ?? "-1");
            configId.Element(orderId)!.Value = (newOrder.Id + 1).ToString();
            configId.Save(XMLTools.GetDir() + s_idConfig);
        }
        orders.Add(OrderToXElement(newOrder));
        orders.Save(XMLTools.GetDir() + $"{s_order}.xml");

        return newOrder.Id;
    }
    /// <summary>
    /// This method deletes an order from the XML file by searching for the order with the given id, removing it from the list and saving the list to the XML file.
    /// </summary>
    /// <param name="idToDelete">The id of the order to be deleted</param>
    /// <exception cref="NullExeption">Thrown when the order is not found</exception>
    public void Delete(int idToDelete)
    {
        XElement? orderElemnt;
        try
        {
            orderElemnt = (from order in orders.Elements()
                           where Convert.ToInt32(order.Element("Id")!.Value) == idToDelete
                           select order).FirstOrDefault();
            orderElemnt?.Remove();
            orders.Save(XMLTools.GetDir() + $"{s_order}.xml");
        }
        catch
        {
            throw new NullExeption($"XMLOrderElemnt");
        } 

    }
    /// <summary>
    /// This method gets an order from the XML file by searching for the order with the given id using the GetElement method which takes a function for searching for the order.
    /// </summary>
    /// <param name="idToGet">The id of the order to be retrieved</param>
    /// <returns>The order object with the given id</returns>
    public Order Get(int idToGet)
    {
        return GetElement(element => element?.Id == idToGet);
    }
    /// <summary>
    /// This method gets an order from the XML file by searching for the order that meets a given condition using a provided function.
    /// </summary>
    /// <param name="myFunc">The function used to search for the order</param>
    /// <returns>The order object that meets the given condition</returns>
    /// <exception cref="NullExeption">Thrown when the condition is null or when the order is not found</exception>
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
    /// <summary>
    /// This method returns a list of orders from the XML file based on a given condition. If no condition is given, returns all orders.
    /// </summary>
    /// <param name="myFunc">The function used to filter the orders</param>
    /// <returns>An enumerable list of orders that meet the given condition</returns>
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
    /// <summary>
    /// This method updates an existing order in the XML file with new information provided.
    /// </summary>
    /// <param name="newEntity">The updated order information</param>
    /// <exception cref="NullExeption">Thrown when the order to be updated cannot be found in the XML file</exception>
    public void Update(Order newEntity)
    {
        XElement? orderElement;
        try
        {
            orderElement = (from order in orders.Elements()
                           where order.Element("Id") != null && Convert.ToInt32(order.Element("Id").Value) == newEntity.Id
                            select order).FirstOrDefault();
            orderElement?.Remove();
            orders.Save(XMLTools.GetDir() + $"{s_order}.xml");
            orders.Add(OrderToXElement(newEntity));
            orders.Save(XMLTools.GetDir() + $"{s_order}.xml");
        }
        catch
        {
            throw new NullExeption($"XMLOrderElemnt");
        }

    }
    /// <summary>
    /// Converts an XElement object to an Order object.
    /// </summary>
    /// <param name="element">The XElement object to convert to an Order object</param>
    /// <returns>An Order object created from the given XElement object</returns>
    private Order XElementToOrder(XElement element)
    {
        var order = new Order
        {
            Id = (int)element.Element("Id")!,
            CustomerName = (string)element.Element("CustomerName")!,
            CustomerEmail = (string)element.Element("CustomerEmail")!,
            CustomerAdress = (string)element.Element("CustomerAdress")!,
            OrderDate = (DateTime.TryParse(element.Element("OrderDate")?.Value, out var orderDate) && orderDate != DateTime.MinValue) ? orderDate : null,
            ShipDate = (DateTime.TryParse(element.Element("ShipDate")?.Value, out var shipDate) && shipDate != DateTime.MinValue) ? shipDate : null,
            DeliveryrDate = (DateTime.TryParse(element.Element("DeliveryrDate")?.Value, out var deliveryrDate) && deliveryrDate != DateTime.MinValue) ? deliveryrDate : null,
        };
        return order;

    }
    /// <summary>
    /// Converts an XElement object to an Order object, with nullable properties for dates.
    /// </summary>
    /// <param name="element">The XElement object to convert</param>
    /// <returns>The converted Order object</returns>
    private Order? XElementToNullOrder(XElement element)
    {
        var order = new Order
        {
            Id = (int)element.Element("Id")!,
            CustomerName = (string)element.Element("CustomerName")!,
            CustomerEmail = (string)element.Element("CustomerEmail")!,
            CustomerAdress = (string)element.Element("CustomerAdress")!,
            OrderDate = (DateTime.TryParse(element.Element("OrderDate")?.Value, out var orderDate) && orderDate != DateTime.MinValue) ? orderDate : null,
            ShipDate = (DateTime.TryParse(element.Element("ShipDate")?.Value, out var shipDate) && shipDate != DateTime.MinValue) ? shipDate : null,
            DeliveryrDate = (DateTime.TryParse(element.Element("DeliveryrDate")?.Value, out var deliveryrDate) && deliveryrDate != DateTime.MinValue) ? deliveryrDate : null,
        };
        return order;

    }
    /// <summary>
    /// Converts an Order object to an XElement object.
    /// </summary>
    /// <param name="order">The order object to convert</param>
    /// <returns></returns>
    private XElement OrderToXElement(Order order)
    {
        var element = new XElement("Order",
            new XElement("Id", order.Id),
            new XElement("CustomerName", order.CustomerName),
            new XElement("CustomerEmail", order.CustomerEmail),
            new XElement("CustomerAdress", order.CustomerAdress),
            new XElement("OrderDate", order.OrderDate.HasValue ? order.OrderDate.Value : default(DateTime)),
            new XElement("ShipDate", order.ShipDate.HasValue ? order.ShipDate.Value : default(DateTime)),
            new XElement("DeliveryrDate", order.DeliveryrDate.HasValue ? order.DeliveryrDate.Value : default(DateTime))
        );
        return element;
    }
}
