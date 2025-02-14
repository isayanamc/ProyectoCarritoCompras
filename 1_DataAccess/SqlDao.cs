using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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
        private static SqlDao _instance;

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
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open(); // 🔹 5.1 Abrir la conexión con la base de datos

                    using (var command = new SqlCommand(sqlOperation.ProcedureName, conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // 🔹 5.2 Agregar los parámetros al comando SQL
                        foreach (var param in sqlOperation.Parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }

                        // 🔹 5.3 Ejecutar el procedimiento almacenado
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠ Error ejecutando el procedimiento {sqlOperation.ProcedureName}: {ex.Message}");
                throw;
            }
        }
    }

    /*
     * Clase que representa una operación SQL con stored procedures.
     */
    public class SqlOperation
    {
        public string ProcedureName { get; set; }
        public Dictionary<string, object> Parameters { get; set; }

        public SqlOperation(string procedureName)
        {
            ProcedureName = procedureName;
            Parameters = new Dictionary<string, object>();
        }

        public void AddParameter(string key, object value)
        {
            Parameters.Add(key, value);
        }
    }
}
