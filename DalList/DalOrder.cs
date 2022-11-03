
using DO;

namespace Dal;

public class DalOrder
{
    public static int AddOrder(Order NewOrder)
    {
        for (int i = 0; i < DataSource.Config.NextOrder; i++)
        {
            if (NewOrder.Id == DataSource.Orders[i].Id)
                throw new Exception("the id is allready exist");
        }
        DataSource.Orders[DataSource.Config.NextOrder++] = NewOrder;

        return NewOrder.Id;
    }
    public static void DeleteOrder(int IDToDelete)
    {
        for (int i = 0; i < DataSource.Config.NextOrder; i++)
        {
            if (IDToDelete == DataSource.Orders[i].Id)
            {
                Order Temp = DataSource.Orders[i];
                DataSource.Orders[i] = DataSource.Orders[DataSource.Config.NextOrder - 1];
                DataSource.Orders[DataSource.Config.NextOrder - 1] = Temp;
                DataSource.Config.NextOrder--;
                break;
            }
        }
    }
    public static void UpdateProduct(Order newOrder)
    {
        for (int i = 0; i < DataSource.Config.NextOrder; i++)
        {
            if (newOrder.Id == DataSource.Orders[i].Id)
            {
                DataSource.Orders[i] = newOrder;
                break;
            }
        }
        throw new Exception("the id is not exist");
    }
    public static Order GetOrder(int IDToGet)
    {
        for (int i = 0; i < DataSource.Config.NextOrder; i++)
        {
            if (IDToGet == DataSource.Orders[i].Id)
            {
                return DataSource.Orders[i];
            }
        }
        throw new Exception("the Order is not exist");
    }
    public static Order[] OrderList()
    {
        Order[] orderList = new Order[DataSource.Config.NextOrder];
        for (int i = 0; i < DataSource.Config.NextOrder; i++)
        {
            orderList[i] = DataSource.Orders[i];
        }
        return orderList;
    }
}
