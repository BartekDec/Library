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
    public class CategoryController : Controller
    {
        private readonly ICategoryRepo  _repo;
        public CategoryController(ICategoryRepo repo)
        {
            _repo = repo;
        }

        // GET: Category
        public ActionResult Index()
        {
            var category = _repo.GetCategory();
            return View(category);
        }

        // GET: Category/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _repo.GetCategoryById((int)id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Category/Create
        [Authorize]
        public ActionResult Create()
        {
            if (User.IsInRole("Admin") || User.IsInRole("Librarian"))
            {
                
                return View();
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            
        }

        // POST: Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Category category)
        {
            
            
            if (ModelState.IsValid)
            {
                try
                {
                    _repo.AddCategory(category);
                    _repo.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(category);
                }

            }

            return View(category);
       

        }

        // GET: Category/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _repo.GetCategoryById((int)id);
            if (category == null)
            {
                return HttpNotFound();
            }else if (!(User.IsInRole("Admin") || User.IsInRole("Librarian")))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(category);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Category category)
        {
            if (ModelState.IsValid )
            {
                try
                {
                    _repo.Actualize(category);
                    _repo.SaveChanges();
                }
                catch
                {
                    return RedirectToAction("Index");
                }
               
               
            }
            return View(category);
        }

        // GET: Category/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _repo.GetCategoryById((int)id);
            if (category == null)
            {
                return HttpNotFound();
            }
            else if (!(User.IsInRole("Admin")))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.DeleteCategory(id);
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
