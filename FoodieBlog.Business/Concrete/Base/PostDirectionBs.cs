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
    public class PostDirectionBs : IPostDirectionBs
    {
        private readonly IPostDirectionRepository _repo;

        public PostDirectionBs(IPostDirectionRepository repo)
        {
            _repo = repo;
        }

        public PostDirection Delete(PostDirection entity)
        {
            return _repo.Delete(entity);
        }

        public PostDirection DeleteById(int Id)
        {
            return _repo.DeleteById(Id);
        }

        public async Task<PostDirection> Get(Expression<Func<PostDirection, bool>> filter, bool Tracking = false, params string[] includelist)
        {
            return await _repo.Get(filter, Tracking, includelist);
        }

        public async Task<List<PostDirection>> GetAll(Expression<Func<PostDirection, bool>> filter = null, Expression<Func<PostDirection, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetAll(filter, orderby, sorted, Tracking, includelist);
        }

        public async Task<List<PostDirection>> GetAllByActive(Expression<Func<PostDirection, bool>> filter = null, Expression<Func<PostDirection, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Aktif = true, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetAllByActive(filter, orderby, sorted, Aktif, Tracking, includelist);

        }

        public async Task<PagingResult<PostDirection>> GetAllPaging(int Page, int PageSize, Expression<Func<PostDirection, bool>> filter = null, Expression<Func<PostDirection, object>> orderby = null, Sorted sorted = Sorted.ASC, params string[] includelist)
        {
            return await _repo.GetAllPaging(Page, PageSize, filter, orderby, sorted, includelist);
        }

        public async Task<PostDirection> GetById(int Id, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetById(Id, Tracking, includelist);
        }

        public async Task<int> GetCount(Expression<Func<PostDirection, bool>> filter = null, params string[] includelist)
        {
            return await _repo.GetCount(filter, includelist);
        }

        public async Task<PostDirection> Insert(PostDirection entity)
        {
            return await _repo.Insert(entity);
        }

        public async Task<PostDirection> Update(PostDirection entity)
        {
            return await _repo.Update(entity);
        }
    }
}
