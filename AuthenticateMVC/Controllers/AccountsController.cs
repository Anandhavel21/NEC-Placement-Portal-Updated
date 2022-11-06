using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AuthenticateMVC.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Logging;

namespace AuthenticateMVC.Controllers
{
    public class AccountsController : Controller
    {
        TACV_DBEntities entity = new TACV_DBEntities();

        // GET: Accounts
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel Credentials)
        {
            bool userexit = entity.UsersTbls.Any(x => x.Email == Credentials.Email && x.Passcode == Credentials.Password);
            UsersTbl u = entity.UsersTbls.FirstOrDefault(x => x.Email == Credentials.Email && x.Passcode == Credentials.Password);
            if (userexit)
            {
                FormsAuthentication.SetAuthCookie(u.Username,false);
                return RedirectToAction("Index","Home");
            }

            //ModelState.AddModelError("", "Username or Password is wrong");

            return View();
        }

        [HttpPost]
        public ActionResult Signup(UsersTbl userinfo)
        {
            entity.UsersTbls.Add(userinfo);
            entity.SaveChanges();
            return RedirectToAction("Login");
        }

        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> ForgotPassword(ForgetViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        //var user = await UserManagerExtensions.FindByEmail(model.Email);
        //        var user = await UserManager

                

        //        return View();
        //    }
        //    return View(model);
        //}

        public ActionResult CheckMail()
        {
            return View();
        }
    }
}