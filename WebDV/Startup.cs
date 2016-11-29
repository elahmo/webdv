using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using WebDV.Models;

[assembly: OwinStartupAttribute(typeof(WebDV.Startup))]
namespace WebDV
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            setUpRoles();
        }
        public void setUpRoles()
        {
            ApplicationDbContext appContext = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(appContext));

            if (!roleManager.RoleExists("Student"))
            {
                var studentRole = new IdentityRole();
                studentRole.Name = "Student";
                roleManager.Create(studentRole);
            }
            if (!roleManager.RoleExists("Lecturer"))
            {
                var lecturerRole = new IdentityRole();
                lecturerRole.Name = "Lecturer";
                roleManager.Create(lecturerRole);
            }
        }
    }
}
