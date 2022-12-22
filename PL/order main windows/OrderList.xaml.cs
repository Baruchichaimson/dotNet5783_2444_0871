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
    /// Interaction logic for OrderList.xaml
    /// </summary>
    public partial class OrderList : Window
    {
        private BlApi.IBl? _bl = BlApi.Factory.Get();
        public OrderList()
        {
            InitializeComponent();
            OrderlistView.ItemsSource = _bl?.Order.GetList();
        }

        private void OrderlistView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(IsMouseCaptureWithin)    
                 new UpdateOrder(_bl, ((BO.OrderForList)OrderlistView.SelectedItem).ID).Show();
        }
    }
}
