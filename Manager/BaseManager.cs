using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class BaseManager <T> where T : class
    {
        AppDbContext DbContext { get; }
        public BaseManager(AppDbContext _dbContext)
        {
            DbContext = _dbContext;
        }
        public IQueryable<T> GetAll() {
            return DbContext.Set<T>().AsQueryable();
        }

        public async Task<T> GetByID(object _ID)
        {
            var entity = await DbContext.Set<T>().FindAsync(_ID);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity of type {typeof(T)} with ID {_ID} was not found.");
            }
            return entity;
        }

        public bool Delete(T _entity)
        {
            try 
            {
                DbContext.Remove(_entity);
                DbContext.SaveChanges();
                return true;

            } catch(Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                return false;
            }
            
            
        }

        public bool Update(T _entity)
        {
            try
            {
                DbContext.Update(_entity);
                DbContext.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                return false;
            }

        }
        public bool Add(T e)
        {
            try
            {
                DbContext.Add(e);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                return false;
            }

        }

        public IQueryable<T> Filter(Expression<Func<T, bool>> expression, string columnOrder = "Id", int categoryID = 0,
            double price = 0, string productName = "",
            bool IsAscending = false, int PageSize = 4, int PageNumber = 1)
        {
            IQueryable<T> query = DbContext.Set<T>().AsQueryable();

            if (expression != null)
                query = query.Where(expression);


            if (!string.IsNullOrEmpty(columnOrder))
                query = query.OrderBy(columnOrder, IsAscending);


            if (PageNumber < 0)
            {
                PageNumber = 1;
            }
            if (PageSize < 0)
            {
                PageSize = 5;
            }

            if (query.Count() < PageSize)
            {
                PageSize = query.Count();
                PageNumber = 1;
            }

            int Skip = (PageNumber - 1) * PageSize;
            query = query.Skip(Skip).Take(PageSize);

            return query;
        }

    }
}
