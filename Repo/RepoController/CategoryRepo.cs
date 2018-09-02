using Repo.IRepo;
using Repo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Repo.RepoController
{
    public class CategoryRepo :  ICategoryRepo
    {
        private readonly ILibraryContext _db = new LibraryContext();
        public CategoryRepo(ILibraryContext db)
        {
            _db = db;
        }

     

       public IQueryable<Category> GetCategory()
        {
            _db.Database.Log = message => Trace.WriteLine(message);
            return _db.Category.AsNoTracking();
        }
     
       public Category GetCategoryById(int id)
        {
            Category category = _db.Category.Find(id);
            return category;
        }
        public void DeleteCategory(int id)
        {
            Category category = _db.Category.Find(id);
            _db.Category.Remove(category);
        }
        public void AddCategory(Category category)
        {
            _db.Category.Add(category);
        }
        public void SaveChanges()
        {
            _db.SaveChanges();
        }
        public void Actualize(Category category)
        {
            _db.Entry(category).State = EntityState.Modified;
        }
    }
}