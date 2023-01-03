using BO;
using PL.cart_main_windows;
using PL.order_main_windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using System.Windows.Navigation;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;

namespace PL.new_order_window;

/// <summary>
/// Interaction logic for NewOrder.xaml
/// </summary>
public partial class NewOrder : Window , INotifyPropertyChanged 
{
    private BlApi.IBl? _bl = BlApi.Factory.Get();
    private BO.Cart cart;
    private CartList? cartWindow;
    private IEnumerable<IGrouping<BO.CoffeeShop?, ProductItem>> groups_p;
    public IEnumerable<IGrouping<BO.CoffeeShop?, ProductItem>> groups { get { return groups_p; } set { groups_p = value; if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("groups")); } } }
    public event PropertyChangedEventHandler? PropertyChanged;
    private IEnumerable<BO.ProductItem?>? productItemsp;
    public IEnumerable<BO.ProductItem?>? productItems { get { return productItemsp; } set { productItemsp = value; if (PropertyChanged != null)  { PropertyChanged(this, new PropertyChangedEventArgs("productItems")); } }}

    public NewOrder()
    {
        InitializeComponent();
        cart = new BO.Cart();
        productItems = _bl?.Product.GetListProductItem(cart)!;
        groups = from item in productItems
                 group item by item.Category into x
                 select x;
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.CoffeeShop));
    }
    private void OnChange()
    {
        productItems = productItems?.Select(x => x);
    }

    private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (CategorySelector.SelectedIndex >= 0)
             productItems = groups.FirstOrDefault(item => (BO.CoffeeShop)CategorySelector.SelectedItem == item.Key);
    }
   
    private void Reset_button_Click(object sender, RoutedEventArgs e)
    {
        productItems = groups.SelectMany(x => x);
        CategorySelector.SelectedIndex = -1;
    }

    private void Cart_button_Click(object sender, RoutedEventArgs e) 
    {
        if (cartWindow != null && cartWindow.Visibility == Visibility.Visible)
            cartWindow.Visibility = Visibility.Collapsed;
        if (cart.Items == null || cart.Items.Count == 0)
        {
            MessageBox.Show("the cart is empty"); 
            return;
        }
        cartWindow = new CartList(_bl, cart, OnChange);
        cartWindow.ShowDialog();     
    }

    private void ProductItemlistView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (ProductItemlistView.SelectedItem is BO.ProductItem Item)
        {
            if (cartWindow != null && cartWindow.Visibility == Visibility.Visible)
                cartWindow.Visibility = Visibility.Collapsed;
            if (IsMouseCaptureWithin)
                new ProductItemWindow(_bl, Item , cart, OnChange).ShowDialog();
           
        }
    }
}
