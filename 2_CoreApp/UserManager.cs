using System;
using DTO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.CRUDs;


namespace CoreApp
{
    // Clase gestora del DTO de usuario, aplica validaciones y conocimiento de negocio.
    // Los managers siguen la misma línea del patrón del CRUD.
    public class UserManager : BaseManager
    {
        private readonly UserCrudFactory uCrud = new UserCrudFactory();

        public void Create(User user)
        {
            // Validar que el usuario sea mayor de 18 años antes de crearlo
            if (!IsOver18(user))
            {
                ManageException(new Exception("User is not over 18 years"));
                return;
            }
            
            uCrud.Create(user);
        }

        public void Update(User user)
        {
            // Validar antes de actualizar
            if (!IsOver18(user))
            {
                ManageException(new Exception("User is not over 18 years"));
                return;
            }

            uCrud.Update(user);
        }

        public void Delete(User user)
        {
            var existingUser = RetrieveById(user.Id);
            if (existingUser != null)
            {
                uCrud.Delete(user);
            }
            else
            {
                ManageException(new Exception("User not found"));
            }
        }

        public List<User> RetrieveAll()
        {
            return uCrud.RetrieveAll<User>();
        }

        public User RetrieveById(int id)
        {
            return uCrud.RetrieveById<User>(id);
        }

        // Método privado para verificar que el usuario sea mayor de 18 años
        private bool IsOver18(User user)
        {
            var currentDate = DateTime.Now;
            int age = currentDate.Year - user.BirthDate.Year;

            if (user.BirthDate.Date > currentDate.AddYears(-age).Date)
            {
                age--;
            }
            return age >= 18;
        }
    }
}
