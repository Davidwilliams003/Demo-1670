using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Demo_1670.Models;
using System.Collections.Generic;
using System.Linq;

namespace Demo_1670.Controllers
{
    public class AdminController : Controller
    {
        private static List<User> accounts = new List<User>
        {
            new User { Username = "admin", Password = "admin123", Role = "Admin" },
            new User { Username = "jobseeker", Password = "123", Role = "JobSeeker" },
            new User { Username = "employer", Password = "123", Role = "Employer" }
        };

        public IActionResult Dashboard()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            return View();
        }

        public IActionResult ManageUsers()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            return View(accounts);
        }

        public IActionResult Detail(string username)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            var user = accounts.FirstOrDefault(u => u.Username == username);
            if (user == null)
                return RedirectToAction("ManageUsers");

            return View(user);
        }

        public IActionResult SystemLogs()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            ViewBag.TotalUsers = accounts.Count;
            ViewBag.AdminCount = accounts.Count(u => u.Role == "Admin");
            ViewBag.JobSeekerCount = accounts.Count(u => u.Role == "JobSeeker");
            ViewBag.EmployerCount = accounts.Count(u => u.Role == "Employer");

      
            var logs = new List<string>
            {
                "Admin logged in at 08:00",
                "Employer posted a job at 09:30",
                "JobSeeker applied at 10:45"
            };

            return View(logs);
        }

        private bool IsAdmin()
        {
            var role = HttpContext.Session.GetString("Role");
            return role == "Admin";
        }
    }
}
