using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class StudentClassManager
    {
        public List<StudentClass> GetAllClasses()
        {
            return new DAL.StudentClassService().GetAllClasses();
        }
    }
}
