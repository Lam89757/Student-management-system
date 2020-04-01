using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using System.Data;
using System.Data.SqlClient;


namespace DAL
{
    public class StudentService
    {

        #region  添加学员对象

        /// <summary>
        /// 添加学员对象
        /// </summary>
        /// <param name="objStudent"></param>
        /// <returns></returns>
        public int AddStudent(Student objStudent)
        {
            //【1】编写SQL语句           
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("insert into Students(studentName,Gender,Birthday,");
            sqlBuilder.Append("StudentIdNo,PhoneNumber,StudentAddress,CardNo,ClassId)");
            sqlBuilder.Append(" values('{0}','{1}','{2}',{3},'{4}','{5}','{6}',{7})");

            //【2】解析对象
            string sql = string.Format(sqlBuilder.ToString(), objStudent.StudentName,
                     objStudent.Gender, objStudent.Birthday,
                    objStudent.StudentIdNo, objStudent.PhoneNumber, objStudent.StudentAddress, objStudent.CardNo,
                    objStudent.ClassId);
            try
            {
                return Convert.ToInt32(SQLHelper.Update(sql)); //【3】执行SQL语句，返回结果
            }
            catch (SqlException ex)
            {
                throw new Exception("数据库操作出现异常！具体信息：\r\n" + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 查询学员信息

        /// <summary>
        /// 根据班级查询学员信息
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        public List<Student> GetStudentByClass(string className)
        {
            string sql = "select StudentId,StudentName,Gender,Birthday,ClassName from Students";
            sql += " inner join StudentClass on Students.ClassId=StudentClass.ClassId ";
            sql += " where ClassName like '%{0}%'";
            sql = string.Format(sql, className);
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<Student> list = new List<Student>();
            while (objReader.Read())
            {
                list.Add(new Student()
                {
                    StudentId = Convert.ToInt32(objReader["StudentId"]),
                    StudentName = objReader["StudentName"].ToString(),
                    Gender = objReader["Gender"].ToString(),
                    Birthday = Convert.ToDateTime(objReader["Birthday"])
                });
            }
            objReader.Close();
            return list;
        }
        /// <summary>
        /// 根据学生编号查询学生信息
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public Student GetStudentById(string studentId)
        {
            string sql = "select StudentId,StudentName,Gender,Birthday,";
            sql += "StudentIdNo,PhoneNumber,StudentAddress,CardNo from Students";
            sql += " inner join StudentClass on Students.ClassId=StudentClass.ClassId ";
            sql += " where StudentId=" + studentId;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            Student objStudent = null;
            if (objReader.Read())
            {
                objStudent = new Student()
                {
                    StudentId = Convert.ToInt32(objReader["StudentId"]),
                    StudentName = objReader["StudentName"].ToString(),
                    Gender = objReader["Gender"].ToString(),
                    Birthday = Convert.ToDateTime(objReader["Birthday"]),
                    CardNo = objReader["CardNo"].ToString(),
                    StudentIdNo = objReader["StudentIdNo"].ToString(),
                    PhoneNumber = objReader["PhoneNumber"].ToString(),
                    StudentAddress = objReader["StudentAddress"].ToString()
                };
            }
            objReader.Close();
            return objStudent;
        }

        #endregion

        #region 修改学员信息

        /// <summary>
        /// 修改学员对象
        /// </summary>
        /// <param name="objStudent"></param>
        /// <returns></returns>
        public int ModifyStudent(Student objStudent)
        {   //【1】编写SQL语句
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("update Students set studentName='{0}',Gender='{1}',Birthday='{2}',");
            sqlBuilder.Append("StudentIdNo={3},PhoneNumber='{4}',StudentAddress='{5}'");
            sqlBuilder.Append(" where StudentId={6}");
            //【2】解析对象
            string sql = string.Format(sqlBuilder.ToString(), objStudent.StudentName,
                     objStudent.Gender, objStudent.Birthday,
                    objStudent.StudentIdNo,
                    objStudent.PhoneNumber, objStudent.StudentAddress, objStudent.StudentId);
            try
            {
                return Convert.ToInt32(SQLHelper.Update(sql));    //【3】执行SQL语句，返回结果
            }
            catch (SqlException ex)
            {
                throw new Exception("数据库操作出现异常！具体信息：\r\n" + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 删除学员对象

        public int DeleteStudentById(string studentId)
        {
            string sql = "delete from Students where StudentId=" + studentId;
            try
            {
                return SQLHelper.Update(sql);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                    throw new Exception("该学号被其他实体引用，不能直接删除该学员对象！");
                else
                    throw new Exception("数据库操作出现异常！具体信息：\r\n" + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///// <summary>
        ///// 删除学员对象
        ///// </summary>
        ///// <param name="objStudent"></param>
        ///// <returns></returns>
        //public int DeleteStudent(Student objStudent)
        //{
        //    string sql = "delete from Students where StudentId=" + objStudent.StudentId;
        //    try
        //    {
        //        return SQLHelper.Update(sql);
        //    }
        //    catch (SqlException ex)
        //    {
        //        if (ex.Number == 547)
        //            throw new Exception("该学号被其他实体引用，不能直接删除该学员对象！");
        //        else
        //            throw new Exception("数据库操作出现异常！具体信息：\r\n" + ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        #endregion

    }
}
