using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using GameSystem.Models;

namespace GameSystem.Controllers
{
    public class AccountController : Controller
    {
        private GameDBEntities db = new GameDBEntities();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(string account, string pwd)
        {
            Account login = db.Account.FirstOrDefault(a => a.AccountName == account && a.AccountPwd == pwd);
            if (login != null)
            {
                Session["login"] = login;//Save user information in Session
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["msg"] = "Wrong username or password,Login failed!!";
            }
            return View();
        }

        // GET: /Account/Register 
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(Account a)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    db.Account.Add(a);
                    db.SaveChanges();
                    TempData["msg"] = "Registering succeeded, please log in with your account and password!!";
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    TempData["msg"] = "Sheet verification failed!";
                }
            }
            catch (Exception)
            {
                TempData["msg"] = "Registering failed!!";
            }
            return View();
        }

    }
}