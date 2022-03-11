using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GameSystem.Models;

namespace GameSystem.Controllers
{
    public class MsgController : Controller
    {
        private GameDBEntities db = new GameDBEntities();

        // GET: get the list of all information
        public ActionResult Index()
        {
            var msg = db.Msg.Include(m => m.Account);
            return View(msg.ToList());
        }

        // GET: Msg/Details/5  search for information details through id
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Msg msg = db.Msg.Find(id);
            //click+1
            msg.MsgHit++;
            db.Entry(msg).State = EntityState.Modified;
            db.SaveChanges();
            if (msg == null)
            {
                return HttpNotFound();
            }
            return View(msg);
        }

        // GET: Msg/Create add new info
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(Msg msg, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                //check if the user has been logged on, add new info after the log-on
                if (Session["Login"] != null)
                {
                    if (photo != null)
                    {
                        msg.MsgPhoto = "/img/" + photo.FileName;
                        photo.SaveAs(Server.MapPath(msg.MsgPhoto));
                    }
                    msg.MsgHit = 0; //default click number is 0
                    msg.MsgTime = System.DateTime.Now; //current time for the information registering
                    msg.AccountID = ((Account)Session["Login"]).AccountID;//writer is the currently-logged-on user
                    try
                    {
                        db.Msg.Add(msg);
                        db.SaveChanges();
                        return RedirectToAction("Index");//redirect to the Index method
                    }
                    catch (Exception)
                    {
                        TempData["msg"] = "adding failed!";
                    }
                }
                else TempData["msg"] = "Please log on first!";
            }
            else TempData["msg"] = "Sheet verification is failed!";
            return View(msg);
        }



        // GET: Msg/Edit/5 modify the information
        public ActionResult Edit(int? id)
        {
            //根据前端传递的msgid,进行数据的查询，并将数据传递到前端页面
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Msg msg = db.Msg.Find(id);
            if (msg == null) return HttpNotFound();
            return View(msg);
        }


        [HttpPost]
        public ActionResult Edit(Msg msg, HttpPostedFileBase photo)  // 自动装载，与edit.cshtml中51行的name属性保持一致 
        {
            if (ModelState.IsValid)
            {
                if (photo != null)
                {
                    msg.MsgPhoto = "/img/" + photo.FileName;
                    photo.SaveAs(Server.MapPath(msg.MsgPhoto));  // upload the photo to the database
                }
                try
                {
                    db.Entry(msg).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["msg"] = "Modifying is failed!";
                }
            }
            else TempData["msg"] = "Sheet verification is failed!";
            return View(msg);
        }

        // GET: Msg/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Msg msg = db.Msg.Find(id);
            if (msg == null)
            {
                return HttpNotFound();
            }
            return View(msg);
        }

        // POST: Msg/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Msg msg = db.Msg.Find(id);
            db.Msg.Remove(msg);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
