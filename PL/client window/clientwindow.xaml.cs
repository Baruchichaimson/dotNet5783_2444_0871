﻿using BO;
using PL.cart_main_windows;
using PL.new_order_window;
using PL.order_main_windows;
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
        WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
    }
    /// <summary>
    /// finction to the button the open to me the list with the item in store.
    /// </summary>
    /// <param name="sender">The object that raised the event.</param>
    /// <param name="e">Event arguments.</param>
    private void ShowProductButton2_Click(object sender, RoutedEventArgs e) => new NewOrder().ShowDialog();
    /// <summary>
    /// function to the button the open to me the window to order tracking.
    /// </summary>
    /// <param name="sender">The object that raised the event.</param>
    /// <param name="e">Event arguments.</param>
    private void ShowProductButton1_Click(object sender, RoutedEventArgs e) => new OrderTrackingWindow().ShowDialog();
}
