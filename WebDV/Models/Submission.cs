﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDV.Models
{
    public class Submission
    {
        public int submissionID { get; set; }
        public int userID { get; set; }
        public DateTime date { get; set; }
        public int Grade { get; set; }
        public string feedbackText { get; set; }
        public int feedbackAuthor { get; set; }
    }
}