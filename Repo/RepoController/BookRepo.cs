using Repo.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repo.Models;
using System.Data.Entity;
using System.Diagnostics;

namespace Repo.RepoController
{
    public class BookRepo : IBookRepo
    {
        private readonly ILibraryContext _db;
        public BookRepo(ILibraryContext db)
        {
            _db = db;
        }
        public void Actualize(Book book)
        {
            _db.Entry(book).State = EntityState.Modified;
        }

        public void AddBook(Book book)
        {
            _db.Book.Add(book);
        }

        public void DeleteBook(int id)
        {
            Book book = _db.Book.Find(id);
            _db.Book.Remove(book);
        }

        public IQueryable<Book> GetBook()
        {
            _db.Database.Log = message => Trace.WriteLine(message);
            return _db.Book.Include(b => b.Category);
        }

        public Book GetBookById(int id)
        {
            Book book = _db.Book.Find(id);
            return book;
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
        public String Availability(int id)
        {
            var result = (from av in _db.Book where av.Id == id select av.Available).Single();
            return result;
        }

        public void ChangeAvailability(int id, string av)
        {
            Book book = _db.Book.Find(id);
            book.Available = av;
            _db.Entry(book).State = EntityState.Modified;

        }
    }
}