using BlApi;
using BO;
using System;
using System.Reflection.Metadata.Ecma335;

namespace Simulator;

public static class Simulator
{
    static IBl? _bl = Factory.Get();
    private static Random random = new Random(DateTime.Now.Millisecond);
    private volatile static bool run;
    private static BO.Order? order;

    private static Action? actionForAdmin;
    public static event Action? ActionForAdmin
    {
        add => actionForAdmin += value;
        remove => actionForAdmin += value;
    }

    private static Action stopSimulator;
    public static event Action StopSimulator
    {
        add => stopSimulator += value;
        remove => stopSimulator += value;
    }

    private static Action<Order, OrderStatus?, DateTime, int>? updatePlWindow;
    public static event Action<Order, OrderStatus?, DateTime, int>? UpdatePlWindow
    {  
        add => updatePlWindow += value;
        remove => updatePlWindow += value;
    }
    private static Action<OrderStatus?>? updateComplete;
    public static event Action<OrderStatus?>? UpdateComplete
    {
        add => updateComplete += value;
        remove => updateComplete += value;
    }

    public static bool _isRunning = false;

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
            _isRunning = true;
            while (run)
            {
                int? id = _bl?.Order.getOldOrder();
                if (id is not null)
                {
                    processTime = random.Next(3, 8);

                    order = _bl?.Order.GetData((int)id!);
                    updatePlWindow?.Invoke(order, order.Status + 1, DateTime.Now, processTime);
                    Thread.Sleep(processTime * 1000);
                    
                    if ( _bl?.Order.GetData((int)id!).ShipDate is null && _bl?.Order.GetData((int)id!).DeliveryrDate is null)
                    {
                        _bl?.Order.UpdateShippingDate(order.ID);
                        updateComplete?.Invoke(OrderStatus.SHIPPED);
                    }
                    else if(_bl?.Order.GetData((int)id!).ShipDate is not null && _bl?.Order.GetData((int)id!).DeliveryrDate is null)
                    {
                        _bl?.Order.DeliveryUpdate(order!.ID);
                        updateComplete?.Invoke(OrderStatus.PROVIDED);
                    }
                    actionForAdmin?.Invoke();
                }
                Thread.Sleep(1000);
            }
            _isRunning = false;
        }).Start();

    }
    /// <summary>
    /// Stops the simulation and invokes the stopSimulator event with the provided message.
    /// </summary>
    /// <param name="messeage">The message to be passed to the stopSimulator event upon invocation.</param>
    public static void StopSimulation()
    {
        run = false;
        new Thread(() =>
        {
            while (_isRunning) { };
            stopSimulator?.Invoke();

        }).Start();
       
    }
}