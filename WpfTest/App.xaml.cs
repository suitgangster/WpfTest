using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Numerics;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using WpfTest.ViewModels;
using WpfTest.Views;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IConfiguration Configuration { get; private set; }

        public static new App Currenr => (App)Application.Current;
        public IServiceProvider ServiceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            #region IOC控制反转
            // 1.新建IOC容器
            ServiceCollection service = new ServiceCollection();
            // 2.注册
            service.AddSingleton<ILogger>(
                new LoggerConfiguration()
                    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                    .CreateLogger()
            );
            service.AddSingleton<LoginViewModel>();
            //service.AddSingleton<LoginView>();
            service.AddSingleton<LoginView>(sp => new LoginView
            {
                DataContext = sp.GetService<LoginViewModel>(),
            });
            service.AddSingleton<MainWindow>();
            // 3.构建服务提供者
            ServiceProvider = service.BuildServiceProvider();
            #endregion


            // 构建配置并加载 appsettings.json
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            base.OnStartup(e);

            // 把loginVIew作为启动页
            this.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            if (new LoginView().ShowDialog() == true)
            {  
                new MainWindow().ShowDialog();
            }
            //this.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            //if (ServiceProvider.GetService<LoginView>().ShowDialog() == true)
            //{
            //    ServiceProvider.GetService<MainWindow>().ShowDialog();
            //}

            Application.Current.Shutdown();
        }
    }
}
