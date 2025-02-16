using System;
using System.Collections.Generic;
using System.Data;
using DataAccess.DAOs;
using Microsoft.Data.SqlClient;

namespace DataAccess.DAOs
{
    /*
     * Clase que se encarga de la comunicación con la base de datos.
     * Solo ejecuta stored procedures.
     * 
     * Implementa el patrón SINGLETON para asegurar una única instancia del DAO.
     */
    public class SqlDao
    {
        // 🔹 1. Crear una instancia privada de la misma clase
        private static SqlDao? _instance;

        // 🔹 2. Definir la cadena de conexión a SQL Server local
        private readonly string _connectionString = "Server=localhost;Database=shopping-cart-db;User Id=sa;Password=SQLServer1234;TrustServerCertificate=True;";

        // 🔹 3. Constructor privado para evitar múltiples instancias
        private SqlDao() { }

        // 🔹 4. Método para obtener la única instancia de la clase
        public static SqlDao GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SqlDao();
            }
            return _instance;
        }

        // 🔹 5. Método para ejecutar procedimientos almacenados
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

    public List<Dictionary<string, object>> ExecuteQuery(SqlOperation sqlOperation)
    {
        var result = new List<Dictionary<string, object>>();

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
                    while (reader.Read())
                    {
                        var row = new Dictionary<string, object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[reader.GetName(i)] = reader.GetValue(i);
                        }
                        result.Add(row);
                    }
                }
            }
        }
        return result;
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
                return command.ExecuteNonQuery(); // Retorna el número de filas afectadas
            }
        }
    }



}
}