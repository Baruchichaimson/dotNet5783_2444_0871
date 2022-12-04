
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
        foreach (Order myOrder in DataSource.Orders)
        {
            if (idToDelete == myOrder.Id)
            {
                /// Replaces with the last one and lowers the size of the array
                DataSource.Orders.Remove(myOrder);
                return;
              
            }
        }
        throw new EntityNotFoundException("order");
    }
    ///Function to update an order
    public void Update(Order newOrder)
    {
        foreach(Order myOrder in DataSource.Orders)
        {
            if (newOrder.Id == myOrder.Id)
            {
                DataSource.Orders.Remove(myOrder);
                DataSource.Orders.Add(newOrder);
                return;
            }
        }
        throw new EntityNotFoundException("order");
    }
    /// A function that returns an order by id
    public Order? Get(int idToGet)
    {
        foreach(Order? myOrder in DataSource.Orders)
        {
            if (idToGet == myOrder.Id)
            {
                return myOrder;
            }
        }
        throw new EntityNotFoundException("order");
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
        Order? order = DataSource.Orders.FirstOrDefault(myFunc);
        if (order == null)
            throw new EntityNotFoundException("order");
        return order;
    }
}
