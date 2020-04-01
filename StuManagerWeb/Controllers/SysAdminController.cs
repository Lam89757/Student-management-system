using BLL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.Security;

namespace StuManagerWeb.Controllers
{
    public class SysAdminController : Controller
    {
        //
        // GET: /SysAdmin/

        public ActionResult Index()
        {
            // return View();
            return View("AdminLogin");
        }

        [HttpPost]
        public ActionResult AdminLogin(SysAdmin objAdmin)
        {
            string adminName = new SysAdminManager().AdminLogin(objAdmin);   //调用业务逻辑
            if (adminName != null)//登录成功
            {
                FormsAuthentication.SetAuthCookie(adminName, false);

                ViewBag.Info = "欢迎您：" + adminName;//向视图传递数据
            }
            else
            {
                ViewBag.Info = "用户名或密码错误！";
            }
            return View("AdminLogin");
        }

        [Authorize]
        public ActionResult AdminExit()
        {
            FormsAuthentication.SignOut();
            Session["CurrentAdmin"] = null;
            //返回登录页面
            return View("AdminLogin");
        }

        public ActionResult GetCurrentUser()
        {
            SysAdmin objAdmin = (SysAdmin)Session["CurrentAdmin"];
            return PartialView("LoginedUser", objAdmin);
        }

    }
}
