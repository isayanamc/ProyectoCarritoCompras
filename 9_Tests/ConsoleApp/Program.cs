using DataAccess.CRUDs;
using DTO;
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            bool salir = false;
            while (!salir)
            {
                Console.WriteLine("=================================");
                Console.WriteLine("        🛒 Menú Principal 🛒        ");
                Console.WriteLine("=================================");
                Console.WriteLine(" 1. Crear Usuario");
                Console.WriteLine(" 2. Actualizar Usuario");
                Console.WriteLine(" 3. Crear Producto");
                Console.WriteLine(" 4. Actualizar Producto");
                Console.WriteLine(" 5. Eliminar Usuario");
                Console.WriteLine(" 6. Eliminar Producto");
                Console.WriteLine(" 7. Salir");
                Console.WriteLine("=================================");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1":
                        CrearUsuario();
                        break;
                    case "2":
                        ActualizarUsuario();
                        break;
                    case "3":
                        CrearProducto();
                        break;
                    case "4":
                        ActualizarProducto();
                        break;
                    case "5":
                        EliminarUsuario();
                        break;
                    case "6":
                        EliminarProducto();
                        break;
                    case "7":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("❌ Opción inválida, intente nuevamente.");
                        break;
                }
            }
        }

        static void CrearUsuario()
        {
            Console.WriteLine("\n=== Crear Usuario ===");
            Console.Write("Código de usuario: ");
            string userCode = Console.ReadLine();

            Console.Write("Nombre: ");
            string name = Console.ReadLine();

            Console.Write("Apellido: ");
            string lastName = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Teléfono: ");
            string _phone = Console.ReadLine();

            Console.Write("Fecha de nacimiento (yyyy-mm-dd): ");
            DateTime birthdate = DateTime.Parse(Console.ReadLine());

            Console.Write("Contraseña: ");
            string password = Console.ReadLine();

            var user = new User()
            {
                UserCode = userCode,
                Name = name,
                LastName = lastName,
                Email = email,
                PhoneNumber = _phone,
                BirthDate = birthdate,
                Password = password
            };

            var userCrud = new UserCrudFactory();
            userCrud.Create(user);

            Console.WriteLine("✅ Usuario creado exitosamente.");
        }

        static void ActualizarUsuario()
        {
            Console.WriteLine("\n=== Actualizar Usuario ===");
            Console.Write("Digite el ID del usuario: ");
            int _id = int.Parse(Console.ReadLine());

            Console.Write("Digite el código de usuario: ");
            string _userCode = Console.ReadLine();

            Console.Write("Digite el nombre: ");
            string _userName = Console.ReadLine();

            Console.Write("Digite el apellido: ");
            string _lastName = Console.ReadLine();

            Console.Write("Digite el email: ");
            string _email = Console.ReadLine();

            Console.Write("Digite el teléfono: ");
            string _phone = Console.ReadLine();

            Console.Write("Digite la fecha de nacimiento (yyyy-MM-dd): ");
            DateTime _birthdate = DateTime.Parse(Console.ReadLine());

            Console.Write("Digite el password: ");
            string _password = Console.ReadLine();

            UserCrudFactory uCrud = new UserCrudFactory();

            // Creación del usuario con los datos obtenidos
            var user = new User
            {
                Id = _id,
                UserCode = _userCode,
                Name = _userName,
                LastName = _lastName,
                Email = _email,
                PhoneNumber = _phone,
                BirthDate = _birthdate,
                Password = _password
            };

            uCrud.Update(user);
            Console.WriteLine("✅ Usuario actualizado exitosamente.");
        }

        static void CrearProducto()
        {
            Console.WriteLine("\n=== Crear Producto ===");
            Console.Write("Código del producto: ");
            string productCode = Console.ReadLine();

            Console.Write("Nombre del producto: ");
            string name = Console.ReadLine();

            Console.Write("Categoría: ");
            string category = Console.ReadLine();

            Console.Write("Precio: ");
            double price = double.Parse(Console.ReadLine());

            Console.Write("Cantidad en stock: ");
            int stock = int.Parse(Console.ReadLine());

            var product = new Product()
            {
                ProductCode = productCode,
                Name = name,
                Category = category,
                Price = price,
                Stock = stock
            };

            var productCrud = new ProductCrudFactory();
            productCrud.Create(product);

            Console.WriteLine("✅ Producto creado exitosamente.");
        }

        static void ActualizarProducto()
        {
            Console.WriteLine("\n=== Actualizar Producto ===");
            Console.Write("Código del producto: ");
            string productCode = Console.ReadLine();

            Console.Write("Nuevo nombre del producto: ");
            string name = Console.ReadLine();

            Console.Write("Nueva categoría: ");
            string category = Console.ReadLine();

            Console.Write("Nuevo precio: ");
            double price = double.Parse(Console.ReadLine());

            Console.Write("Nueva cantidad en stock: ");
            int stock = int.Parse(Console.ReadLine());

            var product = new Product()
            {
                ProductCode = productCode,
                Name = name,
                Category = category,
                Price = price,
                Stock = stock
            };

            var productCrud = new ProductCrudFactory();
            productCrud.Update(product);

            Console.WriteLine("✅ Producto actualizado exitosamente.");
        }

        static void EliminarUsuario()
        {
            Console.WriteLine("\n=== Eliminar Usuario ===");
            Console.Write("Ingrese el código del usuario a eliminar: ");
            string userCode = Console.ReadLine();

            var user = new User() { UserCode = userCode };
            var userCrud = new UserCrudFactory();
            userCrud.Delete(user);

            Console.WriteLine("✅ Usuario eliminado exitosamente.");
        }

        static void EliminarProducto()
        {
            Console.WriteLine("\n=== Eliminar Producto ===");
            Console.Write("Ingrese el código del producto a eliminar: ");
            string productCode = Console.ReadLine();

            var product = new Product()
            {
                ProductCode = productCode,
                Name = "temp",   // Valores temporales para evitar errores
                Category = "temp",
                Price = 0.0,
                Stock = 0
            };

            var productCrud = new ProductCrudFactory();
            productCrud.Delete(product);

            Console.WriteLine("✅ Producto eliminado exitosamente.");
        }
    }
}
