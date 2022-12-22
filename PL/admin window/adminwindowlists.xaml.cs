using PL.order_main_windows;
using PL.product_main_windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.admin_window;

/// <summary>
/// Interaction logic for adminwindowlists.xaml
/// </summary>
public partial class adminwindowlists : Window
{
    public adminwindowlists()
    {
        InitializeComponent();
    }

    private void ShowProductButton1_Click(object sender, RoutedEventArgs e) => new ProductList().Show();

    private void ShowProductButton2_Click(object sender, RoutedEventArgs e) => new OrderList().Show();
}
