using BO;
using System;
using System.ComponentModel;
using System.Diagnostics;
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
    /// <summary>
    /// 
    /// </summary>
    public string MyClock
    {
        get { return (string)GetValue(MyClockProperty); }
        set { SetValue(MyClockProperty, value); }
    }
    /// <summary>
    /// 
    /// </summary>
    public double MyProgressBarValue
    {
        get { return (double)GetValue(MyProgressBarValueProperty); }
        set { SetValue(MyProgressBarValueProperty, value); }
    }
    /// <summary>
    /// 
    /// </summary>
    public ProccessDetails MyProccessDetails
    {
        get { return (ProccessDetails)GetValue(MyProccessDetailsProperty); }
        set { SetValue(MyProccessDetailsProperty, value); }
    }

    /// <summary>
    /// Using a DependencyProperty as the backing store for MyProccessDetails.  This enables animation, styling, binding, etc...
    /// </summary>
    public static readonly DependencyProperty MyProccessDetailsProperty =
        DependencyProperty.Register("MyProccessDetails", typeof(ProccessDetails), typeof(SimulatorWindow));
    public static readonly DependencyProperty MyClockProperty =
        DependencyProperty.Register("MyClock", typeof(string), typeof(SimulatorWindow));
    public static readonly DependencyProperty MyProgressBarValueProperty =
        DependencyProperty.Register("MyProgressBarValue", typeof(double), typeof(SimulatorWindow));
    /// <summary>
    /// 
    /// </summary>
    public SimulatorWindow()
    {
        InitializeComponent();
        stopwatch = new Stopwatch();
        stopwatch.Start();
        MyClock = stopwatch.Elapsed.ToString().Substring(0, 8);

        MyProgressBarValue = 0;

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
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void worker_DoWork(object? sender, DoWorkEventArgs e)
    {
        Simulator.Simulator.RegisterToUpdtes(updateProgres);
        Simulator.Simulator.RegisterToStop(stopWorker);
        Simulator.Simulator.RegisterToComplete(UpdateComplete);
        Simulator.Simulator.StartSimulation();
        while (!worker.CancellationPending)
        {
            Thread.Sleep(1000);
            worker.ReportProgress((int)Progress.Clock);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
    {
        switch ((Progress)e.ProgressPercentage)
        {
            case Progress.Clock:
                MyClock = stopwatch.Elapsed.ToString().Substring(0, 8);
                MyProgressBarValue = (precent++ * 100 / workTime);
                break;
            case Progress.Update:
                MyProccessDetails = (e.UserState as ProccessDetails)!;
                break;
            case Progress.Complete:
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        Simulator.Simulator.DeRegisterToUpdtes(updateProgres);
        Simulator.Simulator.DeRegisterToStop(stopWorker);
        Simulator.Simulator.DeRegisterToComplete(UpdateComplete);
        Close();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    private void stopWorker(string message)
    {
        worker.CancelAsync();
        MessageBox.Show(message);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="order"></param>
    /// <param name="status"></param>
    /// <param name="tretTime"></param>
    /// <param name="treatDuration"></param>
    private void updateProgres(BO.Order order, OrderStatus? status, DateTime tretTime, int treatDuration)
    {
        precent = 0;
        workTime = treatDuration;
        ProccessDetails proc = new()
        {
            id = order.ID,
            CurrentStatus = order.Status,
            NextStatus = status,
            CurrentTreatTime = tretTime,
            EstimatedTreatTime = tretTime + TimeSpan.FromSeconds(treatDuration)
        };

        worker.ReportProgress((int)Progress.Update, proc);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="status"></param>
    private void UpdateComplete(OrderStatus? status)
    => worker.ReportProgress((int)Progress.Complete, status);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Stop_Click(object sender, RoutedEventArgs e)
    => Simulator.Simulator.StopSimulation("Simulation stop");
}
/// <summary>
/// 
/// </summary>
public class ProccessDetails : DependencyObject
{
    public int id { get; set; }
    public OrderStatus? CurrentStatus { get; set; }
    public OrderStatus? NextStatus { get; set; }
    public DateTime CurrentTreatTime { get; set; }
    public DateTime EstimatedTreatTime { get; set; }
}

