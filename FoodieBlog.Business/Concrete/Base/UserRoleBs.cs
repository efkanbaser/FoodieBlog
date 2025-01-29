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
    public class UserRoleBs : IUserRoleBs
    {
        private readonly IUserRoleRepository _repo;

        public UserRoleBs(IUserRoleRepository repo)
        {
            _repo = repo;
        }

        public UserRole Delete(UserRole entity)
        {
            return _repo.Delete(entity);
        }

        public UserRole DeleteById(int Id)
        {
            return _repo.DeleteById(Id);
        }

        public UserRole Get(Expression<Func<UserRole, bool>> filter, bool Tracking = false, params string[] includelist)
        {
            return _repo.Get(filter, Tracking, includelist);
        }

        public List<UserRole> GetAll(Expression<Func<UserRole, bool>> filter = null, Expression<Func<UserRole, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Tracking = false, params string[] includelist)
        {
            return _repo.GetAll(filter, orderby, sorted, Tracking, includelist);
        }

        public List<UserRole> GetAllByActive(Expression<Func<UserRole, bool>> filter = null, Expression<Func<UserRole, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Aktif = true, bool Tracking = false, params string[] includelist)
        {
            return _repo.GetAllByActive(filter, orderby, sorted, Aktif, Tracking, includelist);

        }

        public PagingResult<UserRole> GetAllPaging(int Page, int PageSize, Expression<Func<UserRole, bool>> filter = null, Expression<Func<UserRole, object>> orderby = null, Sorted sorted = Sorted.ASC, params string[] includelist)
        {
            return _repo.GetAllPaging(Page, PageSize, filter, orderby, sorted, includelist);
        }

        public UserRole GetById(int Id, bool Tracking = false, params string[] includelist)
        {
            return _repo.GetById(Id, Tracking, includelist);
        }

        public int GetCount(Expression<Func<UserRole, bool>> filter = null, params string[] includelist)
        {
            return _repo.GetCount(filter, includelist);
        }

        public UserRole Insert(UserRole entity)
        {
            return _repo.Insert(entity);
        }

        public UserRole Update(UserRole entity)
        {
            return _repo.Update(entity);
        }
    }
}
