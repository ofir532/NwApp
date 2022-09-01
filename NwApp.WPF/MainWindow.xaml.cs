using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
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
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            var query = from region in db.Regions select region;

            Dgrid_Update(query.ToList());
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            var query = from region in db.Regions
                        select new { region = region.RegionDescription };

            Dgrid_Update(query.ToList());
        }

        private void Btn3_Click(object sender, RoutedEventArgs e)
        {
            var query = from territory in db.Territories select territory;

            Dgrid_Update(query.ToList());
        }

        private void Btn4_Click(object sender, RoutedEventArgs e)
        {
            var query = from territory in db.Territories
                        select new { description = territory.TerritoryDescription };

            Dgrid_Update(query.ToList());
        }

        private void Btn5_Click(object sender, RoutedEventArgs e)
        {
            var query = from region in db.Regions
                        orderby region.RegionDescription descending
                        join territory in db.Territories
                        on region.RegionId equals territory.RegionId
                        select new { Region = region.RegionDescription, territory = territory.TerritoryDescription };

            Dgrid_Update(query.ToList());
        }
        private void Dgrid_Update(IEnumerable<Object> query)
        {
            Dgrid.ItemsSource = query;
        }

        private void Btn6_Click(object sender, RoutedEventArgs e)
        {
            int order = db.Orders.Skip(db.Orders.Count() - 1).FirstOrDefault().OrderId;

            OrderDetail ord1 = new() { ProductId = 11, OrderId = order, UnitPrice = 95, Quantity = 3, Discount = 0 };
            OrderDetail ord2 = new() { ProductId = 56, OrderId = order, UnitPrice = 47, Quantity = 6, Discount = 0 };
            OrderDetail ord3 = new() { ProductId = 74, OrderId = order, UnitPrice = 120, Quantity = 5, Discount = 0 };

            db.Orders.Add(new Order()
            {
                CustomerId = "FRANK",
                EmployeeId = 6,
                OrderDate = DateTime.Now,
                ShipAddress = "7 Piccadilly Rd.",
                ShipCity = "New York",
                ShipCountry = "New York",
            });

            db.SaveChanges();

            db.OrderDetails.Add(ord1);
            db.OrderDetails.Add(ord2);
            db.OrderDetails.Add(ord3);

            db.SaveChanges();

            var query = from ord in db.OrderDetails
                        select new { ord.OrderId, ord.ProductId, ord.UnitPrice, ord.Quantity, ord.Discount };

            Dgrid_Update(query.ToList());
        }

        private void Btn7_Click(object sender, RoutedEventArgs e)
        {
            var query = from order in db.Orders
                        join detail in db.OrderDetails
                        on order.OrderId equals detail.OrderId
                        join product in db.Products
                        on detail.ProductId equals product.ProductId
                        join employee in db.Employees
                        on order.EmployeeId equals employee.EmployeeId
                        select new { orderId = order.OrderId, product = product.ProductName, employee = employee.FirstName };

            Dgrid_Update(query.ToList());
        }

        private void Btn8_Click(object sender, RoutedEventArgs e)
        {
            int order = db.Orders.Skip(db.Orders.Count() - 1).FirstOrDefault().OrderId;
            var record = db.Orders.SingleOrDefault(x => x.OrderId == order);
            record.EmployeeId = 5;

            var query = from ord in db.Orders
                        where ord.OrderId == order
                        select ord;

            Dgrid_Update(query.ToList());
        }

        private void Btn9_Click(object sender, RoutedEventArgs e)
        {
            int order = db.OrderDetails.Skip(db.OrderDetails.Count() - 1).FirstOrDefault().OrderId;
            db.OrderDetails.Remove(db.OrderDetails.Single(x => x.ProductId == 56 && x.OrderId == order));

            var query = from item in db.OrderDetails select item;

            Dgrid_Update(query.ToList());
        }

    }
}
