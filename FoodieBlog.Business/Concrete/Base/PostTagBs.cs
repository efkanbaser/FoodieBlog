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
    public class PostTagBs : IPostTagBs
    {
        private readonly IPostTagRepository _repo;

        public PostTagBs(IPostTagRepository repo)
        {
            _repo = repo;
        }

        public PostTag Delete(PostTag entity)
        {
            return _repo.Delete(entity);
        }

        public PostTag DeleteById(int Id)
        {
            return _repo.DeleteById(Id);
        }

        public async Task<PostTag> Get(Expression<Func<PostTag, bool>> filter, bool Tracking = false, params string[] includelist)
        {
            return await _repo.Get(filter, Tracking, includelist);
        }

        public async Task<List<PostTag>> GetAll(Expression<Func<PostTag, bool>> filter = null, Expression<Func<PostTag, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetAll(filter, orderby, sorted, Tracking, includelist);
        }

        public async Task<List<PostTag>> GetAllByActive(Expression<Func<PostTag, bool>> filter = null, Expression<Func<PostTag, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Aktif = true, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetAllByActive(filter, orderby, sorted, Aktif, Tracking, includelist);

        }

        public async Task<PagingResult<PostTag>> GetAllPaging(int Page, int PageSize, Expression<Func<PostTag, bool>> filter = null, Expression<Func<PostTag, object>> orderby = null, Sorted sorted = Sorted.ASC, params string[] includelist)
        {
            return await _repo.GetAllPaging(Page, PageSize, filter, orderby, sorted, includelist);
        }

        public async Task<PostTag> GetById(int Id, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetById(Id, Tracking, includelist);
        }

        public async Task<int> GetCount(Expression<Func<PostTag, bool>> filter = null, params string[] includelist)
        {
            return await _repo.GetCount(filter, includelist);
        }

        public async Task<PostTag> Insert(PostTag entity)
        {
            return await _repo.Insert(entity);
        }

        public async Task<PostTag> Update(PostTag entity)
        {
            return await _repo.Update(entity);
        }
    }
}
