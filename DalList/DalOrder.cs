
using DO;
using DalApi;

namespace Dal;
/// class for Manage The order database
public class DalOrder
{

    /// Function to add a new order
    public int Add(Order newOrder)
    {
        newOrder.Id = DataSource.GetOrder;
        if (DataSource.NextOrder == 100)
            throw new StorgeIsFull("order");
        else
            DataSource.Orders[DataSource.NextOrder++] = newOrder;

        return newOrder.Id;
    }
    ///Function to delete an order
    public void Delete(int idToDelete)
    {
        for (int i = 0; i < DataSource.NextOrder; i++)
        {
            if (idToDelete == DataSource.Orders[i].Id)
            {
                if (DataSource.NextOrder == 0)
                    throw new StorgeIsEmpty("order");
                else
                {
                    /// Replaces with the last one and lowers the size of the array
                    Order temp = DataSource.Orders[i];
                    DataSource.Orders[i] = DataSource.Orders[DataSource.NextOrder - 1];
                    DataSource.Orders[DataSource.NextOrder - 1] = temp;
                    DataSource.NextOrder--;
                }
                break;
            }
        }
    }
    ///Function to update an order
    public void Update(Order newOrder)
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
            throw new EntityNotFound("id");
    }
    /// A function that returns an order by id
    public Order Get(int idToGet)
    {
        for (int i = 0; i < DataSource.NextOrder; i++)
        {
            if (idToGet == DataSource.Orders[i].Id)
            {
                return DataSource.Orders[i];
            }
        }
        throw new EntityNotFound("order");
    }
    /// A function that returns an array of the orders in the database
    public Order[] List()
    {
        Order[] orderList = new Order[DataSource.NextOrder];
        for (int i = 0; i < DataSource.NextOrder; i++)
        {
            orderList[i] = DataSource.Orders[i];
        }
        return orderList;
    }
}
