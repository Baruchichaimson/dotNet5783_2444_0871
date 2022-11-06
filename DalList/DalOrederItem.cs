using DO;
namespace Dal;
public class DalOrederItem
{
   
    public static int AddOrderItem(OrderItem NewOrderItem)
    {
        NewOrderItem.Id = DataSource.GetOrderItem;
        bool exist = false;
        for(int i = 0; i < DataSource.Products.Length; i++)
        {
            if(NewOrderItem.ProductID == DataSource.Products[i].Id)
            {
                exist = true; break;
            }
        }
        if (!exist)
        {
            throw new Exception("Product id does not exist");
        }
        if(OrderItemsListByOrder(NewOrderItem.OredrID).Length >= 4)
        {
            throw new Exception("too much items in order");
        }
        if (DataSource.NextOrderItem == 200)
            throw new Exception("the storge of orderitems is full");
        else
            DataSource.OrderItems[DataSource.NextOrderItem++] = NewOrderItem;

        return NewOrderItem.Id;
    }
    public static void DeleteOrderItem(int IDToDelete)
    {
        if (DataSource.NextOrderItem == 0)
            throw new Exception("the storge of orderitems is empty");

        for (int i = 0; i < DataSource.NextOrderItem; i++)
        {
            if (IDToDelete == DataSource.Orders[i].Id)
            {
                    OrderItem Temp = DataSource.OrderItems[i];
                    DataSource.OrderItems[i] = DataSource.OrderItems[DataSource.NextOrderItem - 1];
                    DataSource.OrderItems[DataSource.NextOrderItem - 1] = Temp;
                    DataSource.NextOrderItem--;
                break;
            }
        }
    }
    public static void UpdateOrderItem(OrderItem newOrderItem)
    {
        for (int i = 0; i < DataSource.NextOrderItem; i++)
        {
            if (newOrderItem.Id == DataSource.OrderItems[i].Id)
            {
                DataSource.OrderItems[i] = newOrderItem;
                break;
            }
        }
        throw new Exception("the id is not exist");
    }
    public static OrderItem GetOrderItem(int IDToGet)
    {
        for (int i = 0; i < DataSource.NextOrderItem; i++)
        {
            if (IDToGet == DataSource.OrderItems[i].Id)
            {
                return DataSource.OrderItems[i];
            }
        }
        throw new Exception("the OrderItem is not exist");
    }
    public static OrderItem[] OrderItemsList()
    {
        OrderItem[] orderItemsList = new OrderItem[DataSource.NextOrderItem];
        for (int i = 0; i < DataSource.NextOrderItem; i++)
        {
            orderItemsList[i] = DataSource.OrderItems[i];
        }
        return orderItemsList;
    }
    public static OrderItem GetOrderItemByOrderAndProductId(int OrderId, int ProductId)
    {
        for (int i = 0; i < DataSource.NextOrderItem; i++)
        {
            if(OrderId == DataSource.OrderItems[i].OredrID && ProductId == DataSource.OrderItems[i].ProductID)
            {
                return DataSource.OrderItems[i];
            }
        }
        throw new Exception("the order item is not exist");
    }
    public static OrderItem[] OrderItemsListByOrder(int OrderId)
    {
        
        int CounterOfItems = 0;
        for (int i = 0; i < DataSource.NextOrderItem; i++)
        {
           if(OrderId == DataSource.OrderItems[i].OredrID)
            {
                CounterOfItems++;
            }
        }
        OrderItem[] OrderItemInOrder = new OrderItem[CounterOfItems];
        CounterOfItems = 0;
        for (int i = 0; i < DataSource.NextOrderItem; i++)
        {
            if (OrderId == DataSource.OrderItems[i].OredrID)
            {
                OrderItemInOrder[CounterOfItems++] = DataSource.OrderItems[i];
            }
        }
        return OrderItemInOrder;
    }
}
