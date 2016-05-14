using Kino.App_Start;
using Kino.DAL;
using Kino.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;

namespace Kino.Controllers
{
    public class AccountController : Controller
    {
        private CinemaContext db = new CinemaContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;


        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }


        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Cinema");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }

        // GET: Home
        public ActionResult Login()
        {
            //  User user = new User();
            // db.Users.
            // db.SaveChanges();
            // var element = db.Users.ToList();
            // var user = db.Users.Where(a => a.Login == "Admin");
         //   ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public ActionResult ReturnCinema()
        {
            //  User user = new User();
            // db.Users.
            // db.SaveChanges();
            // var element = db.Users.ToList();
            // var user = db.Users.Where(a => a.Login == "Admin");
            //   ViewBag.ReturnUrl = returnUrl;
            return View("Cinema/Index");
        }


        [HttpPost]
        public async Task<ActionResult> Login(User model,string returnUrl)
        {
            //  User user = new User();
            // db.Users.
            // db.SaveChanges();
            // var element = db.Users.ToList();
            //  var user = db.Users.Where(a => a.Login == "Admin");

            /* if (!ModelState.IsValid)
             {
                 return View(model);
             }
             else
                return RedirectToAction("Index", "Cinema");*/
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password,false, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                   // Session["Username"] = model.Email;
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }


        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            
            return RedirectToAction("Index", "Cinema");
            
        }

        public string CheckUserLogin(string login, string password)
        {
            if (login == "admin" && password == "admin")
            {
                return "1";
            }
            else return "0";
           // return View();
        }

     

      

    }
}