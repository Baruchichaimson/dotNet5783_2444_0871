
using DO;
using DalApi;
using System.Linq;
//using System.Security.Principal;
namespace Dal;

/// class for Manage The order database
internal class DalOrder : IOrder
{
    /// Function to add a new order
    public int Add(Order newOrder)
    {
        newOrder.Id = DataSource.GetOrder;
        DataSource.Orders.Add(newOrder);
        return newOrder.Id;
    }
    ///Function to delete an order
    public void Delete(int idToDelete)
    { 
        DataSource.Orders.Remove(GetElement(element => element?.Id == idToDelete));
    }
    ///Function to update an order
    public void Update(Order newOrder)
    {     
        DataSource.Orders.Remove(GetElement(element => element?.Id == newOrder.Id));
        DataSource.Orders.Add(newOrder);
    }
    /// A function that returns an order by id
    public Order? Get(int idToGet)
    {
        return GetElement(element => element?.Id == idToGet);
    }
    /// A function that returns an array of the orders in the database
    public IEnumerable<Order?> List(Func<Order?, bool>? myFunc = null)
    {
       bool flag = myFunc is null;
        if (flag)
            return DataSource.Orders.Select(order => order);
        else
            return DataSource.Orders.Where(myFunc); 
    }
    public Order? GetElement(Func<Order?, bool>? myFunc)
    {
        if (myFunc is null)
        {
            throw new EntityNotFoundException("order");
        }
        Order? order = DataSource.Orders.FirstOrDefault(myFunc);
        if (order == null)
            throw new EntityNotFoundException("order");
        return order;
    }
}
