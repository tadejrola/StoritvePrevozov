using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoritvePrevozov.Controllers
{
    public class IzvedeniPrevoziController : Controller
    {
        // GET: IzvedeniPrevozi
        public ActionResult Index()
        {
            return View();
        }
    }
}