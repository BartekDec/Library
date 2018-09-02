using Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repo.IRepo
{
    public interface ICategoryRepo
    {
        IQueryable<Category> GetCategory();
        Category GetCategoryById(int id);
        void DeleteCategory(int id);
        void SaveChanges();
        void AddCategory(Category category);
        void Actualize(Category category);
    }
}