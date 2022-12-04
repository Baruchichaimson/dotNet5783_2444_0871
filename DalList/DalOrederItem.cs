using DO;
using DalApi;
using System.Drawing;
using System.Linq;

namespace Dal;

/// class for Manage The order item database
internal class DalOrederItem :IOrderItem
{

    /// Function to add a new order item
    public int Add(OrderItem newOrderItem)
    {
        ///Checking if the product exists in the database
       if(!DataSource.Products.Exists(element => element!.Value.Id == newOrderItem.ProductID))
             throw new EntityNotFoundException("Product");
        if (!DataSource.Orders.Exists(element => element!.Value.Id == newOrderItem.OredrID))
            throw new EntityNotFoundException("order"); 

        if(List(element => element!.Value.OredrID == newOrderItem.OredrID).ToList().Exists(element => element!.Value.ProductID == newOrderItem.ProductID))
            throw new AllreadyExistException("product in order");

        newOrderItem.Id = DataSource.GetOrderItem;
        DataSource.OrderItems.Add(newOrderItem);
        return newOrderItem.Id;
    }
    ///Function to delete an order item
    public void Delete(int idToDelete)
    {
        DataSource.OrderItems.Remove(GetElement(element => element!.Value.Id == idToDelete));   
    }
    ///Function to update an order item
    public void Update(OrderItem newOrderItem)
    {
        OrderItem? orderItem = GetElement(element => element!.Value.Id == newOrderItem.Id);
       newOrderItem.OredrID = orderItem!.Value.OredrID;
       DataSource.OrderItems.Remove(orderItem);  
       DataSource.OrderItems.Add(newOrderItem);
    }
    /// A function that returns an order item by id
    public OrderItem? Get(int idToGet)
    {
       return GetElement(element => element!.Value.Id == idToGet);
    }
    /// A function that returns an array of the order items in the database
    public IEnumerable<OrderItem?> List(Func<OrderItem?, bool>? myFunc = null)
    {
        bool flag = myFunc is null;
        if (flag)
            return DataSource.OrderItems.Select(orderItems => orderItems);
        else
            return DataSource.OrderItems.Where(myFunc!);
    }
    public OrderItem? GetElement(Func<OrderItem?, bool>? myFunc)
    {
        if (myFunc is null)
        {
            throw new EntityNotFoundException("order item");
        }
        OrderItem? orderItem = DataSource.OrderItems.FirstOrDefault(myFunc);
        if (orderItem == null)
            throw new EntityNotFoundException("order item");
        return orderItem;
    }
}
