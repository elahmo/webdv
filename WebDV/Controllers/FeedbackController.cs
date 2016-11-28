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
            SubmissionContext SubContext = new Models.SubmissionContext();
            Submission[] Submissions = SubContext.SubmissionDB.FindBySubmissionID(id).ToArray();
            return View(Submissions);
   
        }

        // POST: Feedback/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                SubmissionContext SubContext = new Models.SubmissionContext();
                Submission[] Submissions = SubContext.SubmissionDB.FindBySubmissionID(id).ToArray();
                Submissions[0].grade = Int32.Parse(collection.Get("grade"));
                Submissions[0].feedbackText = collection.Get("feedbackText");
                Submissions[0].feedbackAuthor = User.Identity.GetUserId();
                SubContext.SaveChanges();
              
                return RedirectToAction("../");
            }
            catch
            {
                SubmissionContext SubContext = new Models.SubmissionContext();
                Submission[] Submissions = SubContext.SubmissionDB.FindBySubmissionID(id).ToArray();
                ModelState.AddModelError("fileError", "The feedback message cannot exceed 200 characters.");
                return View("Details", Submissions);
            }
        }
    }
}
