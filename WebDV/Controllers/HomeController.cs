using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDV.Models;
using System.Data.Entity;

namespace WebDV.Controllers
{
    public class HomeController : Controller
    {
        private SubmissionContext SContext;
        private DbSet<Submission> SubmissionDB;
        private Submission[] Submissions;
        public HomeController()
        {
            SContext = new Models.SubmissionContext();
            SubmissionDB = SContext.SubmissionDB;
            Submissions = SubmissionDB.ToArray();
        }

        [Route("/")]
        [HttpGet]
        public ViewResult Index()
        {

            return View(Submissions);

        }
    }
}