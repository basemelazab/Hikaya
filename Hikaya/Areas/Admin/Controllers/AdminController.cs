using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hikaya.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        [Authorize(Roles = "Admin")]
        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View("مرحبا");
        }
    }
}