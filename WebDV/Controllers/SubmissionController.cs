using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDV.Models;
using System.Diagnostics;
using Microsoft.AspNet.Identity;
using System.Web.Security;

namespace WebDV.Controllers
{
    [Authorize(Roles ="Student")]
    public class SubmissionController : Controller
    {
        // GET: Submission/Details/5
        [HttpGet]
        [Route("Submission/Details/")]
        public ActionResult Details(int id)
        {
            SubmissionContext SubContext = new Models.SubmissionContext();
            Submission[] Submissions = SubContext.SubmissionDB.FindBySubmissionID(id).ToArray();
            var users = Membership.GetAllUsers();
            var usernames = new Dictionary<Guid,string>();
            foreach (MembershipUser mu in users)
            {
                usernames.Add((Guid)mu.ProviderUserKey,mu.UserName);

            }
            ViewData["usernames"] = usernames;
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
                    userID = User.Identity.GetUserId(),
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
            return RedirectToAction("../");
        }
    }

}
