using DTO;
using System;
using DataAccess.DAOs;

namespace DataAccess.CRUDs
{
    public class UserCrudFactory : CrudFactory
    {

        private readonly SqlDao _sqlDao;

        public UserCrudFactory()
        {
            _sqlDao = SqlDao.GetInstance();
        }

        public override void Create(BaseDTO dto)
        {
            // Convertir el DTO genérico a un objeto de tipo User
            var user = dto as User;

            // Crear una instancia de SqlOperation y definir el nombre del procedimiento almacenado
            var sqlOperation = new SqlOperation() { ProcedureName = "CRE_USER_PR" };

            // Agregamos los parámetros con los nombres definidos en el procedimiento almacenado
            sqlOperation.AddStringParameter("P_USER_CODE", user.UserCode);
            sqlOperation.AddStringParameter("P_NAME", user.Name);
            sqlOperation.AddStringParameter("P_LAST_NAME", user.LastName);
            sqlOperation.AddStringParameter("P_EMAIL", user.Email);
            sqlOperation.AddIntParameter("P_PHONE", user.PhoneNumber);
            sqlOperation.AddDateTimeParam("P_BIRTH_DATE", user.BirthDate);
            sqlOperation.AddStringParameter("P_PASSWORD", user.Password);

            // Ir al DAO y ejecutar
            _sqlDao.ExecuteProcedure(sqlOperation);
        }

        public override T Retrieve<T>(BaseDTO dto)
        {
            throw new NotImplementedException();
        }

        public override void Delete(BaseDTO dto)
        {
            throw new NotImplementedException();
        }

        public override T RetrieveById<T>(int id)
        {
            throw new NotImplementedException();
        }

        public override void Update(BaseDTO dto)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            throw new NotImplementedException();
        }


    }
}
