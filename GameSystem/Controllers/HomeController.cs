using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using GameSystem.Models;

namespace GameSystem.Controllers
{
    public class HomeController : Controller
    {
        private GameDBEntities db = new GameDBEntities();
        public ActionResult Index()
        {
            
            ViewBag.list = db.Msg.Include("Account")       
                .OrderByDescending(o => o.MsgTime).ToList();  // ToList will imediately do the checking,  through ViewBag, transfer to the front-end  

            return View();
        }

        public ActionResult News()
        {
            //search in the database which the Msg type is News and transfer to the front-end page
            ViewBag.list = db.Msg.Include("Account")
                .Where(m => m.MsgType == "News")
                .OrderByDescending(o => o.MsgTime).ToList();
            return View();
        }

        public ActionResult Heros()
        {
            
            ViewBag.list = db.Msg.Include("Account")
                .Where(m => m.MsgType == "Heros")
                .OrderByDescending(o => o.MsgTime).ToList();
            return View();
        }

        public ActionResult Videos()
        {
            
            ViewBag.list = db.Msg.Include("Account")
                .Where(m => m.MsgType == "Videos and Pictures")
                .OrderByDescending(o => o.MsgTime).ToList();
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}