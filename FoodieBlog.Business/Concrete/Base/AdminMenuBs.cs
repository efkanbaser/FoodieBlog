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
    public class AdminMenuBs : IAdminMenuBs
    {
        private readonly IAdminMenuRepository _repo;

        public AdminMenuBs (IAdminMenuRepository repo)
        {
            _repo = repo;
        }

        public AdminMenu Delete(AdminMenu entity)
        {
            return _repo.Delete(entity);
        }

        public AdminMenu DeleteById(int Id)
        {
            return _repo.DeleteById(Id);
        }

        public async Task<AdminMenu>Get(Expression<Func<AdminMenu, bool>> filter, bool Tracking = false, params string[] includelist)
        {
            return await _repo.Get(filter, Tracking, includelist);
        }

        public async Task<List<AdminMenu>> GetAll(Expression<Func<AdminMenu, bool>> filter = null, Expression<Func<AdminMenu, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetAll(filter, orderby, sorted, Tracking, includelist);
        }

        public async Task<List<AdminMenu>> GetAllByActive(Expression<Func<AdminMenu, bool>> filter = null, Expression<Func<AdminMenu, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Aktif = true, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetAllByActive(filter, orderby, sorted, Aktif, Tracking, includelist);

        }

        public async Task<PagingResult<AdminMenu>> GetAllPaging(int Page, int PageSize, Expression<Func<AdminMenu, bool>> filter = null, Expression<Func<AdminMenu, object>> orderby = null, Sorted sorted = Sorted.ASC, params string[] includelist)
        {
            return await _repo.GetAllPaging(Page, PageSize, filter, orderby, sorted, includelist);
        }

        public async Task<AdminMenu> GetById(int Id, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetById(Id, Tracking, includelist);
        }

        public async Task<int> GetCount(Expression<Func<AdminMenu, bool>> filter = null, params string[] includelist)
        {
            return await _repo.GetCount(filter, includelist);
        }

        public async Task<AdminMenu> Insert(AdminMenu entity)
        {
            return await _repo.Insert(entity);
        }

        public async Task<AdminMenu> Update(AdminMenu entity)
        {
            return await _repo.Update(entity);
        }
    }
}
