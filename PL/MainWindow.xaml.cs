using PL.cart_main_windows;
using PL.client_window;
using System.Windows;
using PL.admin_window;
using BO;
using PL.Order_Tracking_window;

namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// accses for the logical layyer.
    /// </summary>
    private BlApi.IBl? _bl = BlApi.Factory.Get();

    /// <summary>
    /// constructor
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
        WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
    }

    /// <summary>
    /// event to double click to go update the product.
    /// </summary>
    /// <param name="sender">The object that raised the event.</param>
    /// <param name="e">Event arguments.</param>
    private void ShowProductButton_Click(object sender, RoutedEventArgs e) => new ProductAndOrderList().ShowDialog();
    private void ShowNewOrderButton_Click(object sender, RoutedEventArgs e) => new clientwindow().ShowDialog();
    private void OrderTrackingButton_Click(object sender, RoutedEventArgs e)
    {
        if (_bl?.Order.getOldOrder() is null)
        {
            MessageBox.Show("There are no additional orders to update", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        else
        {
            new SimulatorWindow().Show();
        }
    }
}
