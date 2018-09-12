using CMS.Core.Services;
using CMS.Domain.Domains;
using CMS.Domain.SessionSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CMS.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(User user)
        {
           
            bool result=Services.LoginService.Login(user);
            if (result)
            {

                User me = SessionSet<User>.Get("User");
                Session["Name"] = me.Name;
                return RedirectToAction("List","Layout");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Register(User user)
        {
            bool result = Services.LoginService.Register(user);
            if (result)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Logout()
        {
            
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login","Login");
        }
    }
}