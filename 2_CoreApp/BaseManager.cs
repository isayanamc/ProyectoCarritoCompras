using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp
{
    public class BaseManager
    {

            protected void ManageException(Exception exception)
            {
                //Implementacion sobre el manejo de excepciones pendiente
                Console.WriteLine("😔 se ha producido una excepcion: " + exception.ToString());

            }
    }


}