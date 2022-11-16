using DO;
using DalApi;
using System.Drawing;

namespace Dal;

/// class for Manage The order item database
public class DalOrederItem
{

    /// Function to add a new order item
    public void Add(OrderItem newOrderItem)
    {
        bool productExist = false;
        bool orderExist = false;
        ///Checking if the product exists in the database
        foreach (Product myProduct in DataSource.Products)
        {
            if(newOrderItem.ProductID == myProduct.Id)
                productExist = true; break;
        }
        ///Checking if the order exists in the database
        foreach (Order myOrder in DataSource.Orders)
        {
            if (newOrderItem.OredrID == myOrder.Id)
                orderExist = true; break;
        }
        if (!productExist)
            throw new EntityNotFound("Product");

        if (!orderExist)
            throw new EntityNotFound("order"); 

        ///Checking if the order is full 
        if (OrderItemsListByOrder(newOrderItem.OredrID).Count() >= 4)
            throw new StorgeIsFull("order");

        IEnumerator<OrderItem> iter = OrderItemsListByOrder(newOrderItem.OredrID).GetEnumerator();

        while (iter.MoveNext()){ 

            if (iter.Current.ProductID == newOrderItem.ProductID)
                throw new AllreadyExist("product");
        };

        ///Checking if the orderitem database is full 
        if (DataSource.OrderItems.Count >= 200)
            throw new StorgeIsFull("order items");
        else
        {
            newOrderItem.Id = DataSource.GetOrderItem;
            DataSource.OrderItems.Add(newOrderItem);
        }
          
    }
    ///Function to delete an order item
    public void Delete(int idToDelete)
    {
        if (DataSource.OrderItems.Count == 0)
            throw new StorgeIsEmpty("order items");

        foreach (OrderItem myOrderItem in DataSource.OrderItems)
        {
            if (idToDelete == myOrderItem.Id)
            {
                /// Replaces with the last one and lowers the size of the array
                DataSource.OrderItems.Remove(myOrderItem);
                break;
            }
        }
        throw new EntityNotFound("order item");
    }
    ///Function to update an order item
    public void Update(OrderItem newOrderItem)
    {
        foreach (OrderItem myOrderItem in DataSource.OrderItems)
        {
            if (newOrderItem.Id == myOrderItem.Id)
            {
                newOrderItem.OredrID = myOrderItem.OredrID;
                DataSource.OrderItems.Remove(myOrderItem);
                DataSource.OrderItems.Add(newOrderItem);
                return;         
            }
        }
        throw new EntityNotFound("id");
    }
    /// A function that returns an order item by id
    public OrderItem Get(int idToGet)
    {
        foreach (OrderItem myOrderItem in DataSource.OrderItems)
        {
            if (idToGet == myOrderItem.Id)
            {
                return myOrderItem;
            }
        }
        throw new EntityNotFound("Order item");
    }
    /// A function that returns an array of the order items in the database
    public IEnumerable<OrderItem> List()
    {
        var orderItemToPrint = new List<OrderItem>();
        foreach (OrderItem myOrderItem in DataSource.OrderItems)
        {
            orderItemToPrint.Add(myOrderItem);

        }
        return orderItemToPrint;
    }
    ///A function that returns an order item by prodact and order id;
    public OrderItem GetOrderItemByOrderAndProductId(int orderId, int productId)
    {
        foreach (OrderItem myOrderItem in DataSource.OrderItems)
        {
            if (orderId == myOrderItem.OredrID && productId == myOrderItem.ProductID)
            {
                return myOrderItem;
            }
        }
        throw new EntityNotFound("Order item");
    }
    /// A function that returns an array of the order items by order id
    public IEnumerable<OrderItem> OrderItemsListByOrder(int orderId)
    {
        var orderItemToPrint = new List<OrderItem>();
        foreach (OrderItem myOrderItem in DataSource.OrderItems)
        {
            if (orderId == myOrderItem.OredrID)
            {
                orderItemToPrint.Add(myOrderItem);   
            }

        }
        return orderItemToPrint;
    }
}
