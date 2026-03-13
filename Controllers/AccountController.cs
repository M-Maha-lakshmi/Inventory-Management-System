using InventoryManagementSystem.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly InventoryDbContext _dbcontext;

        public AccountController(InventoryDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _dbcontext.Users.FirstOrDefault(x => x.Username == username);

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                HttpContext.Session.SetString("Username", user.Username);
                return RedirectToAction("Index", "Inventory");
            }

            ViewBag.Error = "Invalid username or password";
            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _dbcontext.Users.Add(user);
            _dbcontext.SaveChanges();

            return RedirectToAction("Login");
        }
    }
}
