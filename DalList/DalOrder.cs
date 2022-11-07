
using DO;

namespace Dal;

// class for Manage The order database
public class DalOrder
{

    // Function to add a new order
    public static int AddOrder(Order newOrder)
    {
        newOrder.Id = DataSource.GetOrder;
        if (DataSource.NextOrder == 100)
            throw new Exception("the storge of order is full\n");
        else
            DataSource.Orders[DataSource.NextOrder++] = newOrder;

        return newOrder.Id;
    }
    //Function to delete an order
    public static void DeleteOrder(int idToDelete)
    {
        for (int i = 0; i < DataSource.NextOrder; i++)
        {
            if (idToDelete == DataSource.Orders[i].Id)
            {
                if (DataSource.NextOrder == 0)
                    throw new Exception("the storge of order is empty\n");
                else
                {
                    // Replaces with the last one and lowers the size of the array
                    Order temp = DataSource.Orders[i];
                    DataSource.Orders[i] = DataSource.Orders[DataSource.NextOrder - 1];
                    DataSource.Orders[DataSource.NextOrder - 1] = temp;
                    DataSource.NextOrder--;
                }
                break;
            }
        }
    }
    //Function to update an order
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
    // A function that returns an order by id
    public static Order GetOrder(int idToGet)
    {
        for (int i = 0; i < DataSource.NextOrder; i++)
        {
            if (idToGet == DataSource.Orders[i].Id)
            {
                return DataSource.Orders[i];
            }
        }
        throw new Exception("the Order is not exist\n");
    }
    // A function that returns an array of the orders in the database
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
