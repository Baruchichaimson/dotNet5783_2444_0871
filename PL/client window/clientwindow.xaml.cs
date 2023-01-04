using BO;
using PL.cart_main_windows;
using PL.new_order_window;
using PL.order_main_windows;
using System.Windows;
namespace PL.client_window;

/// <summary>
/// Interaction logic for clientwindow.xaml
/// </summary>
public partial class clientwindow : Window
{
    public clientwindow()
    {
        InitializeComponent();
        WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
    }
    private void ShowProductButton2_Click(object sender, RoutedEventArgs e) => new NewOrder().ShowDialog();

    private void ShowProductButton1_Click(object sender, RoutedEventArgs e) => new OrderTrackingWindow().ShowDialog();
}
