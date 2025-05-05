using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Demo_1670.Models;
using System.Collections.Generic;
using System.Linq;

namespace Demo_1670.Controllers
{
    public class AccountController : Controller
    {
        
        private static List<User> users = new List<User>
        {
            new User { Username = "admin", Password = "admin123", Role = "Admin" },
            new User { Username = "seeker", Password = "123", Role = "JobSeeker" },
            new User { Username = "employer", Password = "123", Role = "Employer" }
        };

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User model)
        {
            var user = users.FirstOrDefault(u =>
                u.Username == model.Username &&
                u.Password == model.Password &&
                u.Role == model.Role);

            if (user == null)
            {
                ViewBag.Error = "Invalid username, password, or role.";
                return View();
            }

            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);

            switch (user.Role)
            {
                case "Admin": return RedirectToAction("Dashboard", "Admin");
                case "JobSeeker": return RedirectToAction("Dashboard", "JobSeeker");
                case "Employer": return RedirectToAction("Dashboard", "Employer");
                default: return RedirectToAction("Login");
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User model)
        {
            if (users.Any(u => u.Username == model.Username))
            {
                ViewBag.Error = "Username already exists.";
                return View();
            }

            users.Add(model);
            TempData["Message"] = "Registration successful. Please log in.";
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
