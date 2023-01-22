using BlApi;
using BO;
using DalApi;

namespace Simulator;

public static class Simulator
{
    static IBl? bl = Factory.Get();
    private static Random random = new Random(DateTime.Now.Millisecond);
    private volatile static bool run;
    private static Order? order;
    public static Action actionForAdmin;
    private static event Action<string>? stopSimulator;
    private static event Action<Order, OrderStatus?, DateTime, int>? updatePlWindow;
    private static event Action<OrderStatus?>? UpdateComplete;
    /// <summary>
    /// 
    /// </summary>
    public static void StartSimulation()
    {
        int processTime;
        new Thread(() =>
        {
            run = true;

            while (run)
            {
                int? id = bl!.Order.getOldOrder();
                if (id is not null)
                {
                    processTime = random.Next(3, 8);

                    order = bl!.Order.GetData((int)id!);
                    updatePlWindow?.Invoke(order, order.Status + 1, DateTime.Now, processTime);
                    Thread.Sleep(processTime * 1000);
                    
                    if (order.Status == OrderStatus.CONFIRMED && order.ShipDate is null)
                    {
                        bl!.Order.UpdateShippingDate(order.ID);
                        UpdateComplete?.Invoke(OrderStatus.SHIPPED);
                    }
                    else if(order.DeliveryrDate is null)
                    {
                        bl!.Order.DeliveryUpdate(order.ID);
                        UpdateComplete?.Invoke(OrderStatus.PROVIDED);
                    }
                    actionForAdmin?.Invoke();
                }
                else
                    StopSimulation("ther is no more old order");
                Thread.Sleep(1000);
            }
        }).Start();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="messeage"></param>
    public static void StopSimulation(string messeage)
    {
        run = false;
        stopSimulator?.Invoke(messeage);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="action"></param>
    public static void RegisterToAdmin(Action action) => actionForAdmin += action;
    public static void DeRegisterToAdmin(Action action) => actionForAdmin -= action;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="action"></param>
    public static void RegisterToStop(Action<string> action) => stopSimulator += action;
    public static void DeRegisterToStop(Action<string> action) => stopSimulator -= action;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="action"></param>
    public static void RegisterToComplete(Action<OrderStatus?> action) => UpdateComplete += action;
    public static void DeRegisterToComplete(Action<OrderStatus?> action) => UpdateComplete -= action;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="action"></param>
    public static void RegisterToUpdtes(Action<Order, OrderStatus?, DateTime, int> action) => updatePlWindow += action;
    public static void DeRegisterToUpdtes(Action<Order, OrderStatus?, DateTime, int> action) => updatePlWindow -= action;


}