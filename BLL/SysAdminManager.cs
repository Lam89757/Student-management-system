using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DAL;
using Models;
using System.Web;

namespace BLL
{
    public class SysAdminManager
    {
        private SysAdminService objAdminService = new SysAdminService();

        /// <summary>
        /// 根据用户账号和密码登录
        /// </summary>
        /// <param name="objAdmin"></param>
        /// <returns></returns>
        public string AdminLogin(SysAdmin objAdmin)
        {
            objAdmin = objAdminService.AdminLogin(objAdmin);
            if (objAdmin != null)
            {
                HttpContext.Current.Session["CurrentAdmin"] = objAdmin;
                return objAdmin.AdminName;
            }
            else
            {
                return null;
            }
        }
    }
}
