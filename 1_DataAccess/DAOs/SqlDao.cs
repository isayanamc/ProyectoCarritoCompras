using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccess.DAOs;

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

}
}