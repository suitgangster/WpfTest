using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTest.Common;
using WpfTest.DataAccess.DataEntity;
using WpfTest.DataAccess;
using System.Windows;
using Serilog;
using System.Diagnostics;

namespace WpfTest.ViewModels
{
    public class LoginViewModel : NotifyBase
    {
        // 日志级别默认是Information,debug不会写入文件,需要手动设置MinimumLevel.Debug()
        // 通过IOC注入
        //    ILogger logger = new LoggerConfiguration()
        //.WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
        //.CreateLogger();

        private ILogger _logger;

        private string _account;
        public string Account
        {
            get { return _account; }
            set { _account = value; DoNotify(); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; DoNotify(); }
        }

        public CommandBase LoginCommand { get; set; }

        public LoginViewModel(ILogger logger)
        {
            Debug.Write("LoginViewModel已创建");

            LoginCommand = new CommandBase(DoLogin);

            //logger.Debug("LoginViewModel已创建");
            _logger = logger;
            _logger.Information("LoginViewModel已创建hahahaha啦啦啦");
        }

        private void DoLogin(object o)
        {
            LocalDataAccess localDataAccess = LocalDataAccess.GetInstance();

            Task.Run(async () =>
            {
                await Task.Delay(1000);
                try
                {
                    UserEntity userEntity = localDataAccess.CheckUserEntity(Account, Password);
                    if (userEntity == null)
                        throw new Exception("账号密码不正确");

                    _logger.Information("登录成功");
                    Application.Current.Dispatcher.Invoke(() => { (o as Window).DialogResult = true; });
                }
                catch (Exception e)
                {
                    throw e;
                }
            });
        }
    }
}
