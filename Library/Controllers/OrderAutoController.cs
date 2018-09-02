using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repo.Models;

namespace Library.Controllers
{
    public class OrderAutoController : Controller
    {
        private LibraryContext db = new LibraryContext();

        // GET: OrderAuto
        public ActionResult Index()
        {
            var order = db.Order.Include(o => o.Book).Include(o => o.User);
            return View(order.ToList());
        }

        // GET: OrderAuto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: OrderAuto/Create
        public ActionResult Create()
        {
            ViewBag.BookId = new SelectList(db.Book, "Id", "Isbn");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: OrderAuto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderDate,ReceptionDate,ReturnDate,UserId,BookId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Order.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookId = new SelectList(db.Book, "Id", "Isbn", order.BookId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", order.UserId);
            return View(order);
        }

        // GET: OrderAuto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookId = new SelectList(db.Book, "Id", "Isbn", order.BookId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", order.UserId);
            return View(order);
        }

        // POST: OrderAuto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderDate,ReceptionDate,ReturnDate,UserId,BookId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookId = new SelectList(db.Book, "Id", "Isbn", order.BookId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", order.UserId);
            return View(order);
        }

        // GET: OrderAuto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: OrderAuto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Order.Find(id);
            db.Order.Remove(order);
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
