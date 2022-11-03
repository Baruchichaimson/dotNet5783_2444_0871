
using DO;

namespace Dal;

public class DalOrederItem
{
    public static int AddOrderItem(OrderItem NewOrderItem)
    {
        for (int i = 0; i < DataSource.Config.NextOrderItem; i++)
        {
            if (NewOrderItem.Id == DataSource.OrderItems[i].Id)
                throw new Exception("the id is allready exist");
        }
        DataSource.OrderItems[DataSource.Config.NextOrderItem++] = NewOrderItem;

        return NewOrderItem.Id;
    }
    public static void DeleteOrderItem(int IDToDelete)
    {
        for (int i = 0; i < DataSource.Config.NextOrderItem; i++)
        {
            if (IDToDelete == DataSource.Orders[i].Id)
            {
                OrderItem Temp = DataSource.OrderItems[i];
                DataSource.OrderItems[i] = DataSource.OrderItems[DataSource.Config.NextOrderItem - 1];
                DataSource.OrderItems[DataSource.Config.NextOrderItem - 1] = Temp;
                DataSource.Config.NextOrderItem--;
                break;
            }
        }
    }
    public static void UpdateOrderItem(OrderItem newOrderItem)
    {
        for (int i = 0; i < DataSource.Config.NextOrderItem; i++)
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
        for (int i = 0; i < DataSource.Config.NextOrderItem; i++)
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
        OrderItem[] orderItemsList = new OrderItem[DataSource.Config.NextOrderItem];
        for (int i = 0; i < DataSource.Config.NextOrderItem; i++)
        {
            orderItemsList[i] = DataSource.OrderItems[i];
        }
        return orderItemsList;
    }
    public static OrderItem GetOrderItem(int OrderId, int ProductId)
    {
        for (int i = 0; i < DataSource.Config.NextOrderItem; i++)
        {
            if(OrderId == DataSource.OrderItems[i].OredrID && ProductId == DataSource.OrderItems[i].ProductID)
            {
                return DataSource.OrderItems[i];
            }
        }
        throw new Exception("the order item is not exist");
    }
    public static OrderItem[] OrderItemsList(int OrderId)
    {
        OrderItem[] OrderItemInOrder = new OrderItem[4];
        int CounterOfItems = 0;
        for (int i = 0; i < DataSource.Config.NextOrderItem; i++)
        {
           if(OrderId == DataSource.OrderItems[i].OredrID)
            {
                OrderItemInOrder[CounterOfItems++] = DataSource.OrderItems[i];
            }
        }
        return OrderItemInOrder;
    }
}
