using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Order_Confirmation.xaml
    /// </summary>
    public partial class Order_Confirmation : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private string customerName_p;
        private string customerEmail_p;
        private string customerAddress_p;
        private BlApi.IBl? _bl;
        private Cart cart;
        Regex regex;
        public string customerName { get { return customerName_p; } set { customerName_p = value; if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("customerName")); } } }
        public string customerEmail { get { return customerEmail_p; } set { customerEmail_p = value; if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("customerEmail")); } } }
        public string customerAddress { get { return customerAddress_p; } set { customerAddress_p = value; if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("customerAddress")); } } }
        public Order_Confirmation(BlApi.IBl? bl, BO.Cart newCart)
        {
            InitializeComponent();
            _bl = bl;
            cart = newCart;
        }

        private void name_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, @"^[\p{N}]+$");
        }

        private void email_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void adrress_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            regex = new Regex(@"^[A-Za-z0-9]+(?:\s[A-Za-z0-9'_-]+)+$");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void order_now(object sender, RoutedEventArgs e)
        {
            cart.CustomerName = customerName;
            cart.CustomerEmail = customerEmail; 
            cart.CustomerAddress = customerAddress;
            try { _bl.Cart.OrderConfirmation(cart); } catch (BO.EntityDetailsWrongException ex) { MessageBox.Show(ex.Message); }
          
            Close();
        }
    }
}
