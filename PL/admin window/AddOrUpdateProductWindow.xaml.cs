using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections;

namespace PL.admin_window;

internal class Categories : IEnumerable
{
    static readonly IEnumerator s_enumerator = Enum.GetValues(typeof(BO.CoffeeShop)).GetEnumerator();
    public IEnumerator GetEnumerator() => s_enumerator;
}

/// <summary>
/// Interaction logic for AddOrUpdateProductWindow.xaml
/// </summary>
public partial class AddOrUpdateProductWindow : Window , INotifyPropertyChanged 
{
    BlApi.IBl? _bl;
    public event PropertyChangedEventHandler? PropertyChanged;
    private BO.Product Product_p;
    Action changeList;
    public BO.Product productDetail { get { return Product_p; } set { Product_p = value; if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("productdetail")); } } }
    /// <summary>
    /// main constructor 
    /// </summary>
    /// <param name="_blForAdd"></param>
    /// <param name="c"></param>
    public AddOrUpdateProductWindow(BlApi.IBl? _blForAdd, bool c)
    {
        InitializeComponent(); 
        _bl = _blForAdd;
        categorychoose.ItemsSource = Enum.GetValues(typeof(BO.CoffeeShop));
    }
    /// <summary>
    /// constructor for update window
    /// </summary>
    /// <param name="_blForAdd"></param>
    /// <param name="c"></param>
    public AddOrUpdateProductWindow(BlApi.IBl? _blForAdd, int productId , Action action) : this(_blForAdd, true)
    {
        //////////////////////////////////////////////////////////////////////////
        productDetail = _bl?.Product.GetData(productId)!;
        changeList = action;
        addOrUpdateProdut.Content = "Update";
        id.IsEnabled = false;
    }
    /// <summary>
    /// constructor for add window
    /// </summary>
    /// <param name="_blForAdd"></param>
    /// <param name="c"></param>
    public AddOrUpdateProductWindow(BlApi.IBl? _blForAdd, Action action) : this(_blForAdd, true)
    {
        changeList = action;
        productDetail = new BO.Product();
        addOrUpdateProdut.Content = "Add";
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
            if (addOrUpdateProdut?.Content == "Add")
                _bl?.Product.Add(productDetail);
            else
                _bl?.Product.Update(productDetail);
            changeList();
            Close();
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

    /// <summary>
    /// dont put numbers in the text box
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void name_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = Regex.IsMatch(e.Text , "/^[a - z,.'-]+$/i");
    }
    /// <summary>
    /// dont put latters in the text box
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void id_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = Regex.IsMatch(e.Text , "^[^0-9]+$");
    }
    /// <summary>
    /// dont put latters in the text box
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void price_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = Regex.IsMatch(e.Text , "/ ^[0 - 9] + (\\.[0 - 9] +)?$");
    }
    /// <summary>
    /// dont put latters in the text box
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void instoke_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = Regex.IsMatch(e.Text , "^[^0-9]+$");
    }
    private void price_TextChanged(object sender, TextChangedEventArgs e)
    {
        changeList();
        var textPrice = sender as TextBox;
        if (double.TryParse(textPrice?.Text, out double z))
            productDetail.Price = Convert.ToDouble(textPrice?.Text);
        else
            productDetail.Price = 0;
    }
}
