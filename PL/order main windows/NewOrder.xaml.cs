﻿using BO;
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

namespace PL.new_order_window;

/// <summary>
/// Interaction logic for NewOrder.xaml
/// </summary>
public partial class NewOrder : Window , INotifyPropertyChanged 
{
    private BlApi.IBl? _bl = BlApi.Factory.Get();
    private BO.Cart cart;
    IEnumerable<IGrouping<BO.CoffeeShop?, ProductItem>> groups;
    public event PropertyChangedEventHandler? PropertyChanged;
    private IEnumerable<BO.ProductItem?>? productItemsp;
    public IEnumerable<BO.ProductItem?>? productItems { get { return productItemsp; } set { productItemsp = value; if (PropertyChanged != null)  { PropertyChanged(this, new PropertyChangedEventArgs("productItems")); } }}

    public NewOrder()
    {
        InitializeComponent();
        cart = new BO.Cart();
        groups = from item in _bl?.Product.GetListProductItem(cart)
                 group item by item.Category into x
                 select x;
        productItems = _bl?.Product.GetListProductItem(cart)!;
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.CoffeeShop));
    }
     private void OnChange(int productId)
    {
        productItems = productItems.Select(x => { return x; });
       
    }

    private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (CategorySelector.SelectedIndex >= 0)
        productItems = groups.FirstOrDefault(item => (BO.CoffeeShop)CategorySelector.SelectedItem == item.Key);
    }
    private void ProductlistView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
    private void Reset_button_Click(object sender, RoutedEventArgs e)
    {
        productItems = from item in _bl?.Product.GetListProductItem(cart)!
                       select item;
        CategorySelector.SelectedIndex = -1;
    }

    private void Cart_button_Click(object sender, RoutedEventArgs e) => new CartList(_bl, cart).Show();

    private void ProductItemlistView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {

        if (ProductItemlistView.SelectedItem is BO.ProductItem Item)
        {
            if (IsMouseCaptureWithin)
                new ProductItemWindow(_bl, Item , cart, OnChange).Show();
        }

    }
}
