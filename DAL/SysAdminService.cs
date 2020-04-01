using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class SysAdminService
    {
        /// <summary>
        /// 根据用户账号和密码登录
        /// </summary>
        /// <param name="objAdmin"></param>
        /// <returns></returns>
        public SysAdmin AdminLogin(SysAdmin objAdmin)
        {
            string sql = "select LoginId,LoginPwd,AdminName from Admins where loginId={0} and loginPwd='{1}'";
            sql = string.Format(sql, objAdmin.LoginId, objAdmin.LoginPwd);
            try
            {
                SqlDataReader objReader = SQLHelper.GetReader(sql);
                if (objReader.Read())
                {
                    objAdmin.AdminName = objReader["AdminName"].ToString();
                    objReader.Close();
                }
                else
                {
                    objAdmin = null;
                }
            }
            catch (SqlException)
            {
                throw new Exception("应用程序和数据库连接出现问题！");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objAdmin;
        }       
    }
}
