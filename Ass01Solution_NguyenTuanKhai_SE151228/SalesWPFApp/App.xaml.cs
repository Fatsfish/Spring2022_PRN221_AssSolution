using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DataAccess.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;
        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }
        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton(typeof(IMemberRepository), typeof(MemberRepository));
            services.AddSingleton<Members>();

            services.AddSingleton(typeof(IProductRepository), typeof(ProductRepository));
            services.AddSingleton<Products>();

            services.AddSingleton(typeof(IOrderRepository), typeof(OrderRepository));
            services.AddSingleton(typeof(IOrderDetailRepository), typeof(OrderDetailRepository));
            services.AddSingleton<Orders>();


            services.AddSingleton<Login>();
        }
        private void OnStartup (object sender, StartupEventArgs e)
        {
            var Members = serviceProvider.GetService<Members>();
            var Products = serviceProvider.GetService<Products>();
            var Orders = serviceProvider.GetService<Orders>();
            var Login = serviceProvider.GetService<Login>();
            Login.Show();
        }
    }
}
