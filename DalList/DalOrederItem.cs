﻿using DO;
using DalApi;
using System.Drawing;
namespace Dal;

/// class for Manage The order item database
internal class DalOrederItem :IOrderItem
{

    /// Function to add a new order item
    public int Add(OrderItem newOrderItem)
    {
        bool productExist = false;
        bool orderExist = false;
        ///Checking if the product exists in the database
        foreach (Product myProduct in DataSource.Products)
        {
            if(newOrderItem.ProductID == myProduct.Id)
            {
                productExist = true; break;
            }                
        }
        ///Checking if the order exists in the database
        foreach (Order myOrder in DataSource.Orders)
        {
            if (newOrderItem.OredrID == myOrder.Id)
            {
                orderExist = true; break;

            }    
        }
        if (!productExist)
            throw new EntityNotFoundException("Product");

        if (!orderExist)
            throw new EntityNotFoundException("order"); 

        IEnumerator<OrderItem> iter = OrderItemsListByOrder(newOrderItem.OredrID).GetEnumerator();

        while (iter.MoveNext()){ 

            if (iter.Current.ProductID == newOrderItem.ProductID)
                throw new AllreadyExistException("product");
        };

        newOrderItem.Id = DataSource.GetOrderItem;
        DataSource.OrderItems.Add(newOrderItem);
        return newOrderItem.Id;
    }
    ///Function to delete an order item
    public void Delete(int idToDelete)
    {
        foreach (OrderItem myOrderItem in DataSource.OrderItems)
        {
            if (idToDelete == myOrderItem.Id)
            {
                /// Replaces with the last one and lowers the size of the array
                DataSource.OrderItems.Remove(myOrderItem);
                return;
            }
        }
        throw new EntityNotFoundException("order item");
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
        throw new EntityNotFoundException("Order item");
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
        throw new EntityNotFoundException("Order item");
    }
    /// A function that returns an array of the order items in the database
    public IEnumerable<OrderItem?> List(Func<OrderItem?, bool>? myFunc = null)
    {
        var orderItemToPrint = new List<OrderItem?>();
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
        throw new EntityNotFoundException("Order item");
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
