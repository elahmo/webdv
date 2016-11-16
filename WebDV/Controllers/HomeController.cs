using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDV.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace WebDV.Controllers
{
 
    public class HomeController : Controller
    {
        private SubmissionContext SContext;
        private DbSet<Submission> SubmissionDB;
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
                SContext = new Models.SubmissionContext();
                Submissions = SContext.SubmissionDB.FindByUserID(User.Identity.GetUserId()).ToArray();
            }
            else
            {
                SContext = new Models.SubmissionContext();
                Submissions = SContext.SubmissionDB.ToArray();
                //Submissions = SubmissionDB.ToArray();
            }
            return View(Submissions);

        }
    }
}