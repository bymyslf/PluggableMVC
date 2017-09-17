using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestPlugin.Controllers
{
    public class TestPluginController : Controller
    {
        // GET: TestPlugin
        public ActionResult Index()
        {
            return View();
        }
    }
}