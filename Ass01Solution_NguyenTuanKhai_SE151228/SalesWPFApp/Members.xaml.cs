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
using BusinessObject;
using DataAccess.Repository;
namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for Member1.xaml
    /// </summary>
    public partial class Members : Window
    {
        IMemberRepository memberRepository;
        IProductRepository productRepository;
        IOrderRepository orderRepository;
        IOrderDetailRepository orderDetailRepository;
        public bool isAdmin;
        public int memid;
        public Members(IMemberRepository repository)
        {
            InitializeComponent();
            memberRepository = repository;
        }
        public Members(IMemberRepository repository, IProductRepository repository1, IOrderRepository repository2, IOrderDetailRepository repository3)
        {
            InitializeComponent();
            memberRepository = repository;
            productRepository = repository1;
            orderRepository = repository2;
            orderDetailRepository = repository3;
        }
        

        private Member GetMemberObject()
        {
            Member mem = null;
            try
            {
                mem = new Member
                {
                    MemberId = int.Parse(txtMemberId.Text),
                    Email = txtEmail.Text,
                    CompanyName = txtCompanyName.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text,
                    Password = txtPassword.Text
                };
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Get Member");
            }
            return mem;
        }


        public void LoadMemberList()
        {
            if (isAdmin)
            {
                lvMembers.ItemsSource = memberRepository.GetMembers();
            }
            else
            {
                lvMembers.ItemsSource = (System.Collections.IEnumerable)memberRepository.GetMemberByIDList(memid);
            }
        }
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadMemberList();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Member List");
            }
        }
        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            if (isAdmin) { 
            try
            {
                Member mem = GetMemberObject();
                memberRepository.InsertMember(mem);
                LoadMemberList();
                MessageBox.Show($"{mem.Email} inserted succesfully ", "Insert member");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Insert Member");
            }
            }
            else
            {
                MessageBox.Show( "Need role Admin to continue", "Insert member");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Member mem = GetMemberObject();
                memberRepository.UpdateMember(mem);
                LoadMemberList();
                MessageBox.Show($"{mem.Email} updated succesfully ", "Update member");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Member");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (isAdmin)
            {
                try
                {
                    Member mem = GetMemberObject();
                    var c = orderRepository.GetOrders();
                    foreach (var i in c)
                    {
                        if (i.MemberId == mem.MemberId)
                        {
                            MessageBox.Show("Cannot delete Member because there is/are order of this member left! Please delete order first!", "Delete Member");
                            return;
                        }
                    }
                    memberRepository.DeleteMember(mem.MemberId);
                    LoadMemberList();
                    MessageBox.Show($"{mem.Email} deleted succesfully ", "Delete member");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Delete Member");
                }
            }
            else
            {
                MessageBox.Show("Need role Admin to continue", "Delete member");
            }
        }
        private void btnClose_Click(object sender, RoutedEventArgs e) {
            if (Login.IsWindowOpen<Window>("Products"))
            {
                Products screen = new Products(memberRepository, productRepository, orderRepository,orderDetailRepository) { 
                isAdmin=this.isAdmin,
                memid=this.memid
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

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            if (isAdmin)
            {
                Products screen = new Products(memberRepository, productRepository, orderRepository, orderDetailRepository)
                {
                    isAdmin = this.isAdmin,
                    memid = this.memid
                };
                screen.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Need role Admin to continue", "Access Deny");
            }
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
    }
}
