using System;
using System.ComponentModel;
using System.Windows;
namespace PL.admin_window;


/// <summary>
/// Interaction logic for UpdateOrder.xaml
/// </summary>
public partial class UpdateOrder : Window , INotifyPropertyChanged
{
    private BlApi.IBl? _bl;
    public event PropertyChangedEventHandler? PropertyChanged;
    private BO.Order OrderDetail_p;
    public BO.Order OrderDetail { get { return OrderDetail_p; } set { OrderDetail_p = value; if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("OrderDetail")); } } }
    Action changeList;
    public UpdateOrder(BlApi.IBl? bl, int orderId ,Action action)
    {
        InitializeComponent();
        _bl = bl;
        changeList = action;
        OrderDetail = bl?.Order.GetData(orderId)!;

        UpdateDelivery.Content = "Update Delivery";
        ItemListView.ItemsSource = OrderDetail.Items;

        if (OrderDetail.ShipDate is not null)
            UpdateShip.Visibility = Visibility.Hidden;
        else
            UpdateDelivery.Visibility = Visibility.Hidden;

        if (OrderDetail.DeliveryrDate is not null)
            UpdateDelivery.Visibility = Visibility.Hidden;
    }

    private void UpdateDeliveryDate(object sender, RoutedEventArgs e)
    {
        OrderDetail = _bl?.Order.DeliveryUpdate(OrderDetail.ID)!;
        changeList();
        MessageBox.Show("SUCCSES", "SUCCSES", MessageBoxButton.OK, MessageBoxImage.Information);
        UpdateDelivery.Visibility = Visibility.Hidden;
        Close();
    }


    private void UpdateShipDate(object sender, RoutedEventArgs e)
    {
        OrderDetail = _bl?.Order.UpdateShippingDate(OrderDetail.ID)!;
        changeList();
        MessageBox.Show("SUCCSES", "SUCCSES", MessageBoxButton.OK, MessageBoxImage.Information);
        UpdateShip.Visibility = Visibility.Hidden;
        UpdateDelivery.Visibility = Visibility.Visible;
    }
}


