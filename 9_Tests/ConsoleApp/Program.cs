using DataAccess.CRUDs;
using DTO;
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var userCode = "dcordoba";
            var userName = "Dennis";
            var lastName = "Cordoba";
            var email = "dcordoba@ucewnfotec.ac.cr";
            var phone = 87239117;
            var birthdate = DateTime.Now;
            var password = "LaClaveEslamisma";

            var uCrud = new UserCrudFactory();

            var user = new User()
            {
                UserCode = userCode,
                Name = userName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phone,
                BirthDate = birthdate,
                Password = password
            };

            uCrud.Create(user);
            Console.WriteLine("Usuario creado exitosamente en la base de datos.");
        }
    }
}