using Infrastructure.Entity;
using Infrastructure.Enumarations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Abstract
{
    public interface IUOWRepository<T>
        where T : BaseEntity, new()
    {
        List<T> GetAll(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Tracking = false, params string[] includelist);

        PagingResult<T> GetAllPaging(int Page, int PageSize, Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> orderby = null, Sorted sorted = Sorted.ASC, params string[] includelist);

        int GetCount(Expression<Func<T, bool>> filter = null, params string[] includelist);
        T Get(Expression<Func<T, bool>> filter, bool Tracking = false, params string[] includelist);
        List<T> GetAllByActive(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Aktif = true, bool Tracking = false, params string[] includelist);
        T GetById(int Id, bool Tracking = false, params string[] includelist);
        T Insert(T entity);
        T Delete(T entity);
        T DeleteById(int Id);
        T Update(T entity); 
        int SaveChanges();
    }
}
