﻿using MDW.Filters;
using System.Web;
using System.Web.Mvc;

namespace MDW.Controllers
{
    [LogPage]
    [LogError]
    public class LogoutController : ControllerBase
    {
        public ActionResult Index()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();

            Session["username"] = null;

            return RedirectToAction("Index", "Login");
        }
    }
}