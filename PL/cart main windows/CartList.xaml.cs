using BlApi;
using BO;
using PL.order_main_windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace PL.cart_main_windows
{
    
    public class OrderItemViewModel : INotifyPropertyChanged
    {
        private Cart cart;
        Action ListChanged;
        Action DeleteProduct;
        private BlApi.IBl? _bl;
        private BO.OrderItem _item;
        public BO.OrderItem Item
        {
            get { return _item; }
            set
            {
                _item = value;
                OnPropertyChanged();
            }
        }

        public ICommand IncreaseAmountCommand { get; set; }
        public ICommand DecreaseAmountCommand { get; set; }

        public OrderItemViewModel(Cart cart, IBl? bl, Action action, Action deleteProduct)
        {
            IncreaseAmountCommand = new RelayCommand(IncreaseAmount);
            DecreaseAmountCommand = new RelayCommand(DecreaseAmount);
            this.cart = cart;
            _bl = bl;
            ListChanged = action;
            DeleteProduct = deleteProduct;  
        }

        private void IncreaseAmount()
        {
            Item.Amount++;
            Item.TotalPrice += Item.Price;
            _bl?.Cart.UpdateProductAmount(cart,Item.ProductID,Item.Amount);
            ListChanged();
            OnPropertyChanged("Item");
        }

        private void DecreaseAmount()
        {
            if (Item.Amount > 0)
            {
                Item.Amount--;
                Item.TotalPrice -= Item.Price;
                _bl?.Cart.UpdateProductAmount(cart, Item.ProductID, Item.Amount);
                ListChanged();
                OnPropertyChanged("Item");
                if (Item.Amount == 0)
                    DeleteProduct();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    /// <summary>
    /// Interaction logic for CartList.xaml
    /// </summary>

    public partial class CartList : Window , INotifyPropertyChanged
    {
        private BlApi.IBl? _bl;
        private Cart cart;
        Action ListChanged;

        public event PropertyChangedEventHandler? PropertyChanged;
        private IEnumerable<OrderItemViewModel?>? cartItems_p;
        public IEnumerable<OrderItemViewModel?>? CartItems { get => cartItems_p; set { cartItems_p = value; if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("CartItems")); } } }

        public CartList(BlApi.IBl? bl , BO.Cart newCart ,Action action)
        {
            InitializeComponent();
            _bl = bl;
            cart = newCart;
            ListChanged = action;
            CartItems = from item in cart.Items 
                        select new OrderItemViewModel(cart,_bl,ListChanged, DeleteProduct) { Item = item };
        }
        private void DeleteProduct()
        {
            CartItems = from item in cart.Items!
                        select new OrderItemViewModel(cart, _bl, ListChanged, DeleteProduct) { Item = item };
        }
        private void order_now(object sender, RoutedEventArgs e)
        {
            var newOrderWindow = new Order_Confirmation(_bl, cart);
            newOrderWindow.ShowDialog();
            Close();
        }
    }
}
