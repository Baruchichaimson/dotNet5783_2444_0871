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

        /// <summary>
        /// Constructor for the ProductItemWindow class.
        /// </summary>
        /// <param name="_blForAdd">An optional parameter of type BlApi.IBl that represents the business logic object.</param>
        /// <param name="newProductItem">An object of type BO.ProductItem that represents the new product item.</param>
        /// <param name="newcart">An object of type BO.Cart that represents the new cart.</param>
        /// <param name="SendListChanged">A delegate that represents an action to be taken when the list changes.</param>
        public ProductItemWindow(BlApi.IBl? _blForAdd, BO.ProductItem newProductItem , BO.Cart newcart, Action SendListChanged)
        {
            _bl = _blForAdd;
            cart = newcart;
            productItemWindow = newProductItem;
            ListChanged = SendListChanged;
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }
        /// <summary>
        /// function to the button add to cart and active the delget to update aoutomaticly in the list.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
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
