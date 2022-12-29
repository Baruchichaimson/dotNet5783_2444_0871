using PL.new_order_window;
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

namespace PL.order_main_windows
{
    /// <summary>
    /// Interaction logic for OrderTracking.xaml
    /// </summary>
    public partial class OrderTracking : Window
    {
        private BlApi.IBl? _bl = BlApi.Factory.Get();
        public static readonly DependencyProperty orderDetailsProp = DependencyProperty.Register(nameof(orderDetailslist), typeof(List<string?>), typeof(OrderTracking), new PropertyMetadata(null));
        public List<string?> orderDetailslist { get => (List<string?>)GetValue(orderDetailsProp); set => SetValue(orderDetailsProp, value); }
        public OrderTracking()
        {
            InitializeComponent();
           // orderDetailslist = _bl?.Order.OrderTracking(orderId);
        }

        private void addToCart(object sender, RoutedEventArgs e)
        {

        }
    }
}
