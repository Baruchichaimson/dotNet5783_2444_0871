using BlApi;
using PL.cart_main_windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace PL.admin_window;

public class OrderItemViewModel : INotifyPropertyChanged
{
    private BlApi.IBl? _bl;
    Action ChangeAmountItem;
    private BO.Order myOrder;
    private BO.OrderItem _item;
    public BO.OrderItem Item
    {
        get { return _item; }
        set
        {
            _item = value;
            OnPropertyChanged();
        }
    }

    public ICommand IncreaseAmountCommand { get; set; }
    public ICommand DecreaseAmountCommand { get; set; }


    public OrderItemViewModel(IBl? bl , BO.Order order , Action action)
    {
        IncreaseAmountCommand = new RelayCommand(IncreaseAmount);
        DecreaseAmountCommand = new RelayCommand(DecreaseAmount); 
        _bl = bl;
        ChangeAmountItem = action;
        myOrder = order;
    }

    private void IncreaseAmount()
    {
        try
        {
            _bl?.Order.UpdateAdmin(myOrder.ID, Item.ProductID, 1);
            Item.Amount++;
            Item.TotalPrice += Item.Price;
            OnPropertyChanged("Item");
            ChangeAmountItem();
        }

        catch(Exception ex) when (ex is BO.AllreadyExistException || ex is BO.NullExeptionForDO ||
                                  ex is BO.EntityNotFoundException || ex is BO.EntityDetailsWrongException ||
                                  ex is BO.IncorrectAmountException || ex is BO.NullExeption)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void DecreaseAmount()
    {
        try
        {
            if (Item.Amount > 0)
            {
                _bl?.Order.UpdateAdmin(myOrder.ID, Item.ProductID, -1);
                Item.Amount--;
                Item.TotalPrice -= Item.Price;
                OnPropertyChanged("Item");
                ChangeAmountItem();
            }
        }
         catch (Exception ex) when (ex is BO.AllreadyExistException || ex is BO.NullExeptionForDO ||
                                  ex is BO.EntityNotFoundException || ex is BO.EntityDetailsWrongException ||
                                  ex is BO.IncorrectAmountException || ex is BO.NullExeption)
        {
            MessageBox.Show(ex.Message);
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
/// <summary>
/// Interaction logic for UpdateOrder.xaml
/// </summary>
public partial class OrderDatails : Window , INotifyPropertyChanged
{
    public static readonly DependencyProperty UpdateShipProperty =
       DependencyProperty.Register("updateShip", typeof(Visibility), typeof(OrderDatails), new PropertyMetadata(Visibility.Hidden));
    public static readonly DependencyProperty UpdateDeliveryProperty =
      DependencyProperty.Register("updateDelivery", typeof(Visibility), typeof(OrderDatails), new PropertyMetadata(Visibility.Hidden));
    public static readonly DependencyProperty ManegerProdactsButtensProperty =
      DependencyProperty.Register("manegerProdactsButtens", typeof(Visibility), typeof(OrderDatails), new PropertyMetadata(Visibility.Hidden));

    [DependencyProperty]
    public Visibility updateShip
    {
        get { return (Visibility)GetValue(UpdateShipProperty); }
        set { SetValue(UpdateShipProperty, value); }
    }
    [DependencyProperty]
    public Visibility updateDelivery
    {
        get { return (Visibility)GetValue(UpdateDeliveryProperty); }
        set { SetValue(UpdateDeliveryProperty, value); }
    }
    [DependencyProperty]
    public Visibility manegerProdactsButtens
    {
        get { return (Visibility)GetValue(ManegerProdactsButtensProperty); }
        set { SetValue(ManegerProdactsButtensProperty, value); }
    }

    private BlApi.IBl? _bl;

    
    public event PropertyChangedEventHandler? PropertyChanged;
    private IEnumerable<OrderItemViewModel?>? orderItems_p;
    public IEnumerable<OrderItemViewModel?>? OrderItems { get => orderItems_p; set { orderItems_p = value; if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("OrderItems")); } } }

    private BO.Order? OrderDetail_p;
    public BO.Order? OrderDetail { get { return OrderDetail_p; } set { OrderDetail_p = value; if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("OrderDetail")); } } }
    Action changeList;
    public OrderDatails(BlApi.IBl? bl, int orderId ,Action action) : this (bl, orderId)
    {
        changeList = action;
        manegerProdactsButtens = Visibility.Visible;
        if (OrderDetail.ShipDate is not null)
            updateDelivery = Visibility.Visible;
        else
            updateShip = Visibility.Visible;
    }
    public OrderDatails(BlApi.IBl? bl, int orderId)
    {
        InitializeComponent();
        _bl = bl;
        OrderDetail = bl?.Order.GetData(orderId)!;
        updateShip = Visibility.Hidden;
        updateDelivery = Visibility.Hidden;
        manegerProdactsButtens = Visibility.Hidden;
        OrderItems = from item in OrderDetail.Items
                     select new OrderItemViewModel(_bl, OrderDetail, ChangeAmountItem) { Item = item };

    }

    private void UpdateDeliveryDate(object sender, RoutedEventArgs e)
    {
        OrderDetail = _bl?.Order.DeliveryUpdate(OrderDetail!.ID)!;
        changeList();
        MessageBox.Show("SUCCSES", "SUCCSES", MessageBoxButton.OK, MessageBoxImage.Information);
        updateDelivery = Visibility.Hidden;
        Close();
    }


    private void UpdateShipDate(object sender, RoutedEventArgs e)
    {
        OrderDetail = _bl?.Order.UpdateShippingDate(OrderDetail!.ID)!;
        changeList();
        MessageBox.Show("SUCCSES", "SUCCSES", MessageBoxButton.OK, MessageBoxImage.Information);
        updateShip = Visibility.Hidden;
        updateDelivery = Visibility.Visible;
    }
    private void ChangeAmountItem()
    {
        try
        {
            OrderDetail = _bl?.Order.GetData(OrderDetail.ID);
        }
        catch(Exception ex)
        {
            OrderDetail = null;
            Close();
            MessageBox.Show("The order is deleted!");
        }
        changeList();
    }
}


