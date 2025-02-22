using DataAccess.CRUDs;
using DTO;
using System;
using Newtonsoft.Json;  

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
            var uCrud = new UserCrudFactory();
            var user = new User();

            Console.WriteLine("\n=== Creación de usuario ===");

            Console.Write("📌 Digite el código de usuario: ");
            user.UserCode = Console.ReadLine();

            Console.Write("📌 Digite el nombre: ");
            user.Name = Console.ReadLine();

            Console.Write("📌 Digite el apellido: ");
            user.LastName = Console.ReadLine();

            Console.Write("📌 Digite el correo electrónico: ");
            user.Email = Console.ReadLine();

            Console.Write("📌 Digite el número de teléfono: ");
            user.PhoneNumber = Console.ReadLine();

            Console.Write("📌 Digite la fecha de nacimiento (YYYY-MM-DD): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime birthDate))
            {
                user.BirthDate = birthDate;
            }
            else
            {
                Console.WriteLine("❌ Fecha inválida. Se asignará la fecha mínima.");
                user.BirthDate = DateTime.MinValue;
            }

            Console.Write("📌 Digite la contraseña: ");
            user.Password = Console.ReadLine();

            // Llamada a la función para guardar el usuario
            uCrud.Create(user);

            Console.WriteLine("✅ Usuario creado exitosamente.");
        }


        static void ActualizarUsuario()
        {
            var uCrud = new UserCrudFactory();
            Console.Write("Ingrese el ID del usuario a actualizar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var user = uCrud.RetrieveById<User>(id);
                if (user != null)
                {
                    Console.Write("Nuevo nombre: ");
                    user.Name = Console.ReadLine();

                    Console.Write("Nuevo email: ");
                    user.Email = Console.ReadLine();

                    uCrud.Update(user);
                    Console.WriteLine("✅ Usuario actualizado correctamente.");
                }
                else Console.WriteLine("❌ Usuario no encontrado.");
            }
            else Console.WriteLine("❌ ID inválido.");
        }

        static void ListarUsuarios()
        {
            var uCrud = new UserCrudFactory();
            var users = uCrud.RetrieveAll<User>();

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
                var uCrud = new UserCrudFactory();
                var user = uCrud.RetrieveById<User>(id);
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
            var uCrud = new UserCrudFactory();
            Console.Write("Ingrese el ID del usuario a eliminar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var user = uCrud.RetrieveById<User>(id);
                if (user != null)
                {
                    uCrud.Delete(user);
                    Console.WriteLine("✅ Usuario eliminado correctamente.");
                }
                else Console.WriteLine("❌ Usuario no encontrado.");
            }
            else Console.WriteLine("❌ ID inválido.");
        }
    }
}
