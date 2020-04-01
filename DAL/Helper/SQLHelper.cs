using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DAL
{
    /// <summary>
    ///  通用数据访问类
    /// </summary>
    class SQLHelper
    {
        public static readonly string connString =
           ConfigurationManager.ConnectionStrings["connString"].ToString();
        /// <summary>
        /// 执行增、删、改（insert/update/delete）
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int Update(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 执行单一结果查询（select）
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object GetSingleResult(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                object result = cmd.ExecuteScalar();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 执行多结果查询（select）
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static SqlDataReader GetReader(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                SqlDataReader objReader =
                    cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return objReader;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }
        /// <summary>
        /// 执行返回数据集的查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd); //创建数据适配器对象
            DataSet ds = new DataSet();//创建一个内存数据集
            try
            {
                conn.Open();
                da.Fill(ds);  //使用数据适配器填充数据集
                return ds;  //返回数据集
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }      
        /// <summary>
        /// 获取服务器的时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetServerTime()
        {
            return Convert.ToDateTime(GetSingleResult("select getdate()"));
        }
    }
}

