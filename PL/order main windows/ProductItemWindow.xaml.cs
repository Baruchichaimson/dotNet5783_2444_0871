using BlApi;
using BO;
using PL.admin_window;
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

namespace PL.order_main_windows
{
    /// <summary>
    /// Interaction logic for ProductItemWindow.xaml
    /// </summary>
    public partial class ProductItemWindow : Window
    {
        private BlApi.IBl? _bl;
        BO.Cart? cart;
        Action PropertListChanged;
        IEnumerable<BO.ProductItem?> myProducts;
        public static readonly DependencyProperty ProductProp = DependencyProperty.Register(nameof(productItemWindow), typeof(ProductItem), typeof(ProductItemWindow), new PropertyMetadata(null));
        public ProductItem productItemWindow { get => (ProductItem)GetValue(ProductProp); set => SetValue(ProductProp, value); }
        public ProductItemWindow(BlApi.IBl? _blForAdd, BO.ProductItem newProductItem , BO.Cart newcart, Action PropertListChanged1)
        {
            _bl = _blForAdd;
            cart = newcart;
            productItemWindow = newProductItem;
            PropertListChanged = PropertListChanged1;
            InitializeComponent();
        }
        
        private void addToCart(object sender, RoutedEventArgs e)
        {
            _bl?.Cart.AddProduct(cart, productItemWindow.ID);
            myProducts = _bl?.Product.GetListProductItem(cart)!;
            PropertListChanged();
            Close();
        }
    }
}
