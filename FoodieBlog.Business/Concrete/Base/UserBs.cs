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
    public class UserBs : IUserBs
    {
        private readonly IUserRepository _repo;

        public UserBs(IUserRepository repo)
        {
            _repo = repo;
        }

        public User Delete(User entity)
        {
            return _repo.Delete(entity);
        }

        public User DeleteById(int Id)
        {
            return _repo.DeleteById(Id);
        }

        public User Get(Expression<Func<User, bool>> filter, bool Tracking = false, params string[] includelist)
        {
            return _repo.Get(filter, Tracking, includelist);
        }

        public List<User> GetAll(Expression<Func<User, bool>> filter = null, Expression<Func<User, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Tracking = false, params string[] includelist)
        {
            return _repo.GetAll(filter, orderby, sorted, Tracking, includelist);
        }

        public List<User> GetAllByActive(Expression<Func<User, bool>> filter = null, Expression<Func<User, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Aktif = true, bool Tracking = false, params string[] includelist)
        {
            return _repo.GetAllByActive(filter, orderby, sorted, Aktif, Tracking, includelist);

        }

        public PagingResult<User> GetAllPaging(int Page, int PageSize, Expression<Func<User, bool>> filter = null, Expression<Func<User, object>> orderby = null, Sorted sorted = Sorted.ASC, params string[] includelist)
        {
            return _repo.GetAllPaging(Page, PageSize, filter, orderby, sorted, includelist);
        }

        public User GetById(int Id, bool Tracking = false, params string[] includelist)
        {
            return _repo.GetById(Id, Tracking, includelist);
        }

        public int GetCount(Expression<Func<User, bool>> filter = null, params string[] includelist)
        {
            return _repo.GetCount(filter, includelist);
        }

        public User Insert(User entity)
        {
            return _repo.Insert(entity);
        }

        public User Update(User entity)
        {
            return _repo.Update(entity);
        }
    }
}
