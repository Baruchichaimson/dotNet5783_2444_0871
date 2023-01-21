using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Order_Tracking_window;

/// <summary>
/// Interaction logic for SimulatorWindow.xaml
/// </summary>
public partial class SimulatorWindow : Window
{
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
    public ProccessDetails MyProccessDetails
    {
        get { return (ProccessDetails)GetValue(MyProccessDetailsProperty); }
        set { SetValue(MyProccessDetailsProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProccessDetails.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MyProccessDetailsProperty =
        DependencyProperty.Register("MyProccessDetails", typeof(ProccessDetails), typeof(SimulatorWindow));

    public string MyClock
    {
        get { return (string)GetValue(MyClockProperty); }
        set { SetValue(MyClockProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyClock.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MyClockProperty =
        DependencyProperty.Register("MyClock", typeof(string), typeof(SimulatorWindow));

    public double MyProgressBarValue
    {
        get { return (double)GetValue(MyProgressBarValueProperty); }
        set { SetValue(MyProgressBarValueProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MyProgressBarValueProperty =
        DependencyProperty.Register("MyProgressBarValue", typeof(double), typeof(SimulatorWindow));

    private int duration;
    private int precent;

    private Stopwatch stopwatch;

    private BackgroundWorker worker;

    private enum Progress { Clock, Update, Complete }

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

    private void worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
    {
        switch ((Progress)e.ProgressPercentage)
        {
            case Progress.Clock:
                MyClock = stopwatch.Elapsed.ToString().Substring(0, 8);
                MyProgressBarValue = (precent++ * 100 / duration);
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

    private void worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e , string message)
    {
        Simulator.Simulator.DeRegisterToUpdtes(updateProgres);
        Simulator.Simulator.DeRegisterToStop(stopWorker);
        Simulator.Simulator.DeRegisterToComplete(UpdateComplete);

        MessageBox.Show(message);
        Close();
    }

    private void stopWorker()
    {
        worker.CancelAsync();
        MessageBox.Show(message);

    }

    private void updateProgres(BO.Order order, OrderStatus? status, DateTime tretTime, int treatDuration)
    {
        precent = 0;
        duration = treatDuration;
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

    private void UpdateComplete(OrderStatus? status)
    => worker.ReportProgress((int)Progress.Complete, status);

    private void Stop_Click(object sender, RoutedEventArgs e)
    => Simulator.Simulator.StopSimulation("Simulation stop");

}
public class ProccessDetails : DependencyObject
{
    public int id { get; set; }
    public OrderStatus? CurrentStatus { get; set; }
    public OrderStatus? NextStatus { get; set; }
    public DateTime CurrentTreatTime { get; set; }
    public DateTime EstimatedTreatTime { get; set; }

}

