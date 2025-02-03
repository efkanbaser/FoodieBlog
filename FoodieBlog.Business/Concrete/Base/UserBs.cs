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

        public async Task<User> Get(Expression<Func<User, bool>> filter, bool Tracking = false, params string[] includelist)
        {
            return await _repo.Get(filter, Tracking, includelist);
        }

        public async Task<List<User>> GetAll(Expression<Func<User, bool>> filter = null, Expression<Func<User, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetAll(filter, orderby, sorted, Tracking, includelist);
        }

        public async Task<List<User>> GetAllByActive(Expression<Func<User, bool>> filter = null, Expression<Func<User, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Aktif = true, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetAllByActive(filter, orderby, sorted, Aktif, Tracking, includelist);

        }

        public async Task<PagingResult<User>> GetAllPaging(int Page, int PageSize, Expression<Func<User, bool>> filter = null, Expression<Func<User, object>> orderby = null, Sorted sorted = Sorted.ASC, params string[] includelist)
        {
            return await _repo.GetAllPaging(Page, PageSize, filter, orderby, sorted, includelist);
        }

        public async Task<User> GetById(int Id, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetById(Id, Tracking, includelist);
        }

        public async Task<int> GetCount(Expression<Func<User, bool>> filter = null, params string[] includelist)
        {
            return await _repo.GetCount(filter, includelist);
        }

        public async Task<User> Insert(User entity)
        {
            return await _repo.Insert(entity);
        }

        public async Task<User> Update(User entity)
        {
            return await _repo.Update(entity);
        }
    }
}
