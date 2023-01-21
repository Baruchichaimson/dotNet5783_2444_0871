using BlApi;
using BO;

namespace Simulator;

public static class Simulator
{
    static readonly IBl? bl = Factory.Get();

    public static readonly Random random = new Random(DateTime.Now.Millisecond);

    private volatile static bool run;

    private static Order? order;

    private static event Action<string>? stopSimulator;

    private static event Action<Order, OrderStatus?, DateTime, int>? updateSimulator;

    private static event Action<OrderStatus?>? UpdateComplete;

    public static void StartSimulation()
    {
        new Thread(() =>
        {
            run = true;

            while (run)
            {

                    int? id = bl!.Order.getOldOrder();
                if (id is not null)
                {
                    order = bl!.Order.GetData((int)id!);


                    int treatTime = random.Next(3, 11);

                    updateSimulator?.Invoke(order, order.Status + 1, DateTime.Now, treatTime);

                    Thread.Sleep(treatTime * 1000);

                    if (order.Status == OrderStatus.CONFIRMED)
                    {
                        bl!.Order.UpdateShippingDate(order.ID);
                        UpdateComplete?.Invoke(OrderStatus.SHIPPED);
                    }
                    else
                    {
                        bl!.Order.DeliveryUpdate(order.ID);
                        UpdateComplete?.Invoke(OrderStatus.PROVIDED);
                    }
                }
                else
                    StopSimulation("ther is no more old order");
                Thread.Sleep(1000);
            }
        }).Start();
    }

    public static void StopSimulation(string messeage)
    {
        stopSimulator?.Invoke(messeage);
        run = false;
    }

    public static void RegisterToStop(Action<string> action) => stopSimulator += action;
    public static void DeRegisterToStop(Action<string> action) => stopSimulator -= action;

    public static void RegisterToComplete(Action<OrderStatus?> action) => UpdateComplete += action;
    public static void DeRegisterToComplete(Action<OrderStatus?> action) => UpdateComplete -= action;

    public static void RegisterToUpdtes(Action<Order, OrderStatus?, DateTime, int> action) => updateSimulator += action;
    public static void DeRegisterToUpdtes(Action<Order, OrderStatus?, DateTime, int> action) => updateSimulator -= action;


}