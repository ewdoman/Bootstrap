using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bootstrap.Models;

namespace Bootstrap.Controllers
{
    public class SpartansController : Controller
    {
        private Entities db = new Entities();

        // GET: Spartans
        public async Task<ActionResult> Index()
        {
            return View(await db.Spartans.ToListAsync());
        }

        // GET: Spartans/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spartan spartan = await db.Spartans.FindAsync(id);
            if (spartan == null)
            {
                return HttpNotFound();
            }
            return View(spartan);
        }

        // GET: Spartans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Spartans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,gamertag,company")] Spartan spartan)
        {
            if (ModelState.IsValid)
            {
                db.Spartans.Add(spartan);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(spartan);
        }

        // GET: Spartans/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spartan spartan = await db.Spartans.FindAsync(id);
            if (spartan == null)
            {
                return HttpNotFound();
            }
            return View(spartan);
        }

        // POST: Spartans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,gamertag,company")] Spartan spartan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(spartan).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(spartan);
        }

        // GET: Spartans/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spartan spartan = await db.Spartans.FindAsync(id);
            if (spartan == null)
            {
                return HttpNotFound();
            }
            return View(spartan);
        }

        // POST: Spartans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Spartan spartan = await db.Spartans.FindAsync(id);
            db.Spartans.Remove(spartan);
            await db.SaveChangesAsync();
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
