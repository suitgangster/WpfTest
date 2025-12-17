using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WpfTest.DataAccess.DataEntity;

namespace WpfTest.DataAccess
{
    public class LocalDataAccess
    {
        private static LocalDataAccess instance;
        private LocalDataAccess() { }

        public static LocalDataAccess GetInstance()
        {
            return instance ?? (instance = new LocalDataAccess());
        }

        SqlConnection conn;
        SqlDataAdapter adapter;

        private void Dispose()
        {
            if (conn != null) conn.Dispose();
            if (adapter != null) adapter.Dispose();
        }

        private bool DBConnection()
        {
            string connstr = App.Configuration.GetConnectionString("DefaultConnection");
            if (conn == null)
                conn = new SqlConnection(connstr);
            try
            {
                conn.Open();
                return true;
            }
            catch (Exception e)
            {
                string str = e.Message;
                return false;
            }
        }

        public UserEntity CheckUserEntity(string userName,string pwd)
        {
            string sqlstr = "select * from users where user_name=@user_name and password=@pwd and is_validation=1";
            try
            {
                if (DBConnection())
                {
                    adapter = new SqlDataAdapter(sqlstr, conn);

                    //var para = new SqlParameter("@user_name", SqlDbType.VarChar);
                    //para.Value = userName;
                    //adapter.SelectCommand.Parameters.Add(para);

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@user_name", SqlDbType.VarChar) { Value = userName });
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@pwd", SqlDbType.VarChar) { Value = pwd });

                    DataTable dt = new DataTable();
                    int count = adapter.Fill(dt);
                    if (count <= 0)
                        throw new Exception("用户名密码不正确");
                    DataRow dataRow = dt.Rows[0];
                    if (dataRow.Field<Int32>("is_can_login") == 0)
                        throw new Exception("当前用户没有权限");

                    UserEntity userInfo = new UserEntity();
                    userInfo.UserName = dataRow.Field<string>("user_name");
                    userInfo.RealName = dataRow.Field<string>("real_name");
                    userInfo.Password = dataRow.Field<string>("password");
                    userInfo.Avatar = dataRow.Field<string>("avatar");
                    userInfo.Gender = dataRow.Field<Int32>("gender");
                    return userInfo;
                }
                else
                {
                    throw new Exception("连接失败");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                Dispose();
            }
        }
    }
}
