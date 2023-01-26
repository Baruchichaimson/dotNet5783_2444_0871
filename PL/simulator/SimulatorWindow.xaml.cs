using BO;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel.Channels;
using System.Threading;
using System.Windows;


namespace PL.Order_Tracking_window;

/// <summary>
/// Interaction logic for SimulatorWindow.xaml
/// </summary>
public partial class SimulatorWindow : Window
{
    private enum Progress { Clock, Update, Complete }
    private int workTime;
    private int precent;
    private Stopwatch stopwatch;
    private BackgroundWorker worker;

    public Visibility stopButton
    {
        get { return (Visibility)GetValue(StopButtonProperty); }
        set { SetValue(StopButtonProperty, value); }
    }
   
    /// <summary>
    /// Defines a public string property 'MyClock'
    /// with a getter and setter for accessing and updating 
    /// the value of the MyClock dependency property.
    /// </summary>
    public string MyClock
    {
        get { return (string)GetValue(MyClockProperty); }
        set { SetValue(MyClockProperty, value); }
    }
    /// <summary>
    /// Defines a public double property 'MyProgressBarValue'
    /// with a getter and setter for accessing and updating the 
    /// value of the MyProgressBarValue dependency property.
    /// </summary>
    public double MyProgressBarValue
    {
        get { return (double)GetValue(MyProgressBarValueProperty); }
        set { SetValue(MyProgressBarValueProperty, value); }
    }
    /// <summary>
    /// Defines a public OrderDetails property 'MyProccessDetails' with a getter 
    /// and setter for accessing and updating the value of the MyProccessDetails dependency property.
    /// </summary>
    public OrderDetails MyProccessDetails
    {
        get { return (OrderDetails)GetValue(MyProccessDetailsProperty); }
        set { SetValue(MyProccessDetailsProperty, value); }
    }

