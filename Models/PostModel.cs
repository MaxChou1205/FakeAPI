using Dapper;
using FakeAPI.Helpers;
using Microsoft.Extensions.Configuration;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FakeAPI.Models
{
    public static class DataTableExtensions
    {
        public static List<Dictionary<String, Object>> ToList(this DataTable dataTable)
        {
            var rows = new List<Dictionary<string, object>>();
            foreach (DataRow row in dataTable.Rows)
            {
                rows.Add(row.ToDictionary());
            }
            return rows;
        }

        public static Dictionary<String, Object> ToDictionary(this DataRow dr)
        {
            var row = new Dictionary<string, object>();
            foreach (DataColumn col in dr.Table.Columns)
            {
                var o = dr[col];
                row.Add(col.ColumnName, o);
            }
            return row;
        }
    }

    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }

    public class PostModel
    {
        private static LogHelper _logger = new LogHelper(LogHelper.LogName.ModelLogger);
        private static SqlHelper _sqlAdapter = new SqlHelper();
        private static IConfiguration _manager = Startup.Configuration;

        public static List<Post> GetPosts()
        {
            try
            {
                // return _sqlAdapter.ExecuteDataTable("SELECT * FROM Post").ToList();

                using (SqlConnection conn = new SqlConnection(_manager.GetValue<string>("ConnectionStrings")))
                {
                    string strSql = "Select * from Post";
                    return conn.Query<Post>(strSql).ToList();
                }
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                throw e;
            }
        }

        /// <summary>
        /// 取得特定Post
        /// </summary>
        /// <param name="Id">Post Id</param>
        /// <returns>回傳該筆Post，若無相關Post則傳回null</returns>
        public static Post GetPostById(int Id)
        {
            try
            {
                //List<SqlParameter> parameters = new List<SqlParameter>() {
                //                     new SqlParameter("Id", Id)
                //                };
                //return _sqlAdapter.ExecuteDataTable("SELECT * FROM Post WHERE Id=@Id", parameters.ToArray()).ToList().FirstOrDefault();

                using (SqlConnection conn = new SqlConnection(_manager.GetValue<string>("ConnectionStrings")))
                {
                    string strSql = "SELECT * FROM Post WHERE Id=@Id";
                    return conn.Query<Post>(strSql, new { Id = Id }).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                throw e;
            }
        }

        public static void CreatePost(Post post)
        {
            try
            {
                post.CreatedOn = DateTime.Now;
                post.ModifiedOn = DateTime.Now;

                //var parameters = new List<SqlParameter>();
                //var columns = new List<string>();
                //Type t = post.GetType();

                //foreach (var p in t.GetProperties())
                //{
                //    string columnName = p.Name; ;
                //    columns.Add(columnName);
                //    parameters.Add(new SqlParameter(columnName, p.GetValue(post)));
                //}

                // _sqlAdapter.ExecuteNonQuery($"INSERT INTO Post ({string.Join(",", columns)}) VALUES (@{string.Join(",@", columns)})", parameters.ToArray());

                string insertStr = @"INSERT INTO Post (Id,Title,Body,CreatedOn,ModifiedOn) VALUES (@Id,@Title,@Body,@CreatedOn,@ModifiedOn)";

                using (SqlConnection conn = new SqlConnection(_manager.GetValue<string>("ConnectionStrings")))
                {
                    conn.Execute(insertStr, post);
                }
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                throw e;
            }
        }

        public static void UpdatePost(int id, Post post)
        {
            try
            {
                post.ModifiedOn = DateTime.Now;

                //var parameters = new List<SqlParameter>();
                //var columns = new List<string>();
                //Type t = post.GetType();

                //parameters.Add(new SqlParameter("Id", id));
                //foreach (var p in t.GetProperties())
                //{
                //    string columnName = p.Name;
                //    if (columnName == "Id" || columnName == "CreatedOn")
                //        continue;
                //    columns.Add($"{columnName} = @{columnName}");
                //    parameters.Add(new SqlParameter(columnName, p.GetValue(post)));
                //}
                //_sqlAdapter.ExecuteNonQuery($"UPDATE Post SET {string.Join(",", columns)} WHERE Id=@Id", parameters.ToArray());

                string updateStr = @"UPDATE Post SET Title=@Title,Body=@Body,ModifiedOn=@ModifiedOn WHERE Id=@Id";
                using (SqlConnection conn = new SqlConnection(_manager.GetValue<string>("ConnectionStrings")))
                {
                    conn.Execute(updateStr, post);
                }
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                throw e;
            }
        }

        public static void DeletePost(int id)
        {
            try
            {
                //var parameters = new List<SqlParameter>
                //{
                //    new SqlParameter("Id",id)
                //};

                //_sqlAdapter.ExecuteNonQuery($"DELETE FROM Post WHERE Id=@Id", parameters.ToArray());

                string deleteStr = "DELETE Post WHERE Id=@Id";
                using (SqlConnection conn = new SqlConnection(_manager.GetValue<string>("ConnectionStrings")))
                {
                    conn.Execute(deleteStr, new[] { new { Id = id } });
                }
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                throw e;
            }
        }

        public static void ExceptionHandler(LogHelper _loggerAPI, APIResult result, string msg)
        {
            _loggerAPI.Error(msg);
            result.IsSucceed = false;
            result.ErrorMsg = msg;
        }
    }
}
