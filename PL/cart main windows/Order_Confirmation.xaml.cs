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
        /// <summary>
        /// Constructor for the Order_Confirmation class.
        /// </summary>
        /// <param name="bl">An optional parameter of type BlApi.IBl that represents the business logic object.</param>
        /// <param name="newCart">An object of type BO.Cart that represents the new cart.</param>
        public Order_Confirmation(BlApi.IBl? bl, BO.Cart newCart)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            _bl = bl;
            cartDetails = newCart;
        }
        /// <summary>
        /// function to border me to input numbers in the text box
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void name_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, @"^[\p{N}]+$");
        }
        /// <summary>
        /// function to border me to input valid email.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void email_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text , @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        }
        /// <summary>
        /// function to border me to input numbers in the text box
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void adrress_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            e.Handled = Regex.IsMatch(e.Text, @"^[A-Za-z0-9]+(?:\s[A-Za-z0-9'_-]+)+$");
        }
        /// <summary>
        /// the button that confirmd finaly the order after he input is detais in system.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
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
