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

        public PostTag Get(Expression<Func<PostTag, bool>> filter, bool Tracking = false, params string[] includelist)
        {
            return _repo.Get(filter, Tracking, includelist);
        }

        public List<PostTag> GetAll(Expression<Func<PostTag, bool>> filter = null, Expression<Func<PostTag, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Tracking = false, params string[] includelist)
        {
            return _repo.GetAll(filter, orderby, sorted, Tracking, includelist);
        }

        public List<PostTag> GetAllByActive(Expression<Func<PostTag, bool>> filter = null, Expression<Func<PostTag, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Aktif = true, bool Tracking = false, params string[] includelist)
        {
            return _repo.GetAllByActive(filter, orderby, sorted, Aktif, Tracking, includelist);

        }

        public PagingResult<PostTag> GetAllPaging(int Page, int PageSize, Expression<Func<PostTag, bool>> filter = null, Expression<Func<PostTag, object>> orderby = null, Sorted sorted = Sorted.ASC, params string[] includelist)
        {
            return _repo.GetAllPaging(Page, PageSize, filter, orderby, sorted, includelist);
        }

        public PostTag GetById(int Id, bool Tracking = false, params string[] includelist)
        {
            return _repo.GetById(Id, Tracking, includelist);
        }

        public int GetCount(Expression<Func<PostTag, bool>> filter = null, params string[] includelist)
        {
            return _repo.GetCount(filter, includelist);
        }

        public PostTag Insert(PostTag entity)
        {
            return _repo.Insert(entity);
        }

        public PostTag Update(PostTag entity)
        {
            return _repo.Update(entity);
        }
    }
}
