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
using DataAccess.Repository;
using BusinessObject;
namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class Products : Window
    {
        IMemberRepository memberRepository;
        IProductRepository productRepository;
        IOrderRepository orderRepository;
        IOrderDetailRepository orderDetailRepository;
        public bool isAdmin;
        public int memid;
        public Products(IProductRepository repository)
        {
            InitializeComponent();
            productRepository = repository;
        }

        public Products(IMemberRepository repository, IProductRepository repository1, IOrderRepository repository2, IOrderDetailRepository repository3)
        {
            InitializeComponent();
            memberRepository = repository;
            productRepository = repository1;
            orderRepository = repository2;
            orderDetailRepository = repository3;
        }

        private Product GetProductObject()
        {
            Product mem = null;
            try
            {
                mem = new Product
                {
                    ProductId = int.Parse(txtProductId.Text),
                    CategoryId = int.Parse(txtCategoryId.Text),
                    ProductName = txtProductName.Text,
                    Weight = txtWeight.Text,
                    UnitPrice = decimal.Parse(txtUnitPrice.Text),
                    UnitslnStock = int.Parse(txtUnitInStock.Text)
                };
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Get Product");
            }
            return mem;
        }


        public void LoadProductList()
        {
            lvProducts.ItemsSource = productRepository.GetProducts();
        }
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadProductList();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Product List");
            }
        }
        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product mem = GetProductObject();
                productRepository.InsertProduct(mem);
                LoadProductList();
                MessageBox.Show($"{mem.ProductName} inserted succesfully ", "Insert Product");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Insert Product");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product mem = GetProductObject();
                productRepository.UpdateProduct(mem);
                LoadProductList();
                MessageBox.Show($"{mem.ProductName} updated succesfully ", "Udate Product");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Product");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product mem = GetProductObject();
                var c=orderDetailRepository.GetOrderDetails();
                foreach(var i in c)
                {
                    if (i.ProductId == mem.ProductId)
                    {
                        MessageBox.Show("Cannot delete product because there is an orderdetail used! Please delete order detail first!", "Delete Product");
                        return;
                    }
                }
                productRepository.DeleteProduct(mem.ProductId);
                LoadProductList();
                MessageBox.Show($"{mem.ProductName} deleted succesfully ", "Delete Product");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Product");
            }
        }
        private void btnClose_Click(object sender, RoutedEventArgs e) {
                if (Login.IsWindowOpen<Window>("Members"))
                {
                    Members screen = new Members(memberRepository, productRepository, orderRepository, orderDetailRepository)
                    {
                        isAdmin = this.isAdmin,
                        memid = this.memid
                    };
                    screen.Show();
                    this.Close();
                }
                else if (Login.IsWindowOpen<Window>("Orders"))
                {
                    Orders screen = new Orders(memberRepository, productRepository, orderRepository, orderDetailRepository)
                    {
                        isAdmin = this.isAdmin,
                        memid = this.memid
                    };
                    screen.Show();
                    this.Close();
                }
                else
                {
                    Login screen = new Login(memberRepository, productRepository, orderRepository, orderDetailRepository);
                    screen.Show();
                    this.Close();
                }
            }

        private void btnMember_Click(object sender, RoutedEventArgs e)
        {
            Members screen = new Members(memberRepository, productRepository, orderRepository, orderDetailRepository) {
                isAdmin = this.isAdmin,
                memid = this.memid
            };
            screen.Show();
            this.Close();
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            if (!Login.IsWindowOpen<Window>("Orders"))
            { 
                Orders screen = new Orders(memberRepository, productRepository, orderRepository, orderDetailRepository)
            {
                isAdmin = this.isAdmin,
                memid = this.memid
            };
            screen.Show();
            this.Close();
            }
            else
            {
                MessageBox.Show("Order Window has been opened, please close this window and turn back!", "Change window");
            }
}

        /*private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            
            if (!String.IsNullOrWhiteSpace(txtProductName.Text.ToString()))
            {
                lvProducts.ItemsSource = productRepository.GetProductByName(txtProductName.Text);
            }
            else if (!String.IsNullOrWhiteSpace(txtUnitInStock.Text.ToString()))
            {
                lvProducts.ItemsSource = productRepository.GetProductByUnitInStock(int.Parse(txtUnitInStock.Text));
            }
            else if (!String.IsNullOrWhiteSpace(txtUnitPrice.Text.ToString()))
            {
                lvProducts.ItemsSource = productRepository.GetProductByUnitPrice(decimal.Parse(txtUnitPrice.Text));
            }
            else if (!String.IsNullOrWhiteSpace(txtProductId.Text.ToString()))
            {
                lvProducts.ItemsSource = (System.Collections.IEnumerable)productRepository.GetProductByID(int.Parse(txtProductId.Text));
            }
        }*/
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            lvProducts.ItemsSource = productRepository.Filter(txtProductName.Text, txtUnitPrice.Text, txtUnitInStock.Text, txtProductId.Text);
        }
    }
}
