using DataAccess.DAOs;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUDs
{
    // 🔹 Clase abstracta base para las operaciones CRUD
    public abstract class CrudFactory
    {
        // 🔹 Instancia protegida de SqlDao para manejar las consultas a la base de datos
        protected SqlDao _sqlDao;

        // 🔹 Métodos abstractos que cada CRUD específico debe implementar
        public abstract void Create(BaseDTO dto);  // C -> Create
        public abstract void Update(BaseDTO dto);  // U -> Update
        public abstract void Delete(BaseDTO dto);  // D -> Delete
        
        // 🔹 Métodos de recuperación de datos
        public abstract T Retrieve<T>(BaseDTO dto);  // Recuperar un solo objeto basado en DTO
        public abstract T RetrieveById<T>(int id);  // Recuperar un solo objeto basado en un ID
        public abstract List<T> RetrieveAll<T>();  // Recuperar una lista de objetos de tipo T
    }
}
