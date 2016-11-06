using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDV.Models;
using System.Diagnostics;
namespace WebDV.Controllers
{
    public class SubmissionController : Controller
    {
        // GET: Submission/Details/5
        [HttpGet]
        [Route("Submission/Details/")]
        public ActionResult Details()
        {
            int sid = 89;
            SubmissionContext SubContext = new Models.SubmissionContext();
            Submission[] Submissions = SubContext.SubmissionDB.FindBySubmissionID(sid).ToArray();
            return View(Submissions);
        }

        // GET: Submission/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Submission/Upload
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase SelectedSubmission)
        {
            if (SelectedSubmission != null)
            {
                Submission sub = new Submission
                {
                    userID = 1,
                    date = DateTime.Now,
                    grade = 0,
                    feedbackText = null,
                    feedbackAuthor = 1,
                    submissionData = new byte[SelectedSubmission.ContentLength]
                };
                SelectedSubmission.InputStream.Read(sub.submissionData, 0, sub.submissionData.Length);
                SubmissionContext SubContext = new Models.SubmissionContext();
                SubContext.SubmissionDB.Add(sub);
                SubContext.SaveChanges();
            }
            return RedirectToAction("Details");
        }

        // GET: Submission/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Submission/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Submission/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Submission/Delete/5
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
