using FoodieBlog.Business.Abstract;
using FoodieBlog.Data.Abstract;
using FoodieBlog.Model.Entity;
using Infrastructure.Entity;
using Infrastructure.Enumarations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FoodieBlog.Business.Concrete.Base
{
    public class CategoryBs : ICategoryBs
    {
        private readonly ICategoryRepository _repo;

        public CategoryBs(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public Category Delete(Category entity)
        {
            return _repo.Delete(entity);
        }

        public Category DeleteById(int Id)
        {
            return _repo.DeleteById(Id);
        }

        public async Task<Category> Get(Expression<Func<Category, bool>> filter, bool Tracking = false, params string[] includelist)
        {
            return await _repo.Get(filter, Tracking, includelist);
        }

        public async Task<List<Category>> GetAll(Expression<Func<Category, bool>> filter = null, Expression<Func<Category, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetAll(filter, orderby, sorted, Tracking, includelist);
        }

        public async Task<List<Category>> GetAllByActive(Expression<Func<Category, bool>> filter = null, Expression<Func<Category, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Aktif = true, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetAllByActive(filter, orderby, sorted, Aktif, Tracking, includelist);

        }

        public async Task<PagingResult<Category>> GetAllPaging(int Page, int PageSize, Expression<Func<Category, bool>> filter = null, Expression<Func<Category, object>> orderby = null, Sorted sorted = Sorted.ASC, params string[] includelist)
        {
            return await _repo.GetAllPaging(Page, PageSize, filter, orderby, sorted, includelist);
        }

        public async Task<Category> GetById(int Id, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetById(Id, Tracking, includelist);
        }

        public async Task<int> GetCount(Expression<Func<Category, bool>> filter = null, params string[] includelist)
        {
            return await _repo.GetCount(filter, includelist);
        }

        public async Task<Category> Insert(Category entity)
        {
            return await _repo.Insert(entity);
        }

        public async Task<Category> Update(Category entity)
        {
            return await _repo.Update(entity);
        }
    }
}
