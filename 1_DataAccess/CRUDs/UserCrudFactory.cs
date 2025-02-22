using DTO;
using System;
using System.Collections.Generic;
using DataAccess.DAOs;

namespace DataAccess.CRUDs
{
    public class UserCrudFactory : CrudFactory
    {
        private static readonly SqlDao _sqlDao = SqlDao.GetInstance();

        public override void Create(BaseDTO dto)
        {
            if (dto is not User user)
            {
                Console.WriteLine("❌ Error: El DTO proporcionado no es un usuario.");
                return;
            }

            var sqlOperation = new SqlOperation() { ProcedureName = "CRE_USER_PR" };

            sqlOperation.AddStringParameter("P_USER_CODE", user.UserCode);
            sqlOperation.AddStringParameter("P_NAME", user.Name);
            sqlOperation.AddStringParameter("P_LAST_NAME", user.LastName);
            sqlOperation.AddStringParameter("P_EMAIL", user.Email);
            sqlOperation.AddStringParameter("P_PHONE", user.PhoneNumber);
            sqlOperation.AddDateTimeParam("P_BIRTH_DATE", user.BirthDate);
            sqlOperation.AddStringParameter("P_PASSWORD", user.Password);

            _sqlDao.ExecuteProcedure(sqlOperation);
            Console.WriteLine("✅ Usuario creado exitosamente.");
        }

        public override void Delete(BaseDTO dto)
        {
            if (dto is not User user)
            {
                Console.WriteLine("❌ Error: El DTO proporcionado no es un usuario.");
                return;
            }

            var sqlOperation = new SqlOperation() { ProcedureName = "DEL_USER_PR" };
            sqlOperation.AddStringParameter("P_USER_CODE", user.UserCode);

            _sqlDao.ExecuteProcedure(sqlOperation);
            Console.WriteLine("✅ Usuario eliminado exitosamente.");
        }

        public override void Update(BaseDTO dto)
        {
            if (dto is not User user)
            {
                Console.WriteLine("❌ Error: El DTO proporcionado no es un usuario.");
                return;
            }

            if (user.Id <= 0)
            {
                Console.WriteLine("❌ Error: El ID del usuario es inválido.");
                return;
            }

            var sqlOperation = new SqlOperation() { ProcedureName = "UPD_USER_PR" };

            sqlOperation.AddIntParameter("P_ID", user.Id);
            sqlOperation.AddStringParameter("P_USER_CODE", user.UserCode);
            sqlOperation.AddStringParameter("P_NAME", user.Name);
            sqlOperation.AddStringParameter("P_LAST_NAME", user.LastName);
            sqlOperation.AddStringParameter("P_EMAIL", user.Email);
            sqlOperation.AddStringParameter("P_PHONE", user.PhoneNumber);
            sqlOperation.AddDateTimeParam("P_BIRTH_DATE", user.BirthDate);
            sqlOperation.AddStringParameter("P_PASSWORD", user.Password);

            _sqlDao.ExecuteProcedure(sqlOperation);
            Console.WriteLine("✅ Usuario actualizado exitosamente.");
        }

        public override T Retrieve<T>(BaseDTO dto)
        {
            if (dto is not User user)
            {
                Console.WriteLine("❌ Error: El DTO proporcionado no es un usuario.");
                return default;
            }

            var sqlOperation = new SqlOperation() { ProcedureName = "GET_USER_BY_CODE_PR" };
            sqlOperation.AddStringParameter("P_USER_CODE", user.UserCode);

            var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                return (T)Convert.ChangeType(BuildUser(lstResults[0]), typeof(T));
            }

            return default;
        }

        public override T RetrieveById<T>(int id)
        {
            var sqlOperation = new SqlOperation() { ProcedureName = "RET_USER_BY_ID_PR" };
            sqlOperation.AddIntParameter("P_Id", id);

            var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                return (T)Convert.ChangeType(BuildUser(lstResults[0]), typeof(T));
            }

            return default;
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstUsers = new List<T>();
            var sqlOperation = new SqlOperation() { ProcedureName = "RET_ALL_USERS_PR" };

            var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);

            foreach (var row in lstResults)
            {
                lstUsers.Add((T)Convert.ChangeType(BuildUser(row), typeof(T)));
            }

            return lstUsers;
        }

        private User BuildUser(Dictionary<string, object> row)
        {
            return new User
            {
                Id = Convert.ToInt32(row["Id"]),
                Name = row["Name"]?.ToString() ?? "Desconocido",
                LastName = row["LastName"]?.ToString() ?? "Desconocido",
                UserCode = row["UserCode"]?.ToString() ?? "N/A",
                Email = row["Email"]?.ToString() ?? "N/A",
                PhoneNumber = row["PhoneNumber"]?.ToString() ?? "00000000",
                BirthDate = row["BirthDate"] is DBNull ? DateTime.MinValue : Convert.ToDateTime(row["BirthDate"]),
                Password = row["Password"]?.ToString() ?? "********",
                Created = row["Created"] is DBNull ? DateTime.MinValue : Convert.ToDateTime(row["Created"])
            };
        }
    }
}
