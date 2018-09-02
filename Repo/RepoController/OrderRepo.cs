using Repo.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Repo.IRepo;

namespace Repo.RepoController
{
    public class OrderRepo : IOrderRepo
    {
        private readonly ILibraryContext _db;
        
        public OrderRepo(ILibraryContext db)
        {
            _db = db;
        }

        public IQueryable<Order> GetOrder()
        {
            _db.Database.Log = message => Trace.WriteLine(message);
            return _db.Order.Include(o => o.Book).Include(o => o.User);
        }

        public Order GetOrderById(int id)
        {
            Order order = _db.Order.Find(id);
            return order;
        }
       public void DeleteOrder(int id)
        {
            Order order = _db.Order.Find(id);
            _db.Order.Remove(order);
          
        }
        public void SaveChanges()
        {
            _db.SaveChanges();
        }
        public void AddOrder(Order order)
        {
            _db.Order.Add(order); 
        }
        public void Actualize(Order order)
        {
            _db.Entry(order).State = EntityState.Modified;
        }
    }
}