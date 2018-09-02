using Repo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.IRepo
{
    public interface IOrderRepo
    {
        IQueryable<Order> GetOrder();
        Order GetOrderById(int id);
        void DeleteOrder(int id);
        void SaveChanges();
        void AddOrder(Order order);
        void Actualize(Order order);
        
    }
}
