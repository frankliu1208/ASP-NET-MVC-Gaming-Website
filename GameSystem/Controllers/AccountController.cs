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
        private GameDBEntities db = new GameDBEntities();  // 
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
        public ActionResult Login(string account, string pwd)  //account, pwd parameter shall be aligned with the name attribute in Login.cshtml
        {

            // when getting the acounnmae and password,  go to database for checking
            Account login = db.Account.FirstOrDefault(a => a.AccountName == account && a.AccountPwd == pwd);
            if (login != null)
            {
                Session["login"] = login; //Save user information in Session, will be used in Layout page
                return RedirectToAction("Index", "Home");  // go to Home controller,  index view page
            }
            else
            {
                TempData["msg"] = "Wrong username or password,Login failed!!";  // this information will pass to login.cshtml line25
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
                    
                    db.Account.Add(a);  // add to the database
                    db.SaveChanges();
                    TempData["msg"] = "Registering succeeded, please log in with your account and password!!";
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    TempData["msg"] = "Sheet verification failed!";  // relate to the TempData in Register.cshtml line47
                }
            }
            catch (Exception)
            {
                TempData["msg"] = "Registering failed!!";
            }
            return View();
        }

        public ActionResult Quite()
        {

            Session["login"] = null;
            return RedirectToAction("index", "home");

        }



    }
}