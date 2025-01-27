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

        public Comment Get(Expression<Func<Comment, bool>> filter, bool Tracking = false, params string[] includelist)
        {
            return _repo.Get(filter, Tracking, includelist);
        }

        public List<Comment> GetAll(Expression<Func<Comment, bool>> filter = null, Expression<Func<Comment, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Tracking = false, params string[] includelist)
        {
            return _repo.GetAll(filter, orderby, sorted, Tracking, includelist);
        }

        public List<Comment> GetAllByActive(Expression<Func<Comment, bool>> filter = null, Expression<Func<Comment, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Aktif = true, bool Tracking = false, params string[] includelist)
        {
            return _repo.GetAllByActive(filter, orderby, sorted, Aktif, Tracking, includelist);

        }

        public PagingResult<Comment> GetAllPaging(int Page, int PageSize, Expression<Func<Comment, bool>> filter = null, Expression<Func<Comment, object>> orderby = null, Sorted sorted = Sorted.ASC, params string[] includelist)
        {
            return _repo.GetAllPaging(Page, PageSize, filter, orderby, sorted, includelist);
        }

        public Comment GetById(int Id, bool Tracking = false, params string[] includelist)
        {
            return _repo.GetById(Id, Tracking, includelist);
        }

        public int GetCount(Expression<Func<Comment, bool>> filter = null, params string[] includelist)
        {
            return _repo.GetCount(filter, includelist);
        }

        public Comment Insert(Comment entity)
        {
            return _repo.Insert(entity);
        }

        public Comment Update(Comment entity)
        {
            return _repo.Update(entity);
        }
    }
}
