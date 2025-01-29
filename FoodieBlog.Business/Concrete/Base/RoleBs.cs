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
    public class RoleBs : IRoleBs
    {
        private readonly IRoleRepository _repo;

        public RoleBs(IRoleRepository repo)
        {
            _repo = repo;
        }

        public Role Delete(Role entity)
        {
            return _repo.Delete(entity);
        }

        public Role DeleteById(int Id)
        {
            return _repo.DeleteById(Id);
        }

        public Role Get(Expression<Func<Role, bool>> filter, bool Tracking = false, params string[] includelist)
        {
            return _repo.Get(filter, Tracking, includelist);
        }

        public List<Role> GetAll(Expression<Func<Role, bool>> filter = null, Expression<Func<Role, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Tracking = false, params string[] includelist)
        {
            return _repo.GetAll(filter, orderby, sorted, Tracking, includelist);
        }

        public List<Role> GetAllByActive(Expression<Func<Role, bool>> filter = null, Expression<Func<Role, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Aktif = true, bool Tracking = false, params string[] includelist)
        {
            return _repo.GetAllByActive(filter, orderby, sorted, Aktif, Tracking, includelist);

        }

        public PagingResult<Role> GetAllPaging(int Page, int PageSize, Expression<Func<Role, bool>> filter = null, Expression<Func<Role, object>> orderby = null, Sorted sorted = Sorted.ASC, params string[] includelist)
        {
            return _repo.GetAllPaging(Page, PageSize, filter, orderby, sorted, includelist);
        }

        public Role GetById(int Id, bool Tracking = false, params string[] includelist)
        {
            return _repo.GetById(Id, Tracking, includelist);
        }

        public int GetCount(Expression<Func<Role, bool>> filter = null, params string[] includelist)
        {
            return _repo.GetCount(filter, includelist);
        }

        public Role Insert(Role entity)
        {
            return _repo.Insert(entity);
        }

        public Role Update(Role entity)
        {
            return _repo.Update(entity);
        }
    }
}
