using System;
using System.Collections.Generic;
using System.Data;
using DataAccess.DAOs;
using Microsoft.Data.SqlClient;

namespace DataAccess.DAOs
{
    public class SqlDao
    {
        private static SqlDao? _instance;
        private readonly string _connectionString = "Server=localhost;Database=shopping-cart-db;User Id=sa;Password=SQLServer1234;TrustServerCertificate=True;";
        private SqlDao() { }
        public static SqlDao GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SqlDao();
            }
            return _instance;
        }

        public void ExecuteProcedure(SqlOperation sqlOperation)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(sqlOperation.ProcedureName, conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    foreach (var param in sqlOperation.Parameters)
                    {
                        command.Parameters.Add(param);
                    }
                    conn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Dictionary<string, object>> ExecuteQueryProcedure(SqlOperation sqlOperation)
        {
            var lstResults = new List<Dictionary<string, object>>();
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(sqlOperation.ProcedureName, conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    foreach (var param in sqlOperation.Parameters)
                    {
                        command.Parameters.Add(param);
                    }
                    conn.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var rowDict = new Dictionary<string, object>();
                                for (var index = 0; index < reader.FieldCount; index++)
                                {
                                    var key = reader.GetName(index);
                                    var value = reader.GetValue(index);
                                    rowDict[key] = value;
                                }
                                lstResults.Add(rowDict);
                            }
                        }
                    }
                }
            }
            return lstResults;
        }

        public int ExecuteProcedureWithResult(SqlOperation sqlOperation)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(sqlOperation.ProcedureName, conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    foreach (var param in sqlOperation.Parameters)
                    {
                        command.Parameters.Add(param);
                    }
                    conn.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }
    }
}
