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
    /// Interaction logic for Orders.xaml
    /// </summary>
    public partial class Orders : Window
    {
        public Orders()
        {
            InitializeComponent();
        }

        IMemberRepository memberRepository;
        IProductRepository productRepository;
        IOrderRepository orderRepository;
        IOrderDetailRepository orderDetailRepository;
        public bool isAdmin;
        public int memid;
        public Orders(IOrderRepository repository, IOrderDetailRepository repository1)
        {
            InitializeComponent();
            orderRepository = repository;
            orderDetailRepository = repository1;
        }
        public Orders(IMemberRepository repository, IProductRepository repository1, IOrderRepository repository2, IOrderDetailRepository repository3)
        {
            InitializeComponent();
            memberRepository = repository;
            productRepository = repository1;
            orderRepository = repository2;
            orderDetailRepository = repository3;
        }


        private Order GetOrderObject()
        {
            Order mem = null;
            try
            {
                mem = new Order
                {
                    OrderId = int.Parse(txtOrderId.Text),
                    MemberId = int.Parse(txtMemberId.Text),
                    OrderDate = DateTime.Parse(txtOrderDate.Text),
                    RequiredDate = DateTime.Parse(txtRequiredDate.Text),
                    ShippedDate = DateTime.Parse(txtShippedDate.Text),
                    Freight = decimal.Parse(txtFreight.Text),
                };
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Get Order");
            }
            return mem;
        }
        private OrderDetail GetOrderDetailObject()
        {
            OrderDetail mem = null;
            try
            {
                mem = new OrderDetail
                {
                    OrderId = int.Parse(txtOrderId1.Text),
                    ProductId = int.Parse(txtProductId.Text),
                    UnitPrice = decimal.Parse(txtUnitPrice.Text),
                    Quantity = int.Parse(txtQuantity.Text),
                    Discount = float.Parse(txtDiscount.Text),
                };
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Get Order Detail");
            }
            return mem;
        }


        public void LoadOrderList()
        {
            if (isAdmin)
            {
                lvOrders.ItemsSource = orderRepository.GetOrders();
                lvOrderDetails.ItemsSource = orderDetailRepository.GetOrderDetails();
            }
            else
            {
                lvOrders.ItemsSource = (System.Collections.IEnumerable)orderRepository.GetOrderListByMemberID(memid);
                lvOrderDetails.ItemsSource = (System.Collections.IEnumerable)orderDetailRepository.GetOrderDetailListByMemberID(memid);
            }
        }
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadOrderList();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Order List");
            }
        }
        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            if (isAdmin)
            {
                try
                {
                    Order mem = GetOrderObject();
                    orderRepository.InsertOrder(mem);
                    LoadOrderList();
                    MessageBox.Show($"{mem.OrderId} inserted succesfully ", "Insert Order");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Insert Order");
                }
            }
            else
            {
                MessageBox.Show("Need role Admin to continue", "Insert Order");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (isAdmin)
            {
                try
            {
                Order mem = GetOrderObject();
                orderRepository.UpdateOrder(mem);
                LoadOrderList();
                MessageBox.Show($"{mem.OrderId} updated succesfully ", "Update Order");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Order");
                }
            }
            else
            {
                MessageBox.Show("Need role Admin to continue", "Update Order");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (isAdmin)
            {
                try
                {
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure? Delete Order will delete all of its details", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes) { 
                    Order mem = GetOrderObject();
                    orderRepository.DeleteOrder(mem.OrderId);
                    LoadOrderList();
                    MessageBox.Show($"{mem.OrderId} deleted succesfully ", "Delete Order");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Delete Order");
                }
            }
            else
            {
                MessageBox.Show("Need role Admin to continue", "Delete Order");
            }
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (Login.IsWindowOpen<Window>("Products"))
            {
                Products screen = new Products(memberRepository, productRepository, orderRepository, orderDetailRepository) { 
                isAdmin=this.isAdmin,
                memid=this.memid};
                screen.Show();
                this.Close();
            }
            else if (Login.IsWindowOpen<Window>("Members"))
            {
                Members screen = new Members(memberRepository, productRepository, orderRepository, orderDetailRepository)
                { isAdmin = this.isAdmin,
                memid=this.memid
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

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            if (isAdmin) { 
                if (!Login.IsWindowOpen<Window>("Products"))
                {
                    Products screen = new Products(memberRepository, productRepository, orderRepository, orderDetailRepository)
                    {
                        isAdmin = this.isAdmin,
                        memid = this.memid
                    };
                    screen.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Product Window has been opened", "Change window");
                }
        }
            else
            {
                MessageBox.Show("Need role Admin to continue", "Access Deny");
            }
}

        private void btnMember_Click(object sender, RoutedEventArgs e)
        {
            if (!Login.IsWindowOpen<Window>("Members"))
            {
                Members screen = new Members(memberRepository, productRepository, orderRepository, orderDetailRepository)
                {
                    isAdmin = this.isAdmin,
                    memid = this.memid
                };
                screen.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Member Window has been opened, please close this window and turn back!", "Change window");
            }
        }
        private void btnInsert1_Click(object sender, RoutedEventArgs e)
        {
            if (isAdmin)
            {
                try
                {
                    OrderDetail mem = GetOrderDetailObject();
                    orderDetailRepository.InsertOrderDetail(mem);
                    LoadOrderList();
                    MessageBox.Show($"{mem.OrderId}'s Detail inserted succesfully ", "Insert Order Detail");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Insert Order Detail");
                }
            }
            else
            {
                MessageBox.Show("Need role Admin to continue", "Insert Order Detail");
            }
        }

        private void btnUpdate1_Click(object sender, RoutedEventArgs e)
        {
            if (isAdmin)
            {
                try
                {
                    OrderDetail mem = GetOrderDetailObject();
                    orderDetailRepository.UpdateOrderDetail(mem);
                    LoadOrderList();
                    MessageBox.Show($"{mem.OrderId}'s Detail updated succesfully ", "Update Order Detail");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Update Order Detail");
                }
            }
            else
            {
                MessageBox.Show("Need role Admin to continue", "Update Order Detail");
            }
        }

        private void btnDelete1_Click(object sender, RoutedEventArgs e)
        {
            if (isAdmin)
            {
                try
                {
                    List<OrderDetail> list = new List<OrderDetail>();
                    OrderDetail mem = GetOrderDetailObject();
                    var c = orderDetailRepository.GetOrderDetails();
                    foreach (var i in c)
                    {
                        if (i.OrderId == mem.OrderId)
                        {
                            list.Add(i);
                        }
                    }
                    if (list.Count <= 1)
                    {
                        MessageBox.Show("Cannot delete Order detail because there is only an orderdetail left! Please delete order first!", "Delete Order Detail");
                        return;
                    }
                    orderDetailRepository.DeleteOrderDetail(mem.OrderId,mem.ProductId);
                    LoadOrderList();
                    MessageBox.Show($"{mem.OrderId}'s Detail deleted succesfully ", "Delete Order Detail");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Delete Order Detail");
                }
            }
            else
            {
                MessageBox.Show("Need role Admin to continue", "Delete Order Detail");
            }
        }

        private void btnStatistic_Click(object sender, RoutedEventArgs e)
        {
            if (isAdmin)
            {
                lvOrders.ItemsSource = (System.Collections.IEnumerable)orderRepository.GetOrderByOrderdDate(txtStartDate.DisplayDate, txtEndDate.DisplayDate);
                lvOrderDetails.ItemsSource = (System.Collections.IEnumerable)orderDetailRepository.GetOrderDetailListByListOrder(orderRepository.GetOrderByOrderdDate(txtStartDate.DisplayDate, txtEndDate.DisplayDate));
                var total =(decimal)orderDetailRepository.GetStatistic(orderRepository.GetOrderByOrderdDate(txtStartDate.DisplayDate, txtEndDate.DisplayDate));
           
                MessageBox.Show($"The total amount of sales in the selected period is {total} currency", "Statistic Order Detail");
            }
            else
            {
                MessageBox.Show("Need role Admin to continue", "Statistic Order Detail");
            }
        }
    }
}
