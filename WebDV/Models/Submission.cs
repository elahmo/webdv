using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDV.Models
{
    public class Submission
    {
        [Key,Required(ErrorMessage = "submissionID is required")]
        public int submissionID { get; set; }
        [Required(ErrorMessage = "userID is required")]
        public string userID { get; set; }
        [Required(ErrorMessage = "date is required")]
        public DateTime date { get; set; }
        [Required(ErrorMessage = "submissionData is required")]
        public byte[] submissionData { get; set; }
        [Required, Range(-1, 10,ErrorMessage = "Grade must be between -1 or 10.")]
        public int grade { get; set; }
        [StringLength(200, ErrorMessage = "Maximum length of feedback text is 200 characters.")]
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