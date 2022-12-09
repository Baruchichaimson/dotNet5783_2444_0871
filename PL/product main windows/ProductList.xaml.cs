using BlApi;
using BlImplementation;
using BO;
using System;
using System.Collections.Generic;
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

namespace PL.product_main_windows
{
    public  delegate void AddingNewItemEventArgs(string id);

    /// <summary>
    /// Interaction logic for ProductList.xaml
    /// </summary>


    public partial class ProductList : Window
    {
        private IBl _bl;

        public ProductList()
        {
            InitializeComponent();
            _bl = new Bl();
            ProductlistView.ItemsSource = _bl.Product.GetList();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.CoffeeShop));

        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            var combo = sender as ComboBox;
            var s = Convert.ToString(combo.SelectedItem);
            CoffeeShop Categoryname = s switch
            {
                "COFFE_MACHINES" => CoffeeShop.COFFE_MACHINES,
                "CAPSULES" => CoffeeShop.CAPSULES,
                "ACCESSORIES" => CoffeeShop.ACCESSORIES,
                "FROTHERS" => CoffeeShop.FROTHERS,
                "SWEETS" => CoffeeShop.SWEETS
            };
            ProductlistView.ItemsSource = _bl.Product.GetList(element => element?.Category == Categoryname);
        }

        private void Reset_button_Click(object sender, RoutedEventArgs e)
        {
            ProductlistView.ItemsSource = _bl.Product.GetList();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.CoffeeShop));
        }

        private void Add_Product_Button_Click(object sender, RoutedEventArgs e) => new AddOrUpdateProductWindow(_bl).Show();

        private void ProductlistView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ProductlistView.SelectedItem is ProductForList productForList)
            {
                new AddOrUpdateProductWindow(_bl, productForList.ID).Show();
            }
          
            
        }
    }
}
