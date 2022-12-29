using BlApi;
using BO;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PL.new_order_window;
using System.Collections.Generic;

namespace PL.admin_window;

/// <summary>
/// Interaction logic for AddOrUpdateProductWindow.xaml
/// </summary>
public partial class AddOrUpdateProductWindow : Window
{
    BlApi.IBl? _bl;
    Regex regex;

    ProductAndOrderList productListWindow;
    public static readonly DependencyProperty productAddOrUpdateProp = DependencyProperty.Register(nameof(currentProduct), typeof(BO.Product), typeof(AddOrUpdateProductWindow), new PropertyMetadata(null));
    public BO.Product currentProduct { get => (BO.Product)GetValue(productAddOrUpdateProp); set => SetValue(productAddOrUpdateProp, value); }
    private IBl? bl;
    private int iD;
    private NewOrder newOrder;

    /// <summary>
    /// main constructor 
    /// </summary>
    /// <param name="_blForAdd"></param>
    /// <param name="c"></param>
    public AddOrUpdateProductWindow(BlApi.IBl? _blForAdd, ProductAndOrderList sender , bool c)
    {
        InitializeComponent(); 
        _bl = _blForAdd;
        productListWindow = sender;
        categorychoose.ItemsSource = Enum.GetValues(typeof(BO.CoffeeShop));
    }
    /// <summary>
    /// constructor for update window
    /// </summary>
    /// <param name="_blForAdd"></param>
    /// <param name="c"></param>
    public AddOrUpdateProductWindow(BlApi.IBl? _blForAdd, int productId , ProductAndOrderList sender) : this(_blForAdd, sender, true)
    {
        //////////////////////////////////////////////////////////////////////////
        currentProduct = _bl?.Product.GetData(productId)!;
       // DataContext = currentProduct;  
        addOrUpdateProdut.Content = "Update";
        id.IsEnabled = false;
    }
    /// <summary>
    /// constructor for add window
    /// </summary>
    /// <param name="_blForAdd"></param>
    /// <param name="c"></param>
    public AddOrUpdateProductWindow(BlApi.IBl? _blForAdd, ProductAndOrderList sender) : this(_blForAdd, sender, true)
    {
        //currentProduct = new BO.Product();
       // DataContext = currentProduct;
        addOrUpdateProdut.Content = "Add";
    }

    public AddOrUpdateProductWindow(IBl? bl, int iD, NewOrder newOrder)
    {
        this.bl = bl;
        this.iD = iD;
        this.newOrder = newOrder;
    }

    /// <summary>
    /// the button the make the add or update product
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void addOrUpdateProductButton(object sender, RoutedEventArgs e)
    {
        try
        { 
            if (int.TryParse(id.Text, out int x) == false || categorychoose.SelectedItem == null || string.IsNullOrEmpty(name.Text) || double.TryParse(price.Text, out double y) == false || int.TryParse(instoke.Text, out int z) == false)
            {
                MessageBox.Show("missing details");
                return;
            }
            if (addOrUpdateProdut?.Content == "Add")
                _bl?.Product.Add(currentProduct);
            else
                _bl?.Product.Update(currentProduct);
            Close();
            productListWindow.listProduct = _bl?.Product.GetList()!;
        }
        catch(BO.AllreadyExistException ex) when (ex.InnerException is not null)
        {
            MessageBox.Show(ex.Message + ex.InnerException!.Message);
        }
        catch(BO.EntityDetailsWrongException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        
    }
    /// <summary>
    /// dont put numbers in the text box
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void name_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        regex = new Regex("/^[a - z,.'-]+$/i");
        e.Handled = regex.IsMatch(e.Text);
    }
    /// <summary>
    /// dont put latters in the text box
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void id_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        regex = new Regex("^[^0-9]+$");
        e.Handled = regex.IsMatch(e.Text);
    }
    /// <summary>
    /// dont put latters in the text box
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void price_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        regex = new Regex("/ ^[0 - 9] + (\\.[0 - 9] +)?$");
        e.Handled = regex.IsMatch(e.Text);
    }
    /// <summary>
    /// dont put latters in the text box
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void instoke_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        regex = new Regex("^[^0-9]+$");
        e.Handled = regex.IsMatch(e.Text);
    }

    private void price_TextChanged(object sender, TextChangedEventArgs e)
    {
        var textPrice = sender as TextBox;
        if (double.TryParse(textPrice?.Text, out double z))
            currentProduct.Price = Convert.ToDouble(textPrice?.Text);
        else
            currentProduct.Price = 0;
    }
}
