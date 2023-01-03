using BlApi;
using BO;
using PL.admin_window;
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

namespace PL.order_main_windows
{
    /// <summary>
    /// Interaction logic for ProductItemWindow.xaml
    /// </summary>
    public partial class ProductItemWindow : Window, INotifyPropertyChanged
    {
        private BlApi.IBl? _bl;
        BO.Cart? cart;
        Action ListChanged;
        public event PropertyChangedEventHandler? PropertyChanged;
        private ProductItem productItem;
        public ProductItem productItemWindow { get { return productItem; } set { productItem = value; if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("productItemWindow")); } } }

       
        public ProductItemWindow(BlApi.IBl? _blForAdd, BO.ProductItem newProductItem , BO.Cart newcart, Action SendListChanged)
        {
            _bl = _blForAdd;
            cart = newcart;
            productItemWindow = newProductItem;
            ListChanged = SendListChanged;
            InitializeComponent();
        }
        
        private void addToCart(object sender, RoutedEventArgs e)
        {
            try
            {
                _bl?.Cart.AddProduct(cart, productItemWindow.ID);
            }
            catch(BO.IncorrectAmountException ex)
            {
                MessageBox.Show(ex.Message);
            }
            ListChanged();
            Close();
        }
    }
}
