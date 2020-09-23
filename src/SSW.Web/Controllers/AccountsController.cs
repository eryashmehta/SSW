﻿using SSW.Data.Repositories;
using SSW.Web.ViewModels.Account;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using SSW.Web.Services;
using SSW.Data.Entities;

namespace SSW.Web.Controllers
{
    public class AccountsController : Controller
    {
        private const int _cookieTimeoutInMinutes = 10;

        private readonly CookieService _cookieService;
        private readonly IRepository<User> _repository;

        public AccountsController(CookieService cookieService, IRepository<User> repository)
        {
            _cookieService = cookieService;
            _repository = repository;
        }

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<ActionResult> Login(UserLoginVM user)
        // {
        //     var authenticatedUser = await _repository.FirstOrDefaultAsync(x => x.Email == user.Email && x.Password == user.Password, u => new { u.Email });
        //
        //     if (authenticatedUser == null)
        //     {
        //         ModelState.AddModelError("IncorrectPassword", "Incorrect email or password");
        //         return View();
        //     }
        //
        //     _cookieService.SetAuthenticationToken(authenticatedUser.Email, user.RememberMe, _cookieTimeoutInMinutes);
        //     return RedirectToAction("Index", "Home");
        // }
        
        [HttpPost]
        public async Task<JsonResult> Login(UserLoginVM user)
        {
            var authenticatedUser = await _repository.FirstOrDefaultAsync(x => x.Email == user.Email && x.Password == user.Password, u => new { u.Email });

            if (authenticatedUser == null)
            {
                ModelState.AddModelError("IncorrectPassword", "Incorrect email or password");
                return Json(new {success = false, responseText = "Incorrect email or password"});
            }

            _cookieService.SetAuthenticationToken(authenticatedUser.Email, user.RememberMe, _cookieTimeoutInMinutes);
            return Json(new {success = true, responseText = ""});
        }

        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", new { controller = "Home", area = string.Empty });
        }

    }
}