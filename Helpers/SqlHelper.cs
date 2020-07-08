using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace FakeAPI.Helpers
{
    /// <summary>
    /// SqlHelper 
    /// [SKIP]
    /// </summary>
    public class SqlHelper
    {
        private static IConfiguration _manager = Startup.Configuration;
        private static LogHelper _logger = new LogHelper(LogHelper.LogName.DBLogger);
        protected string connection;

        public SqlHelper()
        {
            connection = _manager.GetValue<string>("ConnectionStrings");
        }

        public SqlHelper(string _connection)
        {
            connection = _connection;
        }

        /// <summary>
        /// 將錯誤訊息紀錄下來
        /// </summary>
        /// <param name="command">錯誤的 SQL</param>
        /// <param name="param">錯誤的參數</param>
        private void LogException(Exception ex, string command, SqlParameter[] param)
        {
            // 如果是寫錯誤的 Log 就不紀錄，避免發生 Endless Recursion
            if (command.IndexOf("INSERT INTO Log") > -1) return;
            if (param != null)
            {
                List<string> s = new List<string>();
                foreach (SqlParameter p in param)
                {
                    s.Add("(" + p.ParameterName + "," + p.Value.ToString() + ")");
                }
                _logger.Error("SqlException occurred: " + command + " parameter:" + string.Join(",", s.ToArray()) + " exception:" + ex.ToString());
            }
            else
            {
                _logger.Error("SqlException occurred: " + command + " with no parameter. Exception:" + ex.ToString());
            }
        }

        public int ExecuteNonQuery(CommandType type, string command, SqlParameter[] param)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = command;
                    cmd.CommandType = type;
                    cmd.CommandTimeout = 600;
                    if (param != null)
                    {
                        foreach (SqlParameter p in param)
                        {
                            if (p.Value == null)
                            {
                                p.Value = DBNull.Value;
                            }
                            cmd.Parameters.Add(p);
                        }
                    }
                    conn.Open();
                    try
                    {
                        int ret = cmd.ExecuteNonQuery();
                        return ret;
                    }
                    catch (Exception ex)
                    {
                        LogException(ex, command, param);
                        throw;
                    }
                    finally
                    {
                        cmd.Parameters.Clear();
                    }
                }
            }
        }

        public int ExecuteNonQuery(CommandType type, string command)
        {
            return ExecuteNonQuery(type, command, null);
        }

        public int ExecuteNonQuery(string command, SqlParameter[] param=null)
        {
            return ExecuteNonQuery(CommandType.Text, command, param);
        }

        public DataTable ExecuteDataTable(CommandType type, string query, SqlParameter[] param)
        {
            DataTable oDataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandTimeout = 600;
                    if (param != null)
                    {
                        foreach (SqlParameter p in param)
                        {
                            if (p.Value == null)
                            {
                                p.Value = DBNull.Value;
                            }
                            cmd.Parameters.Add(p);
                        }
                    }
                    try
                    {
                        conn.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                            oDataTable.Load(dr);
                    }
                    catch (Exception ex)
                    {
                        LogException(ex, query, param);
                    }
                    finally
                    {
                        cmd.Parameters.Clear();
                    }
                    return oDataTable;
                }
            }
        }

        public DataTable ExecuteDataTable(CommandType type, string query)
        {
            return ExecuteDataTable(type, query, null);
        }

        public DataTable ExecuteDataTable(string query, SqlParameter[] param = null)
        {
            return ExecuteDataTable(CommandType.Text, query, param);
        }
    }
}