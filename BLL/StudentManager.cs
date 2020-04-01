using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class StudentManager
    {
        /// <summary>
        /// 根据班级查询学员信息
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        public List<Student> GetStudentsByClass(string className)
        {
            return new StudentService().GetStudentByClass(className);
        }

        public Student GetStudentById(string studentId)
        {
            return new StudentService().GetStudentById(studentId);
        }
        public int ModifyStudent(Student objStudent)
        {
            return new StudentService().ModifyStudent(objStudent);
        }

    }
}
