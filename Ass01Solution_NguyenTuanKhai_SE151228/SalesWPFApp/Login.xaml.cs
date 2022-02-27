using System;
using System.Collections.Generic;
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
using DataAccess.Repository;
using Nancy.Json;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        IMemberRepository memberRepository;
        IProductRepository productRepository;
        IOrderRepository orderRepository;
        IOrderDetailRepository orderDetailRepository;
        public Login(IMemberRepository repository, IProductRepository repository1, IOrderRepository repository2, IOrderDetailRepository repository3)
        {
            InitializeComponent();
            memberRepository = repository;
            productRepository = repository1;
            orderRepository = repository2;
            orderDetailRepository = repository3;
        }
        public static bool IsWindowOpen<T>(string name) where T : Window
        {
            return string.IsNullOrEmpty(name)
               ? Application.Current.Windows.OfType<T>().Any()
               : Application.Current.Windows.OfType<T>().Any(w => w.Name.Equals(name));
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string json = string.Empty;

            // read json string from file
            using (StreamReader reader = new StreamReader("appsettings.json"))
            {
                json = reader.ReadToEnd();
            }

            JavaScriptSerializer jss = new JavaScriptSerializer();

            // convert json string to dynamic type
            var obj = jss.Deserialize<dynamic>(json);

            // get contents
            string Email = obj["DefaultAccount"]["Email"];
            string Password = obj["DefaultAccount"]["password"];
            bool isMem = false;

            if (Email.Equals(txtEmail.Text) && Password.Equals(txtPassword.Password))
            {
                Members frm = new Members(memberRepository, productRepository, orderRepository, orderDetailRepository)
                {
                    isAdmin = true
                };
                frm.Show();
                isMem = true;
                this.Close();

            }

            // add employees to richtextbox

            var members = memberRepository.GetMembers();

            foreach (var i in members)
            {
                if (i.Email.Equals(txtEmail.Text) && i.Password.Equals(txtPassword.Password))
                {
                    Members frm = new Members(memberRepository, productRepository, orderRepository, orderDetailRepository)
                    {
                        isAdmin = false,
                        memid=i.MemberId
                    };
                    frm.Show();
                    isMem = true;

                    this.Close();
                    break;

                }

            }
            if (isMem == true)
            {
                MessageBox.Show("Login Success", "Right user");
            }
            else
            {
                MessageBox.Show("Wrong user name or password, please try again", "Wrong user");

            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
