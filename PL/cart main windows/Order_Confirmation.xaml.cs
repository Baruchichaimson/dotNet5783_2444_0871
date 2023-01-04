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
        private BlApi.IBl? _bl;
        private Cart cart_p;
        public Cart cartDetails { get => cart_p;  set { cart_p = value; if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("cartDetails")); } } }
        public Order_Confirmation(BlApi.IBl? bl, BO.Cart newCart)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            _bl = bl;
            cartDetails = newCart;
        }

        private void name_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, @"^[\p{N}]+$");
        }

        private void email_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text , @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        }

        private void adrress_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            e.Handled = Regex.IsMatch(e.Text, @"^[A-Za-z0-9]+(?:\s[A-Za-z0-9'_-]+)+$");
        }

        private void order_now(object sender, RoutedEventArgs e)
        {
            try { _bl?.Cart.OrderConfirmation(cartDetails); }
            catch (BO.EntityDetailsWrongException ex)
            { 
                MessageBox.Show(ex.Message);
                return; 
            }
            catch (BO.CartException ex)
            {
                MessageBox.Show(ex.Message); 
                return;
            }catch (BO.IncorrectAmountException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            Close();
            MessageBox.Show("ORDER CONFIRM", "ORDER CONFIRM", MessageBoxButton.OK, MessageBoxImage.Information);
            
        }
    }
}
