using BlogProject.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogProject.Controllers
{
    public class UserController : Controller
    {

        // GET: Login
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = Request.RawUrl;
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password, string returnUrl)
        {
            if (new LoginState().IsLoginSucces(username, password))
            {
                if (Url.IsLocalUrl(returnUrl) && returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Admin");
                }

            }
            return RedirectToAction("Login", "User");
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Login", "User");
        }
    }
}