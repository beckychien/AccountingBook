using AccountingBook.Models.ViewModels;
using AccountingBook.Repositories;
using AccountingBook.Service;
using AuthSample;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Security.Claims;
using Microsoft.Owin.Security;
using AccountingBook.Models;

namespace AccountingBook.Controllers
{
    public class AccountController : Controller
    {
        private IAuthenticationManager _authManager;

        private readonly AccBookService _accbookSvc;

        public AccountController()
        {
            var unitOfWork = new EFUnitOfWork();
            _accbookSvc = new AccBookService(unitOfWork);

        }

        public IAuthenticationManager AuthenticationManager
        {
            get
            {
                if (_authManager == null)
                    _authManager = HttpContext.GetOwinContext().Authentication;
                return _authManager;
            }
            set { _authManager = value; }
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            if (_accbookSvc.AuthAccount(model.Account, model.Password))
            {
                var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, model.Account) }, "ApplicationCookie");
                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;
                authManager.SignIn(identity);

                return RedirectToAction("Index", "Home", new { area = "Backend" });
            }
            else
            {
                ModelState.AddModelError("", "登入嘗試失試。");
                return View(model);
            }

        }


        public ActionResult SignOut()
        {
            _authManager = HttpContext.GetOwinContext().Authentication;
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");

        }





    }
}