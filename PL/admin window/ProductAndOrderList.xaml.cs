using BlApi;
using BlImplementation;
using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL.admin_window;
/// <summary>
/// Interaction logic for ProductList.xaml
/// </summary>
public partial class ProductAndOrderList : Window , INotifyPropertyChanged
{
    /// <summary>
    /// access to the logical layyer.
    /// </summary>
    private BlApi.IBl? _bl;

    public event PropertyChangedEventHandler? PropertyChanged;
    IEnumerable<IGrouping<BO.CoffeeShop?, ProductForList>> groups;
    /// <summary>
    /// initialize depency property
    /// </summary>
    private IEnumerable<BO.ProductForList?>? productList_p;
    public IEnumerable<BO.ProductForList?>? productList { get { return productList_p; } set { productList_p = value; if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("productList")); } } }

    private IEnumerable<BO.OrderForList?>? orderList_p;
    public IEnumerable<BO.OrderForList?>? orderList { get { return orderList_p; } set { orderList_p = value; if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("orderList")); } } }

    private IEnumerable<StatisticksOrders> statisticksOrdersByMonthsAndYear_p;
    public IEnumerable<StatisticksOrders> statisticksOrdersByMonthsAndYear { get { return statisticksOrdersByMonthsAndYear_p; } set { statisticksOrdersByMonthsAndYear_p = value; if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("statisticksOrdersByMonthsAndYear")); } } }

    /// <summary>
    /// constractor
    /// </summary>
    public ProductAndOrderList(BlApi.IBl? _bl)
    {
        this._bl = _bl;
        Simulator.Simulator.ActionForAdmin += OnChangeOrder;
        this.Closed += MainWindow_Closed;
        InitializeComponent();
        productList = _bl?.Product.GetList()!;
        orderList = _bl?.Order.GetList()!;
        groups = from item in productList
                 group item by item.Category into x
                 select x;
        initStatisticks();

        _bl!.Cart.Action += initStatisticks;

        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.CoffeeShop));
    }

    private void initStatisticks()
    {
        statisticksOrdersByMonthsAndYear = _bl!.Order.GetStatisticksOrdersByMonthsAndYear();
    }

    private void MainWindow_Closed(object sender, EventArgs e)
    {
        // Perform actions when the window is closed
        Simulator.Simulator.ActionForAdmin -= OnChangeOrder;
    }
    /// <summary>
    /// category selector in the combo box
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (CategorySelector.SelectedIndex >= 0)
            productList = groups.FirstOrDefault(item => (BO.CoffeeShop)CategorySelector.SelectedItem == item.Key);
    }
    /// <summary>
    /// button to reset the list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Reset_button_Click(object sender, RoutedEventArgs e)
    {
        productList = groups.SelectMany(x => x);
        CategorySelector.SelectedIndex = -1;
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.CoffeeShop));
    }
    /// <summary>
    /// button to add product
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Add_Product_Button_Click(object sender, RoutedEventArgs e) => new AddOrUpdateProductWindow(_bl, OnChangeProduct).ShowDialog();
    /// <summary>
    /// event double click to open the new window of update product with the id that chooce.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ProductlistView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (ProductlistView.SelectedItem is ProductForList productForList)
        {
            if(IsMouseCaptureWithin)
                new AddOrUpdateProductWindow(_bl, productForList.ID, OnChangeProduct).ShowDialog();
        }
    }
    /// <summary>
    /// function to the mouse double click that send us the window with the details of the product
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OrderlistView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (IsMouseCaptureWithin)
            new OrderDatails(_bl, ((BO.OrderForList)OrderlistView.SelectedItem).ID, OnChangeOrder).ShowDialog();
    }
    /// <summary>
    /// deleget for the product list that we want to update him aoutomaticly.
    /// </summary>
    private void OnChangeProduct()
    {
        productList = _bl?.Product.GetList();
    }
    /// <summary>
    /// deleget for the order list that we want to update him aoutomaticly.
    /// </summary>
    private void OnChangeOrder()
    {
        orderList = _bl?.Order.GetList();
    }
}
