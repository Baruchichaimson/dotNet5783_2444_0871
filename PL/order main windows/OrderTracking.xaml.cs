using BO;
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
    public partial class OrderTrackingWindow : Window , INotifyPropertyChanged
    {
        private BlApi.IBl? _bl = BlApi.Factory.Get();
        public event PropertyChangedEventHandler? PropertyChanged;

        private BO.OrderTracking order;
        private string orderTrackingString_p;
        public string orderTrackingString { get { return orderTrackingString_p; } set { orderTrackingString_p = value; if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("orderTrackingString")); } } }

        private int orderId_p;
        public int orderId { get { return orderId_p; } set { orderId_p = value; if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("orderId")); } } }

        public OrderTrackingWindow()
        {
            InitializeComponent();
        }

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
        private void ID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "^[^0-9]+$");
        }
    }
}
