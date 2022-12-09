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
    /// Interaction logic for AddOrUpdateProductWindow.xaml
    /// </summary>
    public partial class AddOrUpdateProductWindow : Window
    {
        private IBl _blForAdd;

        private BO.Product currentProduct;

        public AddOrUpdateProductWindow(IBl bl, bool c)
        {
            InitializeComponent();
            _blForAdd = bl;
            categorychoose.ItemsSource = Enum.GetValues(typeof(BO.CoffeeShop));
        }

        public AddOrUpdateProductWindow(IBl bl, int productId) : this(bl, true)
        {
            currentProduct = bl.Product.GetData(productId);
            DataContext = currentProduct;
            addOrUpdateProdut.Content = "Update";
        }

        public AddOrUpdateProductWindow(IBl bl) : this(bl, true)
        {
            currentProduct = new BO.Product();
            DataContext = currentProduct;
            addOrUpdateProdut.Content = "Add";
        }

        

        private void addOrUpdateProductButton(object sender, RoutedEventArgs e)
        {
            try
            { 
                var combo = sender as ComboBox;
                if (int.TryParse(id.Text, out int x) == false || string.IsNullOrEmpty(name.Text) || double.TryParse(price.Text, out double y) == false || int.TryParse(instoke.Text, out int z) == false)
                {
                    MessageBox.Show("missing details");
                    return;
                }
                if(addOrUpdateProdut.Content == "Add")
                {
                    _blForAdd.Product.Add(new BO.Product
                    {
                        ID = int.Parse(id.Text),
                        Name = name.Text,
                        Price = double.Parse(price.Text),
                        InStock = int.Parse(instoke.Text),
                        Category = (CoffeeShop)categorychoose.SelectedItem
                    });

                }
                else
                {
                    _blForAdd.Product.Update(new BO.Product
                    {
                        ID = int.Parse(id.Text),
                        Name = name.Text,
                        Price = double.Parse(price.Text),
                        InStock = int.Parse(instoke.Text),
                        Category = (CoffeeShop)categorychoose.SelectedItem
                    });

                }
                this.Close();
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


    }
}
