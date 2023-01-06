using BO;
using PL.admin_window;
using PL.new_order_window;
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

namespace PL.order_main_windows
{
    /// <summary>
    /// Interaction logic for OrderTrackingWindow.xaml
    /// </summary>
    public partial class OrderTrackingWindow : Window, INotifyPropertyChanged
    {
        private BlApi.IBl? _bl = BlApi.Factory.Get();
        public event PropertyChangedEventHandler? PropertyChanged;

        private BO.OrderTracking order;
        /// <summary>
        /// we creat a new property change name.
        /// </summary>
        private string orderTrackingString_p;
        public string orderTrackingString { get { return orderTrackingString_p; } set { orderTrackingString_p = value; if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("orderTrackingString")); } } }
        /// <summary>
        ///  we creat a new property change name.
        /// </summary>
        private int orderId_p;
        public int orderId { get { return orderId_p; } set { orderId_p = value; if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("orderId")); } } }
        /// <summary>
        /// constractor
        /// </summary>
        public OrderTrackingWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// function that if we put id of order in the text box we got the status on him.
        /// aotomticly with the property change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                order = _bl?.Order.OrderTracking(orderId)!;
                orderTrackingString = order.ToString()!;
            }
            catch (BO.NullExeptionForDO ex) when (ex.InnerException is not null)
            {
                MessageBox.Show(ex.Message + ex.InnerException!.Message);
            }
        }
        /// <summary>
        /// function to remove the option to input latters in the text box of id.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "^[^0-9]+$");
        }
        /// <summary>
        /// function to get the order details .
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetItems_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new OrderDatails(_bl, orderId).Show();

            }
            catch (BO.IdNotExsitException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.NullExeptionForDO ex) when (ex.InnerException is not null)
            {
                MessageBox.Show(ex.Message + ex.InnerException!.Message);
            }
        }
    }
}
