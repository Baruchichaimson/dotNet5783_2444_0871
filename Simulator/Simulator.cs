using BlApi;
using BO;

namespace Simulator;

public static class Simulator
{
    static IBl? _bl = Factory.Get();
    private static Random random = new Random(DateTime.Now.Millisecond);
    private volatile static bool run;
    private static Order? order;
    public static Action? actionForAdmin;
    private static event Action<string>? stopSimulator;
    private static event Action<Order, OrderStatus?, DateTime, int>? updatePlWindow;
    private static event Action<OrderStatus?>? UpdateComplete;
    /// <summary>
    /// Starts the simulation of processing and updating orders. 
    /// Continuously checks for old orders, updates their status,
    /// and invokes registered actions for admin and update events.
    /// </summary>
    public static void StartSimulation()
    {
        int processTime;
        new Thread(() =>
        {
            run = true;

            while (run)
            {
                int? id = _bl?.Order.getOldOrder();
                if (id is not null)
                {
                    processTime = random.Next(3, 8);

                    order = _bl?.Order.GetData((int)id!);
                    updatePlWindow?.Invoke(order, order.Status + 1, DateTime.Now, processTime);
                    Thread.Sleep(processTime * 1000);

                    if (order.Status == OrderStatus.CONFIRMED)
                    {
                        _bl?.Order.UpdateShippingDate(order.ID);
                        UpdateComplete?.Invoke(OrderStatus.SHIPPED);
                    }
                    else if(order.ShipDate is not null)
                    {
                        _bl?.Order.DeliveryUpdate(order.ID);
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
    /// Stops the simulation and invokes the stopSimulator event with the provided message.
    /// </summary>
    /// <param name="messeage">The message to be passed to the stopSimulator event upon invocation.</param>
    public static void StopSimulation(string messeage)
    {
        run = false;
        stopSimulator?.Invoke(messeage);
    }
    /// <summary>
    /// Registers or deregisters an action for the admin. The registered action will be invoked when called.
    /// </summary>
    /// <param name="action">The action to be registered or deregistered for the admin.</param>
    public static void RegisterToAdmin(Action? action) => actionForAdmin += action;
    public static void DeRegisterToAdmin(Action? action) => actionForAdmin -= action;
    /// <summary>
    /// Registers or deregisters an action for the admin. The registered action will be invoked when called.
    /// </summary>
    /// <param name="action">The action to be registered or deregistered for the admin.</param>
    public static void RegisterToStop(Action<string> action) => stopSimulator += action;
    public static void DeRegisterToStop(Action<string> action) => stopSimulator -= action;
    /// <summary>
    /// Registers or deregisters an action for the admin. The registered action will be invoked when called.
    /// </summary>
    /// <param name="action">The action to be registered or deregistered for the admin.</param>
    public static void RegisterToComplete(Action<OrderStatus?> action) => UpdateComplete += action;
    public static void DeRegisterToComplete(Action<OrderStatus?> action) => UpdateComplete -= action;
    /// <summary>
    /// Registers or deregisters an action for the admin. The registered action will be invoked when called.
    /// </summary>
    /// <param name="action">The action to be registered or deregistered for the admin.</param>
    public static void RegisterToUpdtes(Action<Order, OrderStatus?, DateTime, int> action) => updatePlWindow += action;
    public static void DeRegisterToUpdtes(Action<Order, OrderStatus?, DateTime, int> action) => updatePlWindow -= action;


}