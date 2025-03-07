using Microsoft.AspNetCore.Mvc;
using _4_WebApp.Models;
namespace _4_WebApp.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
