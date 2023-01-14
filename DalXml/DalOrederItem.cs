using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal
{
    /// <summary>
    /// This class DalOrederItem implements the IOrderItem interface and contains methods for adding, deleting, getting and updating OrderItems from an XML file.
    /// The class uses the XMLTools and XmlLock classes to handle loading and saving data and thread safety.
    /// </summary>
    internal class DalOrederItem : IOrderItem
    {
        const string s_orderItem = @"OrderItems";
        const string s_product = @"Products";
        const string s_order = @"Orders";
        const string s_idConfig = @"ConfigId.xml";
        const string itemId = @"orderItemId";
        /// <summary>
        /// This method is responsible for adding a new OrderItem to an XML file. 
        /// It first checks if the product and order associated with the OrderItem exist, if not it throws an EntityNotFoundException.
        /// Then it verifies if the OrderItem already exists in the list, if yes it throws an AllreadyExistException.
        /// Next it assigns an Id for the OrderItem and updates the config file.
        /// Finally it adds the OrderItem to the list and saves the list to the XML file using the XMLTools class.
        /// It also uses the XmlLock class to handle thread safety.
        /// </summary>
        /// <param name="orderItem">The OrderItem to be added</param>
        /// <returns>The Id of the added OrderItem</returns>
        /// <exception cref="NullExeption">Thrown when the config file is not found</exception>
        /// <exception cref="EntityNotFoundException">Thrown when the associated Product or Order is not found</exception>
        /// <exception cref="AllreadyExistException">Thrown when the OrderItem already exists in the list</exception>

        public int Add(OrderItem orderItem)
        {
            // Load the order items, products, and orders from XML files
            List<OrderItem?> orderItems = XMLTools.LoadListFromXMLSerializer<OrderItem>(s_orderItem);
            List<Product?> products = XMLTools.LoadListFromXMLSerializer<Product>(s_product);
            List<Order?> orders = XMLTools.LoadListFromXMLSerializer<Order>(s_order);
            //Load the configId from XML file
            XElement configId = File.Exists(XMLTools.GetDir() + s_idConfig) ? XElement.Load(XMLTools.GetDir() + s_idConfig) : throw new NullExeption($"{s_idConfig}");

            //Check whether the product and order exist or not
            if (!products.Exists(element => element?.Id == orderItem.ProductID))
                throw new EntityNotFoundException("Product");
            if (!orders.Exists(element => element?.Id == orderItem.OredrID))
                throw new EntityNotFoundException("order");
            //Check whether order item already exists
            if (List(element => element?.OredrID == orderItem.OredrID)?.ToList().Exists(element => element?.ProductID == orderItem.ProductID) ?? throw new NullExeption("order item list"))
                throw new AllreadyExistException("product in order");
            lock (XmlLock.s_lock)
            {
                //Get the Id from Config file
                orderItem.Id = Convert.ToInt32(configId.Element(itemId)?.Value ?? "-1");
                //increment the id and update the config file
                configId.Element(itemId)!.Value = (orderItem.Id + 1).ToString();
                configId.Save(XMLTools.GetDir() + s_idConfig);
            }
            //Add the order item
            orderItems.Add(orderItem);
            //save the order item
            XMLTools.SaveListToXMLSerializer(orderItems, s_orderItem);
            return orderItem.Id;
        }
        /// <summary>
        /// This method is responsible for deleting an OrderItem from the XML file based on the provided id.
        /// It first loads the existing OrderItems from the XML file using the XMLTools class, then removes the OrderItem with the specified id
        /// and saves the updated list back to the XML file.
        /// </summary>
        /// <param name="idToDelete">The id of the OrderItem to be deleted</param>
        public void Delete(int idToDelete)
        {
            // Load the existing OrderItems from the XML file
            List<OrderItem?> orderItems = XMLTools.LoadListFromXMLSerializer<OrderItem>(s_orderItem);
            // Remove the OrderItem with the specified id
            orderItems.Remove(GetElement(element => element?.Id == idToDelete));
            // Save the updated list back to the XML file
            XMLTools.SaveListToXMLSerializer(orderItems, s_orderItem);
        }
        /// <summary>
        /// This method is responsible for retrieving an OrderItem from the XML file based on the provided id.
        /// It uses the GetElement method to find the OrderItem with the specified id and returns it.
        /// </summary>
        /// <param name="idToGet">The id of the OrderItem to be retrieved</param>
        /// <returns>The OrderItem with the specified id</returns>
        public OrderItem Get(int idToGet)
        {
            // Use the GetElement method to find the OrderItem with the specified id
            return GetElement(element => element?.Id == idToGet);
        }
        /// <summary>
        /// This method is responsible for finding and returning a single OrderItem from the XML file based on the provided search condition.
        /// It loads the existing OrderItems from the XML file using the XMLTools class, then uses the provided search condition to find the OrderItem
        /// and returns it. If no search condition is provided it throws a NullException.
        /// </summary>
        /// <param name="myFunc">The search condition used to find the OrderItem</param>
        /// <returns>The found OrderItem</returns>
        /// <exception cref="NullExeption">Thrown when no search condition is provided</exception>
        public OrderItem GetElement(Func<OrderItem?, bool>? myFunc = null)
        {
            // Load the existing OrderItems from the XML file
            List<OrderItem?> orderItems = XMLTools.LoadListFromXMLSerializer<OrderItem>(s_orderItem);

            // Check if a search condition was provided
            if (myFunc is null)
            {
                throw new NullExeption("condition");
            }
            OrderItem orderItem = orderItems.FirstOrDefault(myFunc) ?? throw new NullExeption("order item");
            return orderItem;
        }
        /// <summary>
        /// This method is responsible for finding and returning a list of OrderItems from the XML file based on the provided search condition.
        /// It loads the existing OrderItems from the XML file using the XMLTools class, then uses the provided search condition to find the OrderItems
        /// and returns them. If no search condition is provided, it returns all the OrderItems.
        /// </summary>
        /// <param name="myFunc">The search condition used to find the OrderItems</param>
        /// <returns>A list of found OrderItems</returns>
        public IEnumerable<OrderItem?>? List(Func<OrderItem?, bool>? myFunc = null)
        {
            // Load the existing OrderItems from the XML file

            List<OrderItem?> orderItems = XMLTools.LoadListFromXMLSerializer<OrderItem>(s_orderItem);
            // Check if a search condition was provided

            if (myFunc is null)
                return orderItems.Select(orderItems => orderItems);
            else
                return orderItems.Where(myFunc!);
        }
        /// <summary>
        /// This method is responsible for updating an existing OrderItem in the XML file with the provided new OrderItem.
        /// It loads the existing OrderItems from the XML file using the XMLTools class, then uses the GetElement method to find the existing OrderItem
        /// and removes it from the list. Then it adds the new OrderItem to the list and saves the list back to the XML file.
        /// </summary>
        /// <param name="newOrderItem">The new OrderItem to replace the existing one</param>
        public void Update(OrderItem newOrderItem)
        {
            // Load the existing OrderItems from the XML file
            List<OrderItem?> orderItems = XMLTools.LoadListFromXMLSerializer<OrderItem>(s_orderItem);

            // Find the existing OrderItem and remove it
            OrderItem orderItem = GetElement(element => element?.Id == newOrderItem.Id);
            newOrderItem.OredrID = orderItem.OredrID;
            orderItems.Remove(orderItem);

            // Add the new OrderItem to the list and save the list back to the XML file
            orderItems.Add(newOrderItem);
            XMLTools.SaveListToXMLSerializer(orderItems, s_orderItem);

        }
    }
}
