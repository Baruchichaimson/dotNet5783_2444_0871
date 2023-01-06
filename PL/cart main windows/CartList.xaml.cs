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
        /// Constructor for the OrderItemViewModel class.
        /// </summary>
        /// <param name="cart">An object of type Cart that represents the cart.</param>
        /// <param name="bl">An optional parameter of type IBl that represents the business logic object.</param>
        /// <param name="action">A delegate that represents an action to be taken when the list changes.</param>
        /// <param name="deleteProduct">A delegate that represents an action to be taken when a product is deleted.</param>
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
        /// Method that is called when a property is changed.
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed. This parameter is optional and will default to null if not provided.</param>
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
        /// Constructor for the CartList class.
        /// </summary>
        /// <param name="bl">An optional parameter of type BlApi.IBl that represents the business logic object.</param>
        /// <param name="newCart">An object of type BO.Cart that represents the new cart.</param>
        /// <param name="action">A delegate that represents an action to be taken when the list changes.</param>
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
        ///<summary>
        /// Event handler for the order now button. Opens the Order Confirmation window and closes the current window.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void order_now(object sender, RoutedEventArgs e)
        {
            var newOrderWindow = new Order_Confirmation(_bl, cart);
            newOrderWindow.ShowDialog();
            Close();
        }
    }
}
