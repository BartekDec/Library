using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repo.Models;
using System.Diagnostics;
using Repo.RepoController;
using Repo.IRepo;
using Microsoft.AspNet.Identity;

namespace Library.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepo _repo;
        private readonly ILibraryContext _context;
        private readonly IBookRepo _bookRepo;

        public OrderController(IOrderRepo repo, ILibraryContext context, IBookRepo bookRepo)
        {
            _repo = repo;
            _context = context;
            _bookRepo = bookRepo;
            
        }      

        // GET: Order
        public ActionResult Index()
        {
            if(User.IsInRole("Admin") || User.IsInRole("Librarian"))
            {
                var order = _repo.GetOrder();
                return View(order);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
        }
       
        public ActionResult MyOrder()
        {
            string userId = User.Identity.GetUserId();
            var order = _repo.GetOrder();
            order = order.Where(o => o.UserId == userId);
            return View(order);
        }

        [HttpPost]
        public ActionResult AvailabilityStatus(Order model)
        {
            var id = model.BookId;

            return View();
        }   
            
        // GET: Order/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _repo.GetOrderById((int)id);
            if (order == null)
            {
                return HttpNotFound();
            }
            else if (order.UserId != User.Identity.GetUserId() && (!(User.IsInRole("Admin") || User.IsInRole("Librarian"))))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(order);
        }

        // GET: Order/Create
        [Authorize]
        public ActionResult Create(bool? error)
        {


            ViewBag.BookId = new SelectList(_context.Book, "Id", "Title");

            if (error != null)
                ViewBag.Error = true;
          

            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderDate,ReceptionDate,ReturnDate,UserId,BookId")] Order order, FormCollection form)
        {

            if (ModelState.IsValid)
            {

                try
                {
                    
                    var DDLValue = Int32.Parse(Request.Form["BookId"]);               
                    var availability = _bookRepo.Availability(DDLValue);
                    if (availability == "No")
                    {

                        throw new Exception();
                    }
                    else
                    {
                        
                        order.UserId = User.Identity.GetUserId();
                        order.OrderDate = System.DateTime.Now.Date;
                        DateTime date = order.OrderDate;
                        order.ReceptionDate = date.AddDays(2);
                        order.ReturnDate = date.AddDays(30);
                        

                        _repo.AddOrder(order);
                        _repo.SaveChanges();
                        _bookRepo.ChangeAvailability(order.BookId, "No");
                        _bookRepo.SaveChanges();
                    }

                }
                catch
                {              
                    ViewBag.Error = true;                  
                    return RedirectToAction("Create", new { error = true});
                }
                //ViewBag.Error = false;

                
                    ViewBag.BookId = new SelectList(_context.Book, "Id", "Title", order.BookId);

               
            }
            return RedirectToAction("MyOrder");
            }
        

        // GET: Order/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _repo.GetOrderById((int)id);
            if (order == null)
            {
                return HttpNotFound();
            }
            else if( !(User.IsInRole("Admin") || User.IsInRole("Librarian") ))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        
            ViewBag.BookId = new SelectList(_context.Book, "Id", "Title", order.BookId);
            ViewBag.UserId = new SelectList(_context.User, "Id", "Email", order.UserId);

            return View(order);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderDate,ReceptionDate,ReturnDate,UserId,BookId")] Order order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repo.Actualize(order);
                    _repo.SaveChanges();
                }
                catch
                {
                    ViewBag.Error = true;
                    return View(order);
                }           
             
            }
            ViewBag.BookId = new SelectList(_context.Book, "Id", "Title", order.BookId);
            ViewBag.UserId = new SelectList(_context.User, "Id", "Email", order.UserId);

            ViewBag.Error = false;
            return View(order);
        }

        // GET: Order/Delete/5
        [Authorize]
        public ActionResult Delete(int? id, bool? error)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _repo.GetOrderById((int)id);
            if (order == null)
            {
                return HttpNotFound();
            }
            else if(order.UserId != User.Identity.GetUserId() && !(User.IsInRole("Admin") || User.IsInRole("Librarian")))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (error != null)
                ViewBag.Error = true;
            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            
            try
            {
                Order order = _repo.GetOrderById((int)id);

                _bookRepo.ChangeAvailability(order.BookId, "Yes");
                 _repo.DeleteOrder(id);
                _repo.SaveChanges();
                _bookRepo.SaveChanges();
            }
            catch
            {
                return RedirectToAction("Delete", new { id = id, error = true });
            }
           
            return RedirectToAction("MyOrder");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
