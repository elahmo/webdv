using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebDV.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        [HttpGet]
        public ViewResult Index()
        {
            return View();

        }
    }
}