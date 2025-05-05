using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Demo_1670.Models;

namespace Demo_1670.Controllers
{
    public class JobSeekerController : Controller
    {
        // Dashboard
        public IActionResult Dashboard()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            return View();
        }

        // Profile (GET)
        [HttpGet]
        public IActionResult EditProfile()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            var profile = new JobSeekerProfile
            {
                FullName = "Nguyen Van A",
                Email = "a.nguyen@email.com",
                Skills = "C#, ASP.NET, SQL",
                Experience = "3 years"
            };

            return View(profile);
        }

        // Profile (POST)
        [HttpPost]
        public IActionResult EditProfile(JobSeekerProfile model)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            ViewBag.Message = "Profile updated successfully.";
            return View(model);
        }

        // Application History
        public IActionResult ApplicationHistory()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            var applications = new List<dynamic>
            {
                new { JobTitle = "Backend Developer", Status = "Pending", AppliedDate = DateTime.Now.AddDays(-2) },
                new { JobTitle = "UI/UX Designer", Status = "Accepted", AppliedDate = DateTime.Now.AddDays(-4) }
            };

            return View(applications);
        }

        // MyApplications redirects to ApplicationHistory
        public IActionResult MyApplications()
        {
            return RedirectToAction("ApplicationHistory");
        }

        // Browse Jobs 
        public IActionResult BrowseJobs()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            var jobs = new List<dynamic>
            {
                new { Title = "Backend Developer", Location = "Hanoi", DatePosted = DateTime.Now.AddDays(-5) },
                new { Title = "UI/UX Designer", Location = "Da Nang", DatePosted = DateTime.Now.AddDays(-10) }
            };

            return View(jobs);
        }

        // Apply 
        public IActionResult Apply(string jobTitle)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            TempData["Message"] = $"You have successfully applied for {jobTitle}";
            return RedirectToAction("BrowseJobs");
        }

        // Check login helper
        private bool IsLoggedIn()
        {
            string username = HttpContext.Session.GetString("Username");
            string role = HttpContext.Session.GetString("Role");
            return !(string.IsNullOrEmpty(username) || role != "JobSeeker");
        }
    }
}
