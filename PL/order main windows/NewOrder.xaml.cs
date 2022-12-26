using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.new_order_window
{
    /// <summary>
    /// Interaction logic for NewOrder.xaml
    /// </summary>
    public partial class NewOrder : Window
    {
        private BlApi.IBl? _bl = BlApi.Factory.Get();
        private BO.Cart _cart;
        IEnumerable<IGrouping<BO.CoffeeShop?, ProductForList>> groups;
        private IEnumerable<BO.ProductItem?> productItems;
        public NewOrder()
        {
            InitializeComponent();
            _cart = new BO.Cart();
            productItems = from item in _bl?.Product.GetList()!
                           select _bl?.Product.GetData(item.ID, _cart)!;
            ProductItemlistView.ItemsSource = productItems;
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.CoffeeShop));
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = sender as ComboBox;
            var s = Convert.ToString(combo!.SelectedItem);
            productItems = from item in _bl?.Product.GetList()
                           let category = item.Category
                           where category == (BO.CoffeeShop)CategorySelector.SelectedItem
                           select _bl?.Product.GetData(item.ID, _cart);
            ProductItemlistView.ItemsSource = productItems;
        }


        private void ProductlistView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Reset_button_Click(object sender, RoutedEventArgs e)
        {
            productItems = from item in _bl?.Product.GetList()!
                           select _bl?.Product.GetData(item.ID, _cart)!;
            ProductItemlistView.ItemsSource = productItems;
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.CoffeeShop));
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
        }

    }
}
