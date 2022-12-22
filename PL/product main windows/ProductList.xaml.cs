
using BO;
using DalApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.product_main_windows;
/// <summary>
/// Interaction logic for ProductList.xaml
/// </summary>
public partial class ProductList : Window
{
    /// <summary>
    /// access to the logical layyer.
    /// </summary>
    private BlApi.IBl? _bl = BlApi.Factory.Get();
    /// <summary>
    /// constractor
    /// </summary>
    public ProductList()
    {
        InitializeComponent();
        ProductlistView.ItemsSource = _bl?.Product.GetList();
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.CoffeeShop));
    }
    /// <summary>
    /// category selector in the combo box
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

        var combo = sender as ComboBox;
        var s = Convert.ToString(combo!.SelectedItem);
        CoffeeShop Categoryname = s switch
        {
            "COFFE_MACHINES" => CoffeeShop.COFFE_MACHINES,
            "CAPSULES" => CoffeeShop.CAPSULES,
            "ACCESSORIES" => CoffeeShop.ACCESSORIES,
            "FROTHERS" => CoffeeShop.FROTHERS,
            "SWEETS" => CoffeeShop.SWEETS
        };
        ProductlistView.ItemsSource = _bl?.Product.GetList(element => element?.Category == Categoryname);
    }
    /// <summary>
    /// button to reset the list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Reset_button_Click(object sender, RoutedEventArgs e)
    {
        ProductlistView.ItemsSource = _bl?.Product.GetList();
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.CoffeeShop));
    }
    /// <summary>
    /// button to add product
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Add_Product_Button_Click(object sender, RoutedEventArgs e) => new AddOrUpdateProductWindow(_bl).Show();
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
                new AddOrUpdateProductWindow(_bl, productForList.ID).Show();
        }
    }
    /// <summary>
    /// event double click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ProductlistView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
}
