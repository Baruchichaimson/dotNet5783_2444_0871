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
    /// <summary>
    /// class to input in him more function we need on the list we have in bo layer.
    /// </summary>
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
        /// <summary>
        /// constractor that make for us 2 function from command
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="bl"></param>
        /// <param name="action">deleget</param>
        /// <param name="deleteProduct">deleget</param>
        public OrderItemViewModel(Cart cart, IBl? bl, Action action, Action deleteProduct)
        {
            IncreaseAmountCommand = new RelayCommand(IncreaseAmount);
            DecreaseAmountCommand = new RelayCommand(DecreaseAmount);
            this.cart = cart;
            _bl = bl;
            ListChanged = action;
            DeleteProduct = deleteProduct;  
        }
        /// <summary>
        /// function to increase the amount with property change.
        /// </summary>
        private void IncreaseAmount()
        {
            Item.Amount++;
            Item.TotalPrice += Item.Price;
            _bl?.Cart.UpdateProductAmount(cart,Item.ProductID,Item.Amount);
            ListChanged();
            OnPropertyChanged("Item");
        }
        /// <summary>
        /// function to decrease the amount with property change.
        /// </summary>
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
        /// <summary>
        /// the function run on all the event that sign up and active them to up or down the amount and the total price
        /// </summary>
        /// <param name="propertyName"></param>
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
        /// <summary>
        /// constractor
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="newCart"></param>
        /// <param name="action">delgete</param>
        public CartList(BlApi.IBl? bl , BO.Cart newCart ,Action action)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            _bl = bl;
            cart = newCart;
            ListChanged = action;
            CartItems = from item in cart.Items 
                        select new OrderItemViewModel(cart,_bl,ListChanged, DeleteProduct) { Item = item };
        }
        /// <summary>
        /// function that update the old list with the new list.
        /// </summary>
        private void DeleteProduct()
        {
            CartItems = from item in cart.Items!
                        select new OrderItemViewModel(cart, _bl, ListChanged, DeleteProduct) { Item = item };
        }
        /// <summary>
        /// the function to the button that start the order anf confirmat him.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void order_now(object sender, RoutedEventArgs e)
        {
            var newOrderWindow = new Order_Confirmation(_bl, cart);
            newOrderWindow.ShowDialog();
            Close();
        }
    }
}
