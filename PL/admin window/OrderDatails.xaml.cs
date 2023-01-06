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
/// <summary>
///  class to input in him more function we need on the list we have in bo layer.
/// </summary>
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

    /// <summary>
    /// constractor
    /// </summary>
    /// <param name="bl"></param>
    /// <param name="order"></param>
    /// <param name="action">deleget</param>
    public OrderItemViewModel(IBl? bl , BO.Order order , Action action)
    {
        IncreaseAmountCommand = new RelayCommand(IncreaseAmount);
        DecreaseAmountCommand = new RelayCommand(DecreaseAmount); 
        _bl = bl;
        ChangeAmountItem = action;
        myOrder = order;
    }
    /// <summary>
    /// function to increase the amount with property change.
    /// </summary>
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
    /// <summary>
    /// function to decrease the amount with property change.
    /// </summary>
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
    /// <summary>
    /// constractor that make visibilty the buttons in corrently
    /// </summary>
    /// <param name="bl"></param>
    /// <param name="orderId"></param>
    /// <param name="action"></param>
    public OrderDatails(BlApi.IBl? bl, int orderId ,Action action) : this (bl, orderId)
    {
        changeList = action;
        manegerProdactsButtens = Visibility.Visible;
        if (OrderDetail?.ShipDate is not null)
            updateDelivery = Visibility.Visible;
        else
            updateShip = Visibility.Visible;
        if (OrderDetail?.DeliveryrDate is not null)
            updateDelivery = Visibility.Hidden;
    }
    /// <summary>
    /// constarctor that in the start every button is hidden and put on the text box the all details of the order.
    /// </summary>
    /// <param name="bl"></param>
    /// <param name="orderId"></param>
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
    /// <summary>
    /// function to the button update delivery and update aoutomatcly.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UpdateDeliveryDate(object sender, RoutedEventArgs e)
    {
        try
        {
            OrderDetail = _bl?.Order.DeliveryUpdate(OrderDetail!.ID)!;
            changeList();
            MessageBox.Show("SUCCSES", "SUCCSES", MessageBoxButton.OK, MessageBoxImage.Information);
            updateDelivery = Visibility.Hidden;
        }
        catch(BO.EntityDetailsWrongException ex)
        {
            MessageBox.Show(ex.Message);
        }
        Close();
    }

    /// <summary>
    /// function to the button update shipe date and update aoutomatcly.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UpdateShipDate(object sender, RoutedEventArgs e)
    {
        try
        {
            OrderDetail = _bl?.Order.UpdateShippingDate(OrderDetail!.ID)!;
            changeList();
            MessageBox.Show("SUCCSES", "SUCCSES", MessageBoxButton.OK, MessageBoxImage.Information);
            updateShip = Visibility.Hidden;
            updateDelivery = Visibility.Visible;
        }
        catch(BO.EntityDetailsWrongException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    /// <summary>
    /// function to moove the deleget to the class with new
    /// function that we build in the command to update 
    /// aoutomatcly the amount and the total price if we click on the button + or -
    /// </summary>
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


