using Demo_1670.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo_1670.Controllers
{
    public class EmployerController : Controller
    {
        private bool IsEmployer()
        {
            return HttpContext.Session.GetString("Role") == "Employer";
        }

        public IActionResult JobListing()
        {
            if (!IsEmployer())
                return RedirectToAction("Login", "Account");

            return View(Datastore.JobListings);
        }


        [HttpPost]
        public IActionResult Create(JobListing job)
        {
            if (!IsEmployer()) return RedirectToAction("Login", "Account");

            job.PostedDate = DateTime.Now;
            Datastore.JobListings.Add(job);
            TempData["Message"] = "Job added successfully!";
            return RedirectToAction("JobListing");
        }

        [HttpPost]
        public IActionResult Edit(int index, JobListing updated)
        {
            if (!IsEmployer()) return RedirectToAction("Login", "Account");

            if (index >= 0 && index < Datastore.JobListings.Count)
            {
                updated.PostedDate = Datastore.JobListings[index].PostedDate;
                Datastore.JobListings[index] = updated;
                TempData["Message"] = "Job updated successfully!";
            }
            return RedirectToAction("JobListing");
        }

        [HttpPost]
        public IActionResult Delete(int index)
        {
            if (!IsEmployer()) return RedirectToAction("Login", "Account");

            if (index >= 0 && index < Datastore.JobListings.Count)
            {
                Datastore.JobListings.RemoveAt(index);
                TempData["Message"] = "Job deleted successfully!";
            }
            return RedirectToAction("JobListing");
        }


        public IActionResult Dashboard()
        {
            if (!IsEmployer()) return RedirectToAction("Login", "Account");
            return View();
        }

        public IActionResult Profile()
        {
            if (!IsEmployer()) return RedirectToAction("Login", "Account");

            var employerProfile = new
            {
                CompanyName = "Tech Solutions Co.",
                Email = "contact@techsolutions.com",
                Description = "Leading IT recruitment agency."
            };

            return View(employerProfile);
        }

        

        public IActionResult ViewApplicants(string jobTitle)
        {
            if (!IsEmployer()) return RedirectToAction("Login", "Account");

            var applicants = new List<ApplicantViewModel>
            {
                new ApplicantViewModel { Name = "Nguyen Van A", JobTitle = "Backend Developer", Status = "Pending" },
                new ApplicantViewModel { Name = "Tran Thi B", JobTitle = "Frontend Developer", Status = "Accepted" },
                new ApplicantViewModel { Name = "Le Van C", JobTitle = "Backend Developer", Status = "Rejected" }
            };

            if (!string.IsNullOrEmpty(jobTitle))
            {
                applicants = applicants.Where(a => a.JobTitle == jobTitle).ToList();
            }

            ViewBag.JobTitles = applicants.Select(a => a.JobTitle).Distinct().ToList();
            ViewBag.SelectedJobTitle = jobTitle;

            return View(applicants);
        }

        [HttpPost]
        public IActionResult UpdateStatus(string username, string jobTitle, string status)
        {
            TempData["UpdatedUser"] = username;
            TempData["StatusMessage"] = "Update successful";
            return RedirectToAction("ViewApplicants");
        }
    }
}
