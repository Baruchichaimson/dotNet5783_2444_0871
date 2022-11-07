
using DO;

namespace Dal;

public class DalOrder
{
   
    public static int AddOrder(Order NewOrder)
    {
        NewOrder.Id = DataSource.GetOrder;
        if (DataSource.NextOrder == 100)
            throw new Exception("the storge of order is full\n");
        else
            DataSource.Orders[DataSource.NextOrder++] = NewOrder;

        return NewOrder.Id;
    }
    public static void DeleteOrder(int IDToDelete)
    {
        for (int i = 0; i < DataSource.NextOrder; i++)
        {
            if (IDToDelete == DataSource.Orders[i].Id)
            {
                if (DataSource.NextOrder == 0)
                    throw new Exception("the storge of order is empty\n");
                else
                {
                    Order Temp = DataSource.Orders[i];
                    DataSource.Orders[i] = DataSource.Orders[DataSource.NextOrder - 1];
                    DataSource.Orders[DataSource.NextOrder - 1] = Temp;
                    DataSource.NextOrder--;
                }
                break;
            }
        }
    }
    public static void UpdateOrder(Order newOrder)
    {
        bool exist = false;
        for (int i = 0; i < DataSource.NextOrder; i++)
        {
            if (newOrder.Id == DataSource.Orders[i].Id)
            {
                DataSource.Orders[i] = newOrder;
                exist = true;
                break;
            }
        }
        if (!exist)
             throw new Exception("the id is not exist\n");
    }
    public static Order GetOrder(int IDToGet)
    {
        for (int i = 0; i < DataSource.NextOrder; i++)
        {
            if (IDToGet == DataSource.Orders[i].Id)
            {
                return DataSource.Orders[i];
            }
        }
        throw new Exception("the Order is not exist\n");
    }
    public static Order[] OrderList()
    {
        Order[] orderList = new Order[DataSource.NextOrder];
        for (int i = 0; i < DataSource.NextOrder; i++)
        {
            orderList[i] = DataSource.Orders[i];
        }
        return orderList;
    }
}
