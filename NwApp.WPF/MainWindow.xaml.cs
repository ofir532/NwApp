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
using System.Windows.Navigation;
using System.Windows.Shapes;
using NwApp.Entities.Models;
namespace NwApp.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly NorthwindContext db = new NorthwindContext();
        public MainWindow()
        {
            InitializeComponent();
            Dgrid.Visibility = Visibility.Collapsed;
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            var query = from prd in db.Products
                        select prd;
            Dgrid.ItemsSource = query.ToList();
            Dgrid.Visibility = Visibility.Visible;
        }
    }
}
