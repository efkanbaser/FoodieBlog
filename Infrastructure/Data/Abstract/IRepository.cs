using Infrastructure.Entity;
using Infrastructure.Enumarations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Abstract
{
    public interface IRepository<T>
        where T : BaseEntity, new()
    {
        Task<List<T>> GetAll(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Tracking = false, params string[] includelist);

        Task<PagingResult<T>> GetAllPaging(int Page, int PageSize, Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> orderby = null, Sorted sorted = Sorted.ASC, params string[] includelist);

        Task<int> GetCount(Expression<Func<T, bool>> filter = null, params string[] includelist);
        Task<T> Get(Expression<Func<T, bool>> filter, bool Tracking = false, params string[] includelist);
        Task<List<T>> GetAllByActive(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Aktif = true, bool Tracking = false, params string[] includelist);
        Task<T> GetById(int Id, bool Tracking = false, params string[] includelist);
        Task<T> Insert(T entity);
        T Delete(T entity);
        T DeleteById(int Id);
        Task<T> Update(T entity);
    }
}