    /// <summary>
    /// Using a DependencyProperty as the backing store for MyProccessDetails.  This enables animation, styling, binding, etc...
    /// </summary>
    public static readonly DependencyProperty StopButtonProperty =
       DependencyProperty.Register("stopButton", typeof(Visibility), typeof(SimulatorWindow));
    public static readonly DependencyProperty MyProccessDetailsProperty =
        DependencyProperty.Register("MyProccessDetails", typeof(OrderDetails), typeof(SimulatorWindow));
    public static readonly DependencyProperty MyClockProperty =
        DependencyProperty.Register("MyClock", typeof(string), typeof(SimulatorWindow));
    public static readonly DependencyProperty MyProgressBarValueProperty =
        DependencyProperty.Register("MyProgressBarValue", typeof(double), typeof(SimulatorWindow));
    /// <summary>
    /// Initializes the SimulatorWindow by calling InitializeComponent, 
    /// creating a new Stopwatch object and starting it, setting the initial 
    /// values for MyClock, MyProgressBarValue, and creating a new 
    /// BackgroundWorker object with specified settings. It also assigns event
    /// handlers for the worker's DoWork, ProgressChanged, 
    /// and RunWorkerCompleted events and starts the worker.
    /// </summary>
    public SimulatorWindow()
    {
        InitializeComponent();
        stopwatch = new Stopwatch();
        stopwatch.Start();
        MyClock = stopwatch.Elapsed.ToString().Substring(0, 8);
        WindowStyle = WindowStyle.None;
        MyProgressBarValue = 0;
        stopButton = Visibility.Visible;
        worker = new BackgroundWorker()
        {
            WorkerReportsProgress = true,
            WorkerSupportsCancellation = true
        };

        worker.DoWork += worker_DoWork;
        worker.ProgressChanged += worker_ProgressChanged;
        worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        worker.RunWorkerAsync();
    }
    /// <summary>
    /// Registers the updateProgres, stopWorker and UpdateComplete methods as event handlers
    /// for updates, stop and completion events of the Simulator class. Then starts the
    /// simulation and continuously sleeps for 1 second and reports progress for the clock 
    /// while the worker is not canceled.
    /// </summary>
    /// <param name="sender">The object that raised the event.</param>
    /// <param name="e">The event data.</param>
    private void worker_DoWork(object? sender, DoWorkEventArgs e)
    {
        Simulator.Simulator.UpdatePlWindow += UpdateProgress;
        Simulator.Simulator.StopSimulator += stopWorker;
        Simulator.Simulator.UpdateComplete += UpdateComplete;
        Simulator.Simulator.StartSimulation();
        while (!worker.CancellationPending)
        {
            Thread.Sleep(10);
            worker.ReportProgress((int)Progress.Clock);
        }
    }
    /// <summary>
    /// Handles the progress changed event of the worker. 
    /// It changes the value of MyClock, MyProgressBarValue
    /// and MyProccessDetails depending on the progress percentage passed as an argument.
    /// </summary>
    /// <param name="sender">The object that raised the event.</param>
    /// <param name="e"> The event data, which includes the progress percentage and an optional user state object.</param>
    private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        var progress = (Progress)e.ProgressPercentage;
        if (progress == Progress.Clock)
        {
            MyClock = stopwatch.Elapsed.ToString().Substring(0, 8);
            MyProgressBarValue = precent++  / workTime;
        }
        else if (progress == Progress.Update)
        {
            MyProccessDetails = (e.UserState as OrderDetails)!;
        }
    }
    /// <summary>
    /// Handles the completion event of the worker. It deregisters the updateProgres,
    /// stopWorker and UpdateComplete methods as event handlers 
    /// for updates, stop and completion events of the Simulato
    /// r class. Then it closes the window.
    /// </summary>
    /// <param name="sender"> The object that raised the event.</param>
    /// <param name="e">The event data, which includes the results of the background operation.</param>
    private void worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        Simulator.Simulator.UpdatePlWindow -= UpdateProgress;
        Simulator.Simulator.StopSimulator -= stopWorker;
        Simulator.Simulator.UpdateComplete -= UpdateComplete;
        Close();
    }
    /// <summary>
    /// Stops the background worker and shows a message box with the provided message.
    /// </summary>
    /// <param name="message">The message to be displayed in the message box.</param>
    private void stopWorker()
    {
        worker.CancelAsync();
        MessageBox.Show("The simulator has stopped!");
    }
    /// <summary>
    /// Updates the progress by creating a new OrderDetails object 
    /// with the provided order, status, treat time, and treat duration, 
    /// then sets the work time and percent and reports the progress with the new OrderDetails object.
    /// </summary>
    /// <param name="order">The order that is currently being processed.</param>
    /// <param name="status">The next status of the order.</param>
    /// <param name="tretTime">The time of the treatment.</param>
    /// <param name="timeOfProcces">The duration of the treatment.</param>
    private void UpdateProgress(BO.Order order, OrderStatus? status, DateTime treatTime, int timeOfProcces)
    {
        precent = 0;
        workTime = timeOfProcces;
        var process = new OrderDetails
        {
            id = order.ID,
            CurrentStatus = order.Status,
            NextStatus = status,
            CurrentTreatTime = treatTime,
            EstimatedTreatTime = treatTime + TimeSpan.FromSeconds(timeOfProcces)
        };

        worker.ReportProgress((int)Progress.Update, process);
    }
    /// <summary>
    /// Reports the progress with the status of the order which is complete.
    /// </summary>
    /// <param name="status">The status of the order that is complete.</param>
    private void UpdateComplete(OrderStatus? status)
    => worker.ReportProgress((int)Progress.Complete, status);
    /// <summary>
    /// Stops the simulation by calling the StopSimulation 
    /// method of the Simulator class and providing the message 'Simulation stop
    /// </summary>
    /// <param name="sender"> The object that raised the event.</param>
    /// <param name="e">The event data.</param>
    private void Stop_Click(object sender, RoutedEventArgs e)
    {
        stopButton = Visibility.Hidden;
        Simulator.Simulator.StopSimulation();
    }
    
}
/// <summary>
/// "Defines a class OrderDetails that contains properties for 
/// storing information about the process status of an order,
/// including its ID, current status, next status, current
/// treatment time, and estimated treatment time.
/// </summary>
public class OrderDetails : DependencyObject
{
    public int id { get; set; }
    public OrderStatus? CurrentStatus { get; set; }
    public OrderStatus? NextStatus { get; set; }
    public DateTime CurrentTreatTime { get; set; }
    public DateTime EstimatedTreatTime { get; set; }
}

