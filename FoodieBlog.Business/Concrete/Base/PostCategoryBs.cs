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
    public class PostCategoryBs : IPostCategoryBs
    {
        private readonly IPostCategoryRepository _repo;

        public PostCategoryBs(IPostCategoryRepository repo)
        {
            _repo = repo;
        }

        public PostCategory Delete(PostCategory entity)
        {
            return _repo.Delete(entity);
        }

        public PostCategory DeleteById(int Id)
        {
            return _repo.DeleteById(Id);
        }

        public async Task<PostCategory> Get(Expression<Func<PostCategory, bool>> filter, bool Tracking = false, params string[] includelist)
        {
            return await _repo.Get(filter, Tracking, includelist);
        }

        public async Task<List<PostCategory>> GetAll(Expression<Func<PostCategory, bool>> filter = null, Expression<Func<PostCategory, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetAll(filter, orderby, sorted, Tracking, includelist);
        }

        public async Task<List<PostCategory>> GetAllByActive(Expression<Func<PostCategory, bool>> filter = null, Expression<Func<PostCategory, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Aktif = true, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetAllByActive(filter, orderby, sorted, Aktif, Tracking, includelist);

        }

        public async Task<PagingResult<PostCategory>> GetAllPaging(int Page, int PageSize, Expression<Func<PostCategory, bool>> filter = null, Expression<Func<PostCategory, object>> orderby = null, Sorted sorted = Sorted.ASC, params string[] includelist)
        {
            return await _repo.GetAllPaging(Page, PageSize, filter, orderby, sorted, includelist);
        }

        public async Task<PostCategory> GetById(int Id, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetById(Id, Tracking, includelist);
        }

        public async Task<int> GetCount(Expression<Func<PostCategory, bool>> filter = null, params string[] includelist)
        {
            return await _repo.GetCount(filter, includelist);
        }

        public async Task<PostCategory> Insert(PostCategory entity)
        {
            return await _repo.Insert(entity);
        }

        public async Task<PostCategory> Update(PostCategory entity)
        {
            return await _repo.Update(entity);
        }
    }
}
