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
            Console.WriteLine("      🛒 Menú Principal 🛒      ");
            Console.WriteLine("===============================");
            Console.WriteLine(" 1. Crear Usuario");
            Console.WriteLine(" 2. Actualizar Usuario");
            Console.WriteLine(" 3. Listar Usuarios");
            Console.WriteLine(" 4. Buscar Usuario por ID");
            Console.WriteLine(" 5. Eliminar Usuario");
            Console.WriteLine(" 6. Crear Producto");
            Console.WriteLine(" 7. Listar Productos");
            Console.WriteLine(" 8. Buscar Producto por Código");
            Console.WriteLine(" 9. Actualizar Producto");
            Console.WriteLine("10. Eliminar Producto");
            Console.WriteLine("11. Salir");
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
                case "6": CrearProducto(); break;
                case "7": ListarProductos(); break;
                case "8": BuscarProductoPorID(); break;
                case "9": ActualizarProducto(); break;
                case "10": EliminarProducto(); break;
                case "11": salir = true; break;
                default:
                    Console.WriteLine("❌ Opción inválida, intente nuevamente.");
                    break;
            }
        }
    }

//Usuarios
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
                    uManager.Delete(user.UserCode);
                    Console.WriteLine("✅ Usuario eliminado correctamente.");
                }
                else Console.WriteLine("❌ Usuario no encontrado.");
            }
            else Console.WriteLine("❌ ID inválido.");
        }

//Productos
        static void CrearProducto()
        {
                var pManager = new ProductCrudFactory();
                var product = new Product();

                Console.WriteLine("\n=== Creación de Producto ===");

                Console.Write("📌 Nombre: ");
                product.Name = Console.ReadLine() ?? "Sin Nombre";

                Console.Write("📌 Categoría: ");
                product.Category = Console.ReadLine() ?? "Sin Categoría";

                Console.Write("📌 Precio: ");
                if (!double.TryParse(Console.ReadLine(), out double price) || price < 0)
                {
                    Console.WriteLine("❌ Precio inválido, se establecerá en 0.");
                    price = 0;
                }
                product.Price = price;

                Console.Write("📌 Stock: ");
                if (!int.TryParse(Console.ReadLine(), out int stock) || stock < 0)
                {
                    Console.WriteLine("❌ Stock inválido, se establecerá en 0.");
                    stock = 0;
                }
                product.Stock = stock;

                Console.Write("📌 Código de producto: ");
                product.ProductCode = Console.ReadLine() ?? "SIN_CODIGO";

                try
                {
                    pManager.Create(product);
                    Console.WriteLine("✅ Producto creado exitosamente.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"🚫 No se pudo crear el producto: {ex.Message}");
                }
        }


        static void ActualizarProducto()
        {
            var pManager = new ProductCrudFactory();

            Console.Write("Ingrese el ID del producto a actualizar: ");
            if (!int.TryParse(Console.ReadLine(), out int productId))
            {
                Console.WriteLine("❌ Entrada inválida. Ingrese un número válido.");
                return;
            }

            var productoExistente = pManager.RetrieveById<Product>(productId);
            if (productoExistente == null)
            {
                Console.WriteLine("❌ Producto no encontrado.");
                return;
            }

            Console.Write("Nuevo nombre (dejar vacío para mantener el actual): ");
            string name = Console.ReadLine();
            if (!string.IsNullOrEmpty(name)) productoExistente.Name = name;

            Console.Write("Nueva categoría: ");
            string category = Console.ReadLine();
            if (!string.IsNullOrEmpty(category)) productoExistente.Category = category;

            Console.Write("Nuevo precio: ");
            if (double.TryParse(Console.ReadLine(), out double price) && price >= 0)
                productoExistente.Price = price;

            Console.Write("Nuevo stock: ");
            if (int.TryParse(Console.ReadLine(), out int stock) && stock >= 0)
                productoExistente.Stock = stock;

            try
            {
                pManager.Update(productoExistente);
                Console.WriteLine("✅ Producto actualizado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🚫 No se pudo actualizar el producto: {ex.Message}");
            }
        }


        static void EliminarProducto()
        {
            var pManager = new ProductCrudFactory();

            Console.Write("Ingrese el ID del producto a eliminar: ");
            if (!int.TryParse(Console.ReadLine(), out int productId))
            {
                Console.WriteLine("❌ Entrada inválida. Ingrese un número válido.");
                return;
            }

            var producto = pManager.RetrieveById<Product>(productId);
            if (producto == null)
            {
                Console.WriteLine("⚠️ Producto no encontrado.");
                return;
            }

            try
            {
                pManager.Delete(producto);
                Console.WriteLine("✅ Producto eliminado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🚫 No se pudo eliminar el producto: {ex.Message}");
            }
        }


        static void ListarProductos()
        {
            var pManager = new ProductCrudFactory();
            var productos = pManager.RetrieveAll<Product>();

            if (productos.Count == 0)
            {
                Console.WriteLine("⚠️ No hay productos registrados.");
                return;
            }

            foreach (var p in productos)
            {
                Console.WriteLine(JsonConvert.SerializeObject(p, Formatting.Indented));
            }
        }
        static void BuscarProductoPorID()
        {
            var pManager = new ProductCrudFactory();

            Console.Write("Ingrese el ID del producto a buscar: ");
            if (!int.TryParse(Console.ReadLine(), out int productId))
            {
                Console.WriteLine("❌ Entrada inválida. Ingrese un número válido.");
                return;
            }

            var producto = pManager.RetrieveById<Product>(productId);

            if (producto == null)
            {
                Console.WriteLine("⚠️ Producto no encontrado.");
                return;
            }

            Console.WriteLine(JsonConvert.SerializeObject(producto, Formatting.Indented));
        }

}

}