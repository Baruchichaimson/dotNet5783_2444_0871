using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal
{
    internal class DalOrederItem : IOrderItem
    {
        const string s_orderItem = @"OrderItems";
        const string s_product = @"Products";
        const string s_order = @"Orders";
        const string s_idConfig = @"ConfigId";
        const string itemId = @"orderItemId";       
        XElement configId = File.Exists(XMLTools.GetDir() + $"{s_idConfig}.xml") ? XElement.Load(XMLTools.GetDir() + $"{s_idConfig}.xml") : throw new NullExeption($"{s_idConfig}");
        public int Add(OrderItem orderItem)
        {
            List<OrderItem?> orderItems = XMLTools.LoadListFromXMLSerializer<OrderItem>(s_orderItem);
            List<Product?> products = XMLTools.LoadListFromXMLSerializer<Product>(s_product);
            List<Order?> orders = XMLTools.LoadListFromXMLSerializer<Order>(s_order);

            if (!products.Exists(element => element?.Id == orderItem.ProductID))
                throw new EntityNotFoundException("Product");
            if (!orders.Exists(element => element?.Id == orderItem.OredrID))
                throw new EntityNotFoundException("order");

            if (List(element => element?.OredrID == orderItem.OredrID)?.ToList().Exists(element => element?.ProductID == orderItem.ProductID) ?? throw new NullExeption("order item list"))
                throw new AllreadyExistException("product in order");

            orderItem.Id = Convert.ToInt32(configId.Element(itemId)?.Value ?? "-1");
            configId.Element(itemId)!.Value = (orderItem.Id + 1).ToString();
            orderItems.Add(orderItem);
            XMLTools.SaveListToXMLSerializer(orderItems, s_orderItem);
            return orderItem.Id;
        }

        public void Delete(int idToDelete)
        {
            List<OrderItem?> orderItems = XMLTools.LoadListFromXMLSerializer<OrderItem>(s_orderItem);
            orderItems.Remove(GetElement(element => element?.Id == idToDelete));
            XMLTools.SaveListToXMLSerializer(orderItems, s_orderItem);
        }

        public OrderItem Get(int idToGet)
        {
            return GetElement(element => element?.Id == idToGet);
        }

        public OrderItem GetElement(Func<OrderItem?, bool>? myFunc = null)
        {
            List<OrderItem?> orderItems = XMLTools.LoadListFromXMLSerializer<OrderItem>(s_orderItem);
            if (myFunc is null)
            {
                throw new NullExeption("condition");
            }
            OrderItem orderItem = orderItems.FirstOrDefault(myFunc) ?? throw new NullExeption("order item");
            return orderItem;
        }

        public IEnumerable<OrderItem?>? List(Func<OrderItem?, bool>? myFunc = null)
        {
            List<OrderItem?> orderItems = XMLTools.LoadListFromXMLSerializer<OrderItem>(s_orderItem);
            if (myFunc is null)
                return orderItems.Select(orderItems => orderItems);
            else
                return orderItems.Where(myFunc!);
        }

        public void Update(OrderItem newOrderItem)
        {
            List<OrderItem?> orderItems = XMLTools.LoadListFromXMLSerializer<OrderItem>(s_orderItem);

            OrderItem orderItem = GetElement(element => element?.Id == newOrderItem.Id);
            newOrderItem.OredrID = orderItem.OredrID;
            orderItems.Remove(orderItem);
            orderItems.Add(newOrderItem);
            XMLTools.SaveListToXMLSerializer(orderItems, s_orderItem);

        }
    }
}
