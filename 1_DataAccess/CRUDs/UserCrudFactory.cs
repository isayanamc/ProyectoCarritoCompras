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
            var user = dto as User;
            var sqlOperation = new SqlOperation() { ProcedureName = "CRE_USER_PR" };

            sqlOperation.AddStringParameter("P_USER_CODE", user.UserCode);
            sqlOperation.AddStringParameter("P_NAME", user.Name);
            sqlOperation.AddStringParameter("P_LAST_NAME", user.LastName);
            sqlOperation.AddStringParameter("P_EMAIL", user.Email);
            sqlOperation.AddStringParameter("P_PHONE", user.PhoneNumber);
            sqlOperation.AddDateTimeParam("P_BIRTH_DATE", user.BirthDate);
            sqlOperation.AddStringParameter("P_PASSWORD", user.Password);

            _sqlDao.ExecuteProcedure(sqlOperation);
        }

        public override T Retrieve<T>(BaseDTO dto)
        {
            throw new NotImplementedException();
        }

        public override void Delete(BaseDTO dto)
        {
            var user = dto as User;
            var sqlOperation = new SqlOperation() { ProcedureName = "DEL_USER_PR" };

            sqlOperation.AddStringParameter("P_USER_CODE", user.UserCode);

            _sqlDao.ExecuteProcedure(sqlOperation);
        }

        public override void Update(BaseDTO dto)
        {
            var user = dto as User;

            if (user == null)
            {
                Console.WriteLine("❌ Error: El usuario es nulo.");
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



        public override T RetrieveById<T>(int id)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            throw new NotImplementedException();
        }
    }
}
