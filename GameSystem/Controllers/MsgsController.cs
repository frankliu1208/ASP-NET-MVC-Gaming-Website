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
    public class MsgsController : Controller
    {
        private GameDBEntities db = new GameDBEntities();

        // GET: Msgs
        public ActionResult Index()
        {
            var msg = db.Msg.Include(m => m.Account);
            return View(msg.ToList());
        }

        // GET: Msgs/Details/5
        public ActionResult Details(int? id)
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

        // GET: Msgs/Create
        public ActionResult Create()
        {
            ViewBag.AccountID = new SelectList(db.Account, "AccountID", "AccountName");
            return View();
        }

        // POST: Msgs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MsgID,MsgTitle,MsgText,MsgType,MsgPhoto,MsgTime,MsgHit,AccountID")] Msg msg)
        {
            if (ModelState.IsValid)
            {
                db.Msg.Add(msg);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountID = new SelectList(db.Account, "AccountID", "AccountName", msg.AccountID);
            return View(msg);
        }

        // GET: Msgs/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.AccountID = new SelectList(db.Account, "AccountID", "AccountName", msg.AccountID);
            return View(msg);
        }

        // POST: Msgs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MsgID,MsgTitle,MsgText,MsgType,MsgPhoto,MsgTime,MsgHit,AccountID")] Msg msg)
        {
            if (ModelState.IsValid)
            {
                db.Entry(msg).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountID = new SelectList(db.Account, "AccountID", "AccountName", msg.AccountID);
            return View(msg);
        }

        // GET: Msgs/Delete/5
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

        // POST: Msgs/Delete/5
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
