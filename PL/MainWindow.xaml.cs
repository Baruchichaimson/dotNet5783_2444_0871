using PL.cart_main_windows;
using PL.client_window;
using System.Windows;
using PL.admin_window;

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
       // string s = Directory.GetCurrentDirectory();
       // background = new BitmapImage(new Uri(@$"{s}\images\Logo.png"));
        InitializeComponent();
    }
    /// <summary>
    /// event to double click to go update the product.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ShowProductButton_Click(object sender, RoutedEventArgs e) => new ProductAndOrderList().Show();

    private void ShowNewOrderButton_Click(object sender, RoutedEventArgs e) => new clientwindow().Show();

}
