using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbSet<T> entities;
        protected ApplicationDbContext applicationDbContext;
        public Repository(ApplicationDbContext applicationDbContext)
        {
           // entities = new List<T>();
            this.applicationDbContext = applicationDbContext;
            this.entities = applicationDbContext.Set<T>();
        }
        public void Add(T entity)
        {
            entities.Add(entity);
        }

        public void Delete(T entity)
        {
            entities.Remove(entity);
        }

        public T Get(Func<T, bool> expression)
        {
            return entities.FirstOrDefault(expression);
        }

        public IEnumerable<T> GetAll(Func<T, bool>? expression = null)
        {
            if(expression!=null)
            {
                return entities.Where(expression);
            }
            return entities;
        }
    }
}
