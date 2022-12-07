using DO;
using DalApi;
using System.Drawing;
using System.Linq;
using System;

namespace Dal;

/// <summary>
/// class for Manage The order item database
/// </summary>
internal class DalOrederItem :IOrderItem
{

    /// <summary>
    /// Function to add a new order item
    /// </summary>
    /// <param name="newOrderItem"> ordwr item</param>
    /// <returns>the new order item id</returns>
    /// <exception cref="EntityNotFoundException"> when not in database</exception>
    /// <exception cref="NullExeption"> when the list is null</exception>
    /// <exception cref="AllreadyExistException">when exist in one order</exception>
    public int Add(OrderItem newOrderItem)
    {
        ///Checking if the product exists in the database
       if(!DataSource.Products.Exists(element => element?.Id == newOrderItem.ProductID))
             throw new EntityNotFoundException("Product");
        if (!DataSource.Orders.Exists(element => element?.Id == newOrderItem.OredrID))
            throw new EntityNotFoundException("order"); 

        if(List(element => element?.OredrID == newOrderItem.OredrID)?.ToList().Exists(element => element?.ProductID == newOrderItem.ProductID) ?? throw new NullExeption("order item list"))
            throw new AllreadyExistException("product in order");

        newOrderItem.Id = DataSource.GetOrderItem;
        DataSource.OrderItems.Add(newOrderItem);
        return newOrderItem.Id;
    }
    ///<summary>
    /// Function to delete an order item
    /// </summary>
    /// <param name="idToDelete">order item for delete</param>
    public void Delete(int idToDelete)
    {
        DataSource.OrderItems.Remove(GetElement(element => element?.Id == idToDelete));   
    }
    /// <summary>
    /// Function to update an order item
    /// </summary>
    /// <param name="newOrderItem">order item with new datails</param>
    public void Update(OrderItem newOrderItem)
    {
       OrderItem orderItem = GetElement(element => element?.Id == newOrderItem.Id);
       newOrderItem.OredrID = orderItem.OredrID;
       DataSource.OrderItems.Remove(orderItem);  
       DataSource.OrderItems.Add(newOrderItem);
    }
    ///<summary>
    /// A function that returns an order item by id
    /// </summary>
    /// <param name="idToGet">order item id</param>
    /// <returns> order item</returns>
    public OrderItem Get(int idToGet)
    {
       return GetElement(element => element?.Id == idToGet);
    }
    /// <summary>
    /// A function for getting a IEnumerable of the database list
    /// </summary>
    /// <param name="myFunc"> a condition delegate for filtering the list</param>
    /// <returns>IEnumerable</returns>
    public IEnumerable<OrderItem?>? List(Func<OrderItem?, bool>? myFunc = null)
    {
        if (myFunc is null)
            return DataSource.OrderItems.Select(orderItems => orderItems);
        else
            return DataSource.OrderItems.Where(myFunc!);
    }
    /// <summary>
    /// A function for getting a element from the database
    /// </summary>
    /// <param name="myFunc">a condition delegate for a certain element</param>
    /// <returns> one item </returns>
    /// <exception cref="NullExeption"> if the item is null</exception>
    public OrderItem GetElement(Func<OrderItem?, bool>? myFunc)
    {
        if (myFunc is null)
        {
            throw new NullExeption("condition");
        }
        OrderItem orderItem = DataSource.OrderItems.FirstOrDefault(myFunc) ?? throw new NullExeption("order item");
        return orderItem;
    }
}
