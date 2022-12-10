using BlApi;
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
        private IBl bl = new Bl();
        public ImageSource background { set; get; }
        public MainWindow()
        {
           // string s = Directory.GetCurrentDirectory();
           // background = new BitmapImage(new Uri(@$"{s}\images\Logo.png"));
            InitializeComponent();
           
        }
        private void ShowProductButton_Click(object sender, RoutedEventArgs e) => new ProductList().Show();
    }
}
