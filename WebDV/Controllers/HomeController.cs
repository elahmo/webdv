using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDV.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Web.Security;

namespace WebDV.Controllers
{
 
    public class HomeController : Controller
    {
        private ApplicationDbContext SContext;
        private Submission[] Submissions;
       
        public HomeController()
        {
            
                     
        }

        [Route("/")]
        [HttpGet]
        [Authorize]
        public ViewResult Index()
        {
            bool isStudent = User.IsInRole("Student");
            if (isStudent)
            {
                SContext = new Models.ApplicationDbContext();
                Submissions = SContext.SubmissionDB.FindByUserID(User.Identity.GetUserId()).ToArray();
            }
            else
            {
                SContext = new Models.ApplicationDbContext();
                Submissions = SContext.SubmissionDB.ToArray();
            }
            var UsersContext = new ApplicationDbContext();
            var usernames = new Dictionary<string, string>();
            foreach (var mu in UsersContext.Users.ToList())
            {
                usernames.Add(mu.Id, mu.FirstName + " " + mu.LastName);

            }
            ViewBag.UserNames = usernames;
            return View(Submissions);

        }
    }
}