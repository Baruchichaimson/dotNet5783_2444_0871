﻿using BO;
using PL.cart_main_windows;
using PL.new_order_window;
using System.Windows;
namespace PL.client_window;

/// <summary>
/// Interaction logic for clientwindow.xaml
/// </summary>
public partial class clientwindow : Window
{
    public clientwindow()
    {
        InitializeComponent();
    }
    private void ShowProductButton2_Click(object sender, RoutedEventArgs e) => new NewOrder().Show();

    private void ShowProductButton1_Click(object sender, RoutedEventArgs e) => new NewOrder().Show();
}