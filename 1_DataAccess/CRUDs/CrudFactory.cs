using DataAccess.DAOs;
using DTO;
using System;
using System.Collections.Generic;

namespace DataAccess.CRUDs
{
    // 🔹 Clase abstracta base para las operaciones CRUD
    public abstract class CrudFactory
    {
        // 🔹 Instancia de SqlDao protegida y accesible para las clases que hereden de CrudFactory
        protected readonly SqlDao _sqlDao;

        // 🔹 Constructor base que inicializa la instancia de SqlDao
        protected CrudFactory()
        {
            _sqlDao = SqlDao.GetInstance();
        }

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
