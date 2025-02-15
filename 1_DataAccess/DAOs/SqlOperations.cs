using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.DAOs
{
    /*
     * Clase que representa una operación SQL
     * Define el nombre del procedimiento almacenado y los parámetros que usará
     * Clase con instrucciones de lo que tiene que hacer el SqlDao
     */
    public class SqlOperation
    {
        public string ProcedureName { get; set; }
        public List<SqlParameter> Parameters { get; set; }

       
        //private readonly string _connectionString = "Server=localhost;Database=shopping-cart-db;User Id=sa;Password=SQLServer1234;TrustServerCertificate=True;";

        // Constructor sin parámetros que inicializa la lista de parámetros
        public SqlOperation()
        {
            Parameters = new List<SqlParameter>();
        }

        // Método para agregar un parámetro de tipo string
        public void AddStringParameter(string paramName, string paramValue)
        {
            Parameters.Add(new SqlParameter(paramName, paramValue));
        }

        // Método para agregar un parámetro de tipo int
        public void AddIntParameter(string paramName, int paramValue)
        {
            Parameters.Add(new SqlParameter(paramName, paramValue));
        }

        // Método para agregar un parámetro de tipo double
        public void AddDoubleParam(string paramName, double paramValue)
        {
            Parameters.Add(new SqlParameter(paramName, paramValue));
        }

        // Método para agregar un parámetro de tipo DateTime
        public void AddDateTimeParam(string paramName, DateTime paramValue)
        {
            Parameters.Add(new SqlParameter(paramName, paramValue));
        }

    }
}
