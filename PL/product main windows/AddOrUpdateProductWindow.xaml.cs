using BO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        BlApi.IBl? _bl;
        Regex regex;

        ProductList productListWindow;
        private BO.Product currentProduct;
        /// <summary>
        /// main constructor 
        /// </summary>
        /// <param name="_blForAdd"></param>
        /// <param name="c"></param>
        public AddOrUpdateProductWindow(BlApi.IBl? _blForAdd, ProductList sender , bool c)
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
        public AddOrUpdateProductWindow(BlApi.IBl? _blForAdd, int productId , ProductList sender) : this(_blForAdd, sender, true)
        {
            //////////////////////////////////////////////////////////////////////////
            currentProduct = _bl?.Product.GetData(productId) ?? throw new Exception("");
            DataContext = currentProduct;
            productListWindow = sender;
            addOrUpdateProdut.Content = "Update";
            id.IsEnabled = false;
        }
        /// <summary>
        /// constructor for add window
        /// </summary>
        /// <param name="_blForAdd"></param>
        /// <param name="c"></param>
        public AddOrUpdateProductWindow(BlApi.IBl? _blForAdd, ProductList sender) : this(_blForAdd, sender, true)
        {
            currentProduct = new BO.Product();
            DataContext = currentProduct;
            productListWindow = sender;
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
                if (int.TryParse(id.Text, out int x) == false || string.IsNullOrEmpty(name.Text) || double.TryParse(price.Text, out double y) == false || int.TryParse(instoke.Text, out int z) == false)
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
            currentProduct.Price = Convert.ToDouble(textPrice?.Text);
        }
    }
}
