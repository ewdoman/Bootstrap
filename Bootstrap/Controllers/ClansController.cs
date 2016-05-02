using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Quartermaster;
using Bootstrap.Models;

namespace Bootstrap.Controllers
{
    public class ClansController : Controller
    {
        private ClanDBContext db = new ClanDBContext();

        // GET: Clans
        public ActionResult Index()
        {
            return View(db.Clans.ToList());
        }

        // GET?: SEARCH: search clans battle history 
        public ActionResult Search()
        {  
            return View();
        }

        // GET?: RESULTS: result page controller
        public ActionResult Results()
        {
            return View();
        }

        // GET: Clans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clan clan = db.Clans.Find(id);
            if (clan == null)
            {
                return HttpNotFound();
            }
            return View(clan);
        }

        // GET: Clans/Create
        public ActionResult Create()
        {
            //System.Diagnostics.Debug.WriteLine("CREATE NORMAL");
            ViewBag.ProgList = new List<string>();
            return View();
        }

        // POST: Clans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Gamertag,Company")] Clan clan)
        {
            List<string> gamertags = Quartermaster.Quartermaster.GetGamertagsForCompany(clan.Company);
            foreach (string member in gamertags)
            {
                System.Diagnostics.Debug.WriteLine(member);
            }

            ViewBag.ProgList = gamertags;
            ViewBag.Message = "Company: " + clan.Company;
            //System.Diagnostics.Debug.WriteLine("CREATE OVERLOAD");

            foreach (string member in gamertags)
            {
                //TODO: needs some error checking, and dup checking 
                clan.Gamertag = member;
                db.Clans.Add(clan);
                db.SaveChanges();
            }
            //if (ModelState.IsValid)
            //{
            //    db.Clans.Add(clan);
            //    db.SaveChanges();
            return RedirectToAction("Index");
            //}

            //return View(clan);
            //return View();
        }

        // GET: Clans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clan clan = db.Clans.Find(id);
            if (clan == null)
            {
                return HttpNotFound();
            }
            return View(clan);
        }

        // POST: Clans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Gamertag,Company")] Clan clan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clan);
        }

        // GET: Clans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clan clan = db.Clans.Find(id);
            if (clan == null)
            {
                return HttpNotFound();
            }
            return View(clan);
        }

        // POST: Clans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clan clan = db.Clans.Find(id);
            db.Clans.Remove(clan);
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
