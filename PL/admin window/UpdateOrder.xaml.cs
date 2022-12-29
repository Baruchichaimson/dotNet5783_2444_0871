using System;
using System.Windows;
namespace PL.admin_window;


/// <summary>
/// Interaction logic for UpdateOrder.xaml
/// </summary>
public partial class UpdateOrder : Window
{
    private BlApi.IBl? _bl;

    private BO.Order currentOrder;

    public UpdateOrder(BlApi.IBl? bl, int orderId)
    {
        InitializeComponent();
        _bl = bl;
        currentOrder = bl?.Order.GetData(orderId) ?? throw new Exception("");
        DataContext = currentOrder;
        UpdateDelivery.Content = "Update Delivery";
        ItemListView.ItemsSource = currentOrder.Items;

        if (currentOrder.ShipDate is not null)
            UpdateShip.Visibility = Visibility.Hidden;
        else
            UpdateDelivery.Visibility = Visibility.Hidden;

        if (currentOrder.DeliveryrDate is not null)
            UpdateDelivery.Visibility = Visibility.Hidden;
    }

    private void UpdateDeliveryDate(object sender, RoutedEventArgs e)
    {
        currentOrder = _bl?.Order.DeliveryUpdate(currentOrder.ID)!;
        DataContext = currentOrder;
        Status.Text = currentOrder.Status.ToString();
        MessageBox.Show("SUCCSES", "SUCCSES", MessageBoxButton.OK, MessageBoxImage.Information);
        UpdateDelivery.Visibility = Visibility.Hidden;
    }


    private void UpdateShipDate(object sender, RoutedEventArgs e)
    {
        currentOrder = _bl?.Order.UpdateShippingDate(currentOrder.ID)!;
        DataContext = currentOrder;
        Status.Text = currentOrder.Status.ToString();
        MessageBox.Show("SUCCSES", "SUCCSES", MessageBoxButton.OK, MessageBoxImage.Information);
        UpdateShip.Visibility = Visibility.Hidden;
        UpdateDelivery.Visibility = Visibility.Visible;
    }

}


