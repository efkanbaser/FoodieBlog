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
    public class PostBs : IPostBs
    {
        private readonly IPostRepository _repo;

        public PostBs(IPostRepository repo)
        {
            _repo = repo;
        }

        public Post Delete(Post entity)
        {
            return _repo.Delete(entity);
        }

        public Post DeleteById(int Id)
        {
            return _repo.DeleteById(Id);
        }

        public async Task<Post> Get(Expression<Func<Post, bool>> filter, bool Tracking = false, params string[] includelist)
        {
            return await _repo.Get(filter, Tracking, includelist);
        }

        public async Task<List<Post>> GetAll(Expression<Func<Post, bool>> filter = null, Expression<Func<Post, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetAll(filter, orderby, sorted, Tracking, includelist);
        }

        public async Task<List<Post>> GetAllByActive(Expression<Func<Post, bool>> filter = null, Expression<Func<Post, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Aktif = true, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetAllByActive(filter, orderby, sorted, Aktif, Tracking, includelist);

        }

        public async Task<PagingResult<Post>> GetAllPaging(int Page, int PageSize, Expression<Func<Post, bool>> filter = null, Expression<Func<Post, object>> orderby = null, Sorted sorted = Sorted.ASC, params string[] includelist)
        {
            return await _repo.GetAllPaging(Page, PageSize, filter, orderby, sorted, includelist);
        }

        public async Task<Post> GetById(int Id, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetById(Id, Tracking, includelist);
        }

        public async Task<int> GetCount(Expression<Func<Post, bool>> filter = null, params string[] includelist)
        {
            return await _repo.GetCount(filter, includelist);
        }

        public async Task<Post> Insert(Post entity)
        {
            return await _repo.Insert(entity);
        }

        public async Task<Post> Update(Post entity)
        {
            return await _repo.Update(entity);
        }
    }
}
