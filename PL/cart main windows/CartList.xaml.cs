using BlApi;
using BO;
using PL.order_main_windows;
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

namespace PL.cart_main_windows
{
    /// <summary>
    /// Interaction logic for CartList.xaml
    /// </summary>
    public partial class CartList : Window
    {
        private BlApi.IBl? _bl;
        private Cart cart;
        public static readonly DependencyProperty cartItemsProp = DependencyProperty.Register(nameof(cartItems), typeof(List<OrderItem?>), typeof(CartList), new PropertyMetadata(null));
        public List<OrderItem?> cartItems  { get => (List<OrderItem?>)GetValue(cartItemsProp); set => SetValue(cartItemsProp, value); }
        public CartList(BlApi.IBl? bl , BO.Cart newCart)
        {
            InitializeComponent();
            _bl = bl;
            cart = newCart;
            cartItems = newCart.Items!;
        }

        private void CartlistView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void CartlistView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void order_now(object sender, RoutedEventArgs e)
        {
            new Order_Confirmation(_bl, cart).Show();
            Close();
        }
    }
}
