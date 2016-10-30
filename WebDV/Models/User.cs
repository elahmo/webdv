using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebDV.Models
{
	public class User
	{
        public int userID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int role { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
    public class UserContext : DbContext
    {
        public UserContext() : base() { }
        public DbSet<User> UserDB { get; set; }
}