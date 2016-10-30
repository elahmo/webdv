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
            SubmissionDB.Add(new Submission { submissionID = 0, date = new DateTime(2016, 1, 5), feedbackAuthor = 0, feedbackText = "Good Job", grade = 10, userID = 1 });
            SubmissionDB.Add(new Submission { submissionID = 1, date = new DateTime(2016, 2, 4), feedbackAuthor = 0, feedbackText = "Hola Amigo como estas?", grade = 9, userID = 1 });
            SubmissionDB.Add(new Submission { submissionID = 2, date = new DateTime(2016, 3, 3), feedbackAuthor = 0, feedbackText = "Well that's fine.", grade = 8, userID = 1 });
            SubmissionDB.Add(new Submission { submissionID = 3, date = new DateTime(2016, 4, 2), feedbackAuthor = 0, feedbackText = "Hooray!!", grade = 6, userID = 1 });
            SubmissionDB.Add(new Submission { submissionID = 4, date = new DateTime(2016, 5, 1), feedbackAuthor = 0, feedbackText = "Money!!!", grade = 5, userID = 1 });
            SContext.SaveChanges();
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