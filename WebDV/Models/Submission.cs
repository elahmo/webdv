using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;


namespace WebDV.Models
{
    public class Submission
    {
        public int submissionID { get; set; }
        [Required]
        public string userID { get; set; }
        public DateTime date { get; set; }
        [Required]
        public byte[] submissionData { get; set; }
        public int grade { get; set; }
        [StringLength(200)]
        public string feedbackText { get; set; }
        public string feedbackAuthor { get; set; }
    }

    public static class MoreExtensionMethods
    {
        public static IEnumerable<Submission> FindBySubmissionID(
            this IEnumerable<Submission> submissions, int sid)
        {
            return (from i in submissions where i.submissionID == sid select i);
        }
        public static IEnumerable<Submission> FindByUserID(
            this IEnumerable<Submission> submissions, string uid)
        {
            return (from i in submissions where i.userID == uid select i);
        }
        public static void DeleteImagesByPersonID(
            this ApplicationDbContext submissions, int sid)
        {
            foreach (Submission i in submissions.SubmissionDB)
            {
                if (i.submissionID == sid) submissions.SubmissionDB.Remove(i);
            }
            submissions.SaveChanges();
        }
    }
}