using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDV.Models;


namespace WebDV.Controllers
{
    public class FeedbackController : Controller
    {
        // GET: Feedback/Details/5
        public ActionResult Details(int id)
        {
            //int sid = 1;
            SubmissionContext SubContext = new Models.SubmissionContext();
            Submission[] Submissions = SubContext.SubmissionDB.FindBySubmissionID(id).ToArray();
            return View(Submissions);
        }

        // GET: Feedback/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Feedback/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Feedback/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
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
                Submissions[0].feedbackAuthor = 1;
                SubContext.SaveChanges();
              
                return RedirectToAction("../Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Feedback/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Feedback/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
