using System;
using System.Collections.Generic;
namespace BO;

/// <summary>
///  the class is make the entity of the list order item and all what we need in the order item.
/// </summary>
public class OrderForList
{
    /// <summary>
    /// the order id 
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// customer name in the order
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// bring the status of the order.
    /// </summary>
    public OrderStatus? Status { get; set; }
    /// <summary>
    ///  the amount of item in order
    /// </summary>
    public int AmountOfItems { get; set; }
    /// <summary>
    /// the total price in order
    /// </summary>
    public double TotalPrice { get; set; }
    public override string ToString() => $@"
        Order ID: {ID}
        Customer Name: {CustomerName}
        Amount Of Items: {AmountOfItems}
        Total Price: {TotalPrice}
        status: {Status}";

}
