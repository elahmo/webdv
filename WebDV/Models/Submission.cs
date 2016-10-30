using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebDV.Models
{
    public class Submission
    {
        public int submissionID { get; set; }
        public int userID { get; set; }
        public DateTime date { get; set; }
        public int grade { get; set; }
        public string feedbackText { get; set; }
        public int feedbackAuthor { get; set; }
    }
    public class SubmissionContext : DbContext
    {
        public SubmissionContext() : base() { }
        public DbSet<Submission> SubmissionDB { get; set; }
    }
}