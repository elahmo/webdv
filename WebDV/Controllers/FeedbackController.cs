using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDV.Models;


namespace WebDV.Controllers
{
    [Authorize(Roles = "Lecturer")]
    public class FeedbackController : Controller
    {
        // GET: Feedback/Details/5
        public ActionResult Details(int id)
        {
            ApplicationDbContext SubContext = new Models.ApplicationDbContext();
            Submission[] Submissions = SubContext.SubmissionDB.FindBySubmissionID(id).ToArray();
            return View(Submissions);
   
        }

        // GET: Feedback/View/5
        [HttpGet]
        [Route("Feedback/View/")]
        public String View(int id)
        {
            ApplicationDbContext SubContext = new Models.ApplicationDbContext();
            Submission[] Submissions = SubContext.SubmissionDB.FindBySubmissionID(id).ToArray();

            return System.Text.Encoding.UTF8.GetString(Submissions[0].submissionData);
        }

        // POST: Feedback/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            ApplicationDbContext SubContext = new Models.ApplicationDbContext();
            try
            {
                using (var transactionQueue = SubContext.Database.BeginTransaction())
                {
                    try
                    {
                        Submission[] Submissions = SubContext.SubmissionDB.FindBySubmissionID(id).ToArray();
                        Submissions[0].grade = Int32.Parse(collection.Get("grade"));
                        Submissions[0].feedbackText = collection.Get("feedbackText");
                        Submissions[0].feedbackAuthor = User.Identity.GetUserId();
                        SubContext.SaveChanges();

                        transactionQueue.Commit();

                        return RedirectToAction("../");

                    } catch {
                        transactionQueue.Rollback();
                        Submission[] Submissions = SubContext.SubmissionDB.FindBySubmissionID(id).ToArray();
                        ModelState.AddModelError("fileError", "There was an error with talking to the database.");
                        return View("Details", Submissions);
                    }

                }                
            }
            catch
            {
                Submission[] Submissions = SubContext.SubmissionDB.FindBySubmissionID(id).ToArray();
                ModelState.AddModelError("fileError", "The feedback message cannot exceed 200 characters.");
                return View("Details", Submissions);
            }
        }
    }
}
