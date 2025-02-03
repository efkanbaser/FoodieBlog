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

        public async Task<Role> Get(Expression<Func<Role, bool>> filter, bool Tracking = false, params string[] includelist)
        {
            return await _repo.Get(filter, Tracking, includelist);
        }

        public async Task<List<Role>> GetAll(Expression<Func<Role, bool>> filter = null, Expression<Func<Role, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetAll(filter, orderby, sorted, Tracking, includelist);
        }

        public async Task<List<Role>> GetAllByActive(Expression<Func<Role, bool>> filter = null, Expression<Func<Role, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Aktif = true, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetAllByActive(filter, orderby, sorted, Aktif, Tracking, includelist);

        }

        public async Task<PagingResult<Role>> GetAllPaging(int Page, int PageSize, Expression<Func<Role, bool>> filter = null, Expression<Func<Role, object>> orderby = null, Sorted sorted = Sorted.ASC, params string[] includelist)
        {
            return await _repo.GetAllPaging(Page, PageSize, filter, orderby, sorted, includelist);
        }

        public async Task<Role> GetById(int Id, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetById(Id, Tracking, includelist);
        }

        public async Task<int> GetCount(Expression<Func<Role, bool>> filter = null, params string[] includelist)
        {
            return await _repo.GetCount(filter, includelist);
        }

        public async Task<Role> Insert(Role entity)
        {
            return await _repo.Insert(entity);
        }

        public async Task<Role> Update(Role entity)
        {
            return await _repo.Update(entity);
        }
    }
}
