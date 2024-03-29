﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDV.Models;
using System.Diagnostics;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using System.Data.Entity.Validation;

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
            ApplicationDbContext SubContext = new Models.ApplicationDbContext();
            Submission[] Submissions = SubContext.SubmissionDB.FindBySubmissionID(id).ToArray();

            //check if user has made the submission, if not do not allow the access
            if (User.Identity.GetUserId() != Submissions[0].userID)
            {
                return RedirectToAction("../");
            } else //user is a student that has uploaded the file 
            {
                return View(Submissions);
            }
        }

        // GET: Submission/View/5
        [HttpGet]
        [Route("Submission/View/")]
        public String View(int id)
        {
            ApplicationDbContext SubContext = new Models.ApplicationDbContext();
            Submission[] Submissions = SubContext.SubmissionDB.FindBySubmissionID(id).ToArray();

            //check if user has made the submission, if not do not allow the access
            if (User.Identity.GetUserId() != Submissions[0].userID)
            {
                return "Not allowed!";
            }
            else //user is a student that has uploaded the file 
            {
                return System.Text.Encoding.UTF8.GetString(Submissions[0].submissionData);
            }
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
            if (SelectedSubmission != null && SelectedSubmission.ContentType.ToLower() == "text/html" && 
                (System.IO.Path.GetExtension(SelectedSubmission.FileName).ToLower() != ".html" ||
                System.IO.Path.GetExtension(SelectedSubmission.FileName).ToLower() != ".htm"))
            {
                Submission sub = new Submission
                {
                    userID = User.Identity.GetUserId(),
                    date = DateTime.Now,
                    grade = -1,
                    feedbackText = null,
                    feedbackAuthor = null,
                    submissionData = new byte[SelectedSubmission.ContentLength]
                };
                SelectedSubmission.InputStream.Read(sub.submissionData, 0, sub.submissionData.Length);
                ApplicationDbContext SubContext = new Models.ApplicationDbContext();
                using (var transactionQueue = SubContext.Database.BeginTransaction())
                {
                    try {
                        SubContext.SubmissionDB.Add(sub);
                        SubContext.SaveChanges();

                        transactionQueue.Commit();

                        return RedirectToAction("../");

                    } catch (DbEntityValidationException exception) {
                        foreach (var errors in exception.EntityValidationErrors)
                        {
                            foreach (var error in errors.ValidationErrors)
                            {
                                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                            }
                        }
                        return View("Create");
                    } catch {
                        transactionQueue.Rollback();
                        ModelState.AddModelError("fileError", "There was an error with talking to the database.");
                        return View("Create");
                    }
                }
            } else {
                ModelState.AddModelError("fileError", "Something was wrong with the file you selected. Please ensure it is a proper html file.");
                return View("Create");
            }
        }
    }

}
