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
    public class CommentBs : ICommentBs
    {
        private readonly ICommentRepository _repo;

        public CommentBs(ICommentRepository repo)
        {
            _repo = repo;
        }

        public Comment Delete(Comment entity)
        {
            return _repo.Delete(entity);
        }

        public Comment DeleteById(int Id)
        {
            return _repo.DeleteById(Id);
        }

        public async Task<Comment> Get(Expression<Func<Comment, bool>> filter, bool Tracking = false, params string[] includelist)
        {
            return await _repo.Get(filter, Tracking, includelist);
        }

        public async Task<List<Comment>> GetAll(Expression<Func<Comment, bool>> filter = null, Expression<Func<Comment, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetAll(filter, orderby, sorted, Tracking, includelist);
        }

        public async Task<List<Comment>> GetAllByActive(Expression<Func<Comment, bool>> filter = null, Expression<Func<Comment, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Aktif = true, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetAllByActive(filter, orderby, sorted, Aktif, Tracking, includelist);

        }

        public async Task<PagingResult<Comment>> GetAllPaging(int Page, int PageSize, Expression<Func<Comment, bool>> filter = null, Expression<Func<Comment, object>> orderby = null, Sorted sorted = Sorted.ASC, params string[] includelist)
        {
            return await _repo.GetAllPaging(Page, PageSize, filter, orderby, sorted, includelist);
        }

        public async Task<Comment> GetById(int Id, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetById(Id, Tracking, includelist);
        }

        public async Task<int> GetCount(Expression<Func<Comment, bool>> filter = null, params string[] includelist)
        {
            return await _repo.GetCount(filter, includelist);
        }

        public async Task<Comment> Insert(Comment entity)
        {
            return await _repo.Insert(entity);
        }

        public async Task<Comment> Update(Comment entity)
        {
            return await _repo.Update(entity);
        }
    }
}
