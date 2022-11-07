using DO;
using System.Drawing;

namespace Dal;

// class for Manage The order item database
public class DalOrederItem
{

    // Function to add a new order item
    public static int AddOrderItem(OrderItem newOrderItem)
    {
        newOrderItem.Id = DataSource.GetOrderItem;
        bool productExist = false;
        bool orderExist = false;
        //Checking if the product exists in the database
        for (int i = 0; i < DataSource.Products.Length; i++)
        {
            if(newOrderItem.ProductID == DataSource.Products[i].Id)
                productExist = true; break;
        }
        //Checking if the order exists in the database
        for (int i = 0; i < DataSource.Orders.Length; i++)
        {
            if (newOrderItem.OredrID == DataSource.Orders[i].Id)
                orderExist = true; break;
        }
        if (!productExist)
           throw new Exception("Product id does not exist\n");
        
        if (!orderExist)
            throw new Exception("Order id does not exist\n");

        //Checking if the order is full 
        if (OrderItemsListByOrder(newOrderItem.OredrID).Length >= 4)
            throw new Exception("too much items in order\n");

        for(int i = 0; i < OrderItemsListByOrder(newOrderItem.OredrID).Length; i++)
        {
            if (OrderItemsListByOrder(newOrderItem.OredrID)[i].ProductID == newOrderItem.ProductID)
                throw new Exception("the product is allready exist in the order\n");
        }
        //Checking if the orderitem database is full 
        if (DataSource.NextOrderItem == 200)
            throw new Exception("the storge of orderitems is full\n");
        else
            DataSource.OrderItems[DataSource.NextOrderItem++] = newOrderItem;

        return newOrderItem.Id;
    }
    //Function to delete an order item
    public static void DeleteOrderItem(int idToDelete)
    {
        if (DataSource.NextOrderItem == 0)
            throw new Exception("the storge of orderitems is empty\n");

        for (int i = 0; i < DataSource.NextOrderItem; i++)
        {
            if (idToDelete == DataSource.OrderItems[i].Id)
            {
               // Replaces with the last one and lowers the size of the array
                    OrderItem temp = DataSource.OrderItems[i];
                    DataSource.OrderItems[i] = DataSource.OrderItems[DataSource.NextOrderItem - 1];
                    DataSource.OrderItems[DataSource.NextOrderItem - 1] = temp;
                    DataSource.NextOrderItem--;
                break;
            }
        }
    }
    //Function to update an order item
    public static void UpdateOrderItem(OrderItem newOrderItem)
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
        throw new Exception("the id is not exist");
    }
    // A function that returns an order item by id
    public static OrderItem GetOrderItem(int idToGet)
    {
        for (int i = 0; i < DataSource.NextOrderItem; i++)
        {
            if (idToGet == DataSource.OrderItems[i].Id)
            {
                return DataSource.OrderItems[i];
            }
        }
        throw new Exception("the OrderItem is not exist");
    }
    // A function that returns an array of the order items in the database
    public static OrderItem[] OrderItemsList()
    {
        OrderItem[] orderItemsList = new OrderItem[DataSource.NextOrderItem];
        for (int i = 0; i < DataSource.NextOrderItem; i++)
        {
            orderItemsList[i] = DataSource.OrderItems[i];
        }
        return orderItemsList;
    }
    //A function that returns an order item by prodact and order id;
    public static OrderItem GetOrderItemByOrderAndProductId(int orderId, int productId)
    {
        for (int i = 0; i < DataSource.NextOrderItem; i++)
        {
            if(orderId == DataSource.OrderItems[i].OredrID && productId == DataSource.OrderItems[i].ProductID)
            {
                return DataSource.OrderItems[i];
            }
        }
        throw new Exception("the order item is not exist");
    }
    // A function that returns an array of the order items by order id
    public static OrderItem[] OrderItemsListByOrder(int orderId)
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
