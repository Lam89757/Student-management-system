using BLL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StuManagerWeb.Controllers
{
    public class StudentController : Controller
    {
        //
        // GET: /Student/

        //public ActionResult Index()
        //{
        //    if (this.User.Identity.IsAuthenticated)
        //    {
        //        string adminName = this.User.Identity.Name;  //获取写入的adminName

        //        ViewBag.adminName = adminName;
        //        return View("StudentManage");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "SysAdmin");
        //    }           
        //}
        [Authorize]
        public ActionResult Index()
        {
            string adminName = this.User.Identity.Name;  //获取写入的adminName

            ViewBag.adminName = adminName;
            return View("StudentManage");

        }



        /// <summary>
        /// 根据班级查询学员
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [HandleError(ExceptionType = typeof(System.Exception), View = "Error")]
        public ActionResult GetStuList(string className)
        {
            //调用模型处理业务
            List<Student> stuList = new StudentManager().GetStudentsByClass(className);
      
            //保存数据
            ViewBag.className = className;//为后续继续使用
            ViewBag.stuList = stuList;
            //返回视图
            return View("StudentManage");
        }

        //[HttpGet ]
        //public ActionResult GetStuList()
        //{
        //    ViewBag.stuList = new StudentManager().GetStudentsByClass("");  
        //    return View("StudentManage");
        //}

        //[NonAction]
        // public ActionResult GetStuList()
        // {
        //     ViewBag.stuList = new StudentManager().GetStudentsByClass("");
        //     return View("StudentManage");
        // }

        [ActionName("GetAllStuList")]
        public ActionResult GetStuList()
        {
            ViewBag.stuList = new StudentManager().GetStudentsByClass("");
            return View("StudentManage");
        }

        public ActionResult GetStuDetail(string stuId)
        {
            Student objStudent = new StudentManager().GetStudentById(stuId);
            //调用带Model的重载视图方法
            return View("StudentDetail", objStudent);
        }

        public ActionResult GetEditInfo(string stuId)    //获取要修改的学员对象
        {
            Student objStudent = new StudentManager().GetStudentById(stuId);
            return View("StudentEidt", objStudent);
        }
        public ActionResult Edit(Student objStudent)//修改学员对象
        {
            int result = new StudentManager().ModifyStudent(objStudent);
            //返回
            return Content("<script>alert('修改成功');document.location='" +
                Url.Action("Index", "Student") + "';</script>");
        }
        public ActionResult GotAdd()
        {
            return View("StudentAdd");
        }
        public ActionResult Add()
        {
            //省略...
            return null;
        }
    }
}
