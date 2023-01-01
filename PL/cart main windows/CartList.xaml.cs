using BlApi;
using BO;
using PL.order_main_windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class CartList : Window , INotifyPropertyChanged
    {
        private BlApi.IBl? _bl;
        private Cart cart;

        public event PropertyChangedEventHandler? PropertyChanged;
        private List<OrderItem?>? cartItems_p;
        public List<OrderItem?>? CartItems { get => cartItems_p; set { cartItems_p = value; if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("CartItems")); } } }

        public CartList(BlApi.IBl? bl , BO.Cart newCart)
        {
            InitializeComponent();
            _bl = bl;
            cart = newCart;
            CartItems = newCart.Items!;
        }

        private void order_now(object sender, RoutedEventArgs e)
        {
            new Order_Confirmation(_bl, cart).Show();
            Close();
        }
    }
}
