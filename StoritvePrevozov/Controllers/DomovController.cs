using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoritvePrevozov.Controllers
{
    public class DomovController : Controller
    {
        // GET: Domov
        public ActionResult Index()
        {
            return View();
        }
    }
}