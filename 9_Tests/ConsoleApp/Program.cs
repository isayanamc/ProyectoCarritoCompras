using DataAccess.CRUDs;
using DTO;
using System;
using Newtonsoft.Json;  
using CoreApp;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            bool salir = false;
            while (!salir)
            {
                Console.WriteLine("\n===============================");
                Console.WriteLine("        🛒 Menú Principal 🛒        ");
                Console.WriteLine("===============================");
                Console.WriteLine(" 1. Crear Usuario");
                Console.WriteLine(" 2. Actualizar Usuario");
                Console.WriteLine(" 3. Listar Usuarios");
                Console.WriteLine(" 4. Buscar Usuario por ID");
                Console.WriteLine(" 5. Eliminar Usuario");
                Console.WriteLine(" 6. Salir");
                Console.WriteLine("===============================");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1": CrearUsuario(); break;
                    case "2": ActualizarUsuario(); break;   
                    case "3": ListarUsuarios(); break;
                    case "4": BuscarUsuarioPorID(); break;
                    case "5": EliminarUsuario(); break;   
                    case "6": salir = true; break;
                    default:
                        Console.WriteLine("❌ Opción inválida, intente nuevamente.");
                        break;
                }
            }
        }

        static void CrearUsuario()
        {
            var uManager = new UserManager();
            var user = new User();

            Console.WriteLine("\n=== Creación de usuario ===");

            Console.Write("📌 Digite el nombre: ");
            user.Name = Console.ReadLine();

            Console.Write("📌 Digite el apellido: ");
            user.LastName = Console.ReadLine();

            Console.Write("📌 Digite el correo electrónico: ");
            user.Email = Console.ReadLine();

            Console.Write("📌 Digite el número de teléfono: ");
            user.PhoneNumber = Console.ReadLine();

            Console.Write("📌 Digite la fecha de nacimiento (YYYY-MM-DD): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime birthDate))
            {
                Console.WriteLine("❌ Fecha inválida. Se asignará la fecha mínima.");
                birthDate = DateTime.MinValue;
            }
            user.BirthDate = birthDate;

            Console.Write("📌 Digite la contraseña: ");
            user.Password = Console.ReadLine();

            try
            {
                uManager.Create(user);
                Console.WriteLine("✅ Usuario creado exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🚫 No se pudo crear el usuario: {ex.Message}");
            }
        }

        static void ActualizarUsuario()
        {
            var uManager = new UserManager();
            Console.Write("Ingrese el ID del usuario a actualizar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var user = uManager.RetrieveById(id);
                if (user != null)
                {
                    Console.Write("Nuevo nombre: ");
                    user.Name = Console.ReadLine();

                    Console.Write("Nuevo email: ");
                    user.Email = Console.ReadLine();

                    uManager.Update(user);
                    Console.WriteLine("✅ Usuario actualizado correctamente.");
                }
                else Console.WriteLine("❌ Usuario no encontrado.");
            }
            else Console.WriteLine("❌ ID inválido.");
        }

        static void ListarUsuarios()
        {
            var uManager = new UserManager();
            var users = uManager.RetrieveAll();

            foreach (var u in users)
            {
                Console.WriteLine(JsonConvert.SerializeObject(u, Formatting.Indented)); 
            }
        }

        static void BuscarUsuarioPorID()
        {
            Console.Write("Ingrese el ID del usuario: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var uManager = new UserManager();
                var user = uManager.RetrieveById(id);
                if (user != null)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(user, Formatting.Indented));
                }
                else Console.WriteLine("❌ Usuario no encontrado.");
            }
            else Console.WriteLine("❌ ID inválido.");
        }

        static void EliminarUsuario()
        {
            var uManager = new UserManager();
            Console.Write("Ingrese el ID del usuario a eliminar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var user = uManager.RetrieveById(id);
                if (user != null)
                {
                    uManager.Delete(user);
                    Console.WriteLine("✅ Usuario eliminado correctamente.");
                }
                else Console.WriteLine("❌ Usuario no encontrado.");
            }
            else Console.WriteLine("❌ ID inválido.");
        }
    }
}
