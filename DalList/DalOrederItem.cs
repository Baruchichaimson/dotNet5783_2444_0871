using DO;
using DalApi;
using System.Drawing;

namespace Dal;

/// class for Manage The order item database
public class DalOrederItem
{

    /// Function to add a new order item
    public int Add(OrderItem newOrderItem)
    {
        newOrderItem.Id = DataSource.GetOrderItem;
        bool productExist = false;
        bool orderExist = false;
        ///Checking if the product exists in the database
        for (int i = 0; i < DataSource.Products.Length; i++)
        {
            if(newOrderItem.ProductID == DataSource.Products[i].Id)
                productExist = true; break;
        }
        ///Checking if the order exists in the database
        for (int i = 0; i < DataSource.Orders.Length; i++)
        {
            if (newOrderItem.OredrID == DataSource.Orders[i].Id)
                orderExist = true; break;
        }
        if (!productExist)
            throw new EntityNotFound("Product");

        if (!orderExist)
            throw new EntityNotFound("order"); 

        ///Checking if the order is full 
        if (OrderItemsListByOrder(newOrderItem.OredrID).Length >= 4)
            throw new StorgeIsFull("order");

        for(int i = 0; i < OrderItemsListByOrder(newOrderItem.OredrID).Length; i++)
        {
            if (OrderItemsListByOrder(newOrderItem.OredrID)[i].ProductID == newOrderItem.ProductID)
                throw new AllreadyExist("product");
        }
        ///Checking if the orderitem database is full 
        if (DataSource.NextOrderItem == 200)
            throw new StorgeIsFull("order items");
        else
            DataSource.OrderItems[DataSource.NextOrderItem++] = newOrderItem;

        return newOrderItem.Id;
    }
    ///Function to delete an order item
    public void Delete(int idToDelete)
    {
        if (DataSource.NextOrderItem == 0)
            throw new StorgeIsEmpty("order items");

        for (int i = 0; i < DataSource.NextOrderItem; i++)
        {
            if (idToDelete == DataSource.OrderItems[i].Id)
            {
               /// Replaces with the last one and lowers the size of the array
                    OrderItem temp = DataSource.OrderItems[i];
                    DataSource.OrderItems[i] = DataSource.OrderItems[DataSource.NextOrderItem - 1];
                    DataSource.OrderItems[DataSource.NextOrderItem - 1] = temp;
                    DataSource.NextOrderItem--;
                break;
            }
        }
    }
    ///Function to update an order item
    public void Update(OrderItem newOrderItem)
    {
        for (int i = 0; i < DataSource.NextOrderItem; i++)
        {
            if (newOrderItem.Id == DataSource.OrderItems[i].Id)
            {
                newOrderItem.OredrID = DataSource.OrderItems[i].OredrID;
                DataSource.OrderItems[i] = newOrderItem;
                return;         
            }
        }
        throw new EntityNotFound("id");
    }
    /// A function that returns an order item by id
    public OrderItem Get(int idToGet)
    {
        for (int i = 0; i < DataSource.NextOrderItem; i++)
        {
            if (idToGet == DataSource.OrderItems[i].Id)
            {
                return DataSource.OrderItems[i];
            }
        }
        throw new EntityNotFound("Order item");
    }
    /// A function that returns an array of the order items in the database
    public OrderItem[] List()
    {
        OrderItem[] orderItemsList = new OrderItem[DataSource.NextOrderItem];
        for (int i = 0; i < DataSource.NextOrderItem; i++)
        {
            orderItemsList[i] = DataSource.OrderItems[i];
        }
        return orderItemsList;
    }
    ///A function that returns an order item by prodact and order id;
    public OrderItem GetOrderItemByOrderAndProductId(int orderId, int productId)
    {
        for (int i = 0; i < DataSource.NextOrderItem; i++)
        {
            if(orderId == DataSource.OrderItems[i].OredrID && productId == DataSource.OrderItems[i].ProductID)
            {
                return DataSource.OrderItems[i];
            }
        }
        throw new EntityNotFound("order item");
    }
    /// A function that returns an array of the order items by order id
    public OrderItem[] OrderItemsListByOrder(int orderId)
    {
        
        int counterOfItems = 0;
        for (int i = 0; i < DataSource.NextOrderItem; i++)
        {
           if(orderId == DataSource.OrderItems[i].OredrID)
            {
                counterOfItems++;
            }
        }
        OrderItem[] orderItemInOrder = new OrderItem[counterOfItems];
        counterOfItems = 0;
        for (int i = 0; i < DataSource.NextOrderItem; i++)
        {
            if (orderId == DataSource.OrderItems[i].OredrID)
            {
                orderItemInOrder[counterOfItems++] = DataSource.OrderItems[i];
            }

        }
        return orderItemInOrder;
    }
}
