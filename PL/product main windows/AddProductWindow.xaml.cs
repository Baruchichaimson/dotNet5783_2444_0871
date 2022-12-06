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
    /// <summary>
    /// Interaction logic for AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        private IBl blForAdd;
        public AddProductWindow(IBl bl)
        {
            blForAdd = bl;
            InitializeComponent();
        }

        private void addProductButton(object sender, RoutedEventArgs e)
        {
            blForAdd.Product.Add(new BO.Product
            {
                ID = int.Parse(id.Text),
                Name = name.Text,
                Price = double.Parse(price.Text),
                InStock = int.Parse(instoke.Text),
                Category = (CoffeeShop)Enum.Parse(typeof(CoffeeShop), category.Text, true)

            });
           this.Close();
        }

    }
}
