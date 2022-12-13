
using BlImplementation;

using PL.product_main_windows;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// accses for the logical layyer.
        /// </summary>
        private BlApi.IBl? _bl = BlApi.Factory.Get();
        /// <summary>
        /// constructor
        /// </summary>
        public MainWindow()
        {
           // string s = Directory.GetCurrentDirectory();
           // background = new BitmapImage(new Uri(@$"{s}\images\Logo.png"));
            InitializeComponent();
        }
        /// <summary>
        /// event to double click to go update the product.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowProductButton_Click(object sender, RoutedEventArgs e) => new ProductList().Show();
    }
}
