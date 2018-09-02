using Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.IRepo
{
    public interface IBookRepo
    {
        IQueryable<Book> GetBook();
        Book GetBookById(int id);
        void DeleteBook(int id);
        void SaveChanges();
        void AddBook(Book book);
        void Actualize(Book book);
        String Availability(int id);
        void ChangeAvailability(int id, string av);
    }
}
