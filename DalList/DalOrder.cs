using DO;
using DalApi;
using System.Runtime.CompilerServices;
//using System.Security.Principal;
namespace Dal;
/// <summary>
/// class for Manage The order database
/// </summary>
internal class DalOrder : IOrder
{
    /// <summary>
    /// Function to add a new order
    /// </summary>
    /// <param name="newOrder"> order to add</param>
    /// <returns>the new order id</returns>
    public int Add(Order newOrder)
    {
        newOrder.Id = DataSource.GetOrder;
        DataSource.Orders.Add(newOrder);
        return newOrder.Id;
    }
    ///<summary>
    /// Function to delete an order
    /// </summary>
    /// <param name="idToDelete"> order id for delete</param>
    public void Delete(int idToDelete)
    { 
        DataSource.Orders.Remove(GetElement(element => element?.Id == idToDelete));
    }
    ///<summary>
    /// Function to update an order
    /// </summary>
    /// <param name="newOrder"> order with the new details</param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Order newOrder)
    {     
        DataSource.Orders.Remove(GetElement(element => element?.Id == newOrder.Id));
        DataSource.Orders.Add(newOrder);
    }
    /// <summary>
    /// A function that returns an order by id
    /// </summary>
    /// <param name="idToGet"> order id </param>
    /// <returns>order</returns>
    public Order Get(int idToGet)
    {
        return GetElement(element => element?.Id == idToGet);
    }
    /// <summary>
    /// A function for getting a IEnumerable of the database list
    /// </summary>
    /// <param name="myFunc"> a condition delegate for filtering the list</param>
    /// <returns>IEnumerable</returns>
    public IEnumerable<Order?> List(Func<Order?, bool>? myFunc = null)
    {
        if (myFunc is null)
            return DataSource.Orders.Select(order => order);
        else
            return DataSource.Orders.Where(myFunc); 
    }
    /// <summary>
    /// A function for getting a element from the database
    /// </summary>
    /// <param name="myFunc">a condition delegate for a certain element</param>
    /// <returns> one item </returns>
    /// <exception cref="NullExeption"> if the item is null</exception>
    public Order GetElement(Func<Order?, bool>? myFunc)
    {
        if (myFunc is null)
        {
            throw new NullExeption("order");
        }
        Order order = DataSource.Orders.FirstOrDefault(myFunc) ?? throw new NullExeption("order"); 
        return order;
    }
}
