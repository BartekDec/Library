using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repo.Models;
using Repo.IRepo;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepo _repo;
        private readonly ILibraryContext _context;

        public BookController(IBookRepo repo, ILibraryContext context)
        {
            _repo = repo;
            _context = context;
        }

        // GET: Book
        public ActionResult Index()
        {
            var book = _repo.GetBook();
           // var book = db.Book.Include(b => b.Category);
            return View(book);
        }

        // GET: Book/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = _repo.GetBookById((int)id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Book/Create
        [Authorize]
        public ActionResult Create()
        {
            if (User.IsInRole("Admin") || User.IsInRole("Librarian"))
            {
                ViewBag.CategoryId = new SelectList(_context.Category, "Id", "Name");
                return View();
            }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }

   
        }

        // POST: Book/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Isbn,Title,Author,Page,Publisher,PublicationYear,Available,CategoryId")] Book book)
        {
            if (ModelState.IsValid)
            {
                
                    try
                    {
                        _repo.AddBook(book);
                        _repo.SaveChanges();
                       return RedirectToAction("Index"); 
                    }
                    catch
                    {
                        return View(book);
                    }
                 
            }
            ViewBag.CategoryId = new SelectList(_context.Category, "Id", "Name", book.CategoryId);

            return View(book);
        }

        // GET: Book/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = _repo.GetBookById((int)id);
            if (book == null)
            {
                return HttpNotFound();
            }
            else if(!(User.IsInRole("Admin") || User.IsInRole("Librarian")))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.CategoryId = new SelectList(_context.Category, "Id", "Name", book.CategoryId);
            return View(book);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Isbn,Title,Author,Page,Publisher,PublicationYear,Available,CategoryId")] Book book)
        {
            if (ModelState.IsValid)
            {
                if (User.IsInRole("Admin") || User.IsInRole("Librarian"))
                {


                    try
                    {
                        _repo.Actualize(book);
                        _repo.SaveChanges();
                    }
                    catch
                    {
                        return View(book);
                    }
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            ViewBag.CategoryId = new SelectList(_context.Category, "Id", "Name", book.CategoryId);

            return View(book);
        }

        // GET: Book/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = _repo.GetBookById((int)id);
            if (book == null)
            {
                return HttpNotFound();
            }
            else if(!User.IsInRole("Admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.DeleteBook(id);
            try
            {
                _repo.SaveChanges();
            }
            catch
            {
                return RedirectToAction("Delete");
            }
            return RedirectToAction("Index");
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
