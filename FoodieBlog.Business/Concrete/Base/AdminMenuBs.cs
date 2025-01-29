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

        public AdminMenuBs(IAdminMenuRepository repo)
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

        public AdminMenu Get(Expression<Func<AdminMenu, bool>> filter, bool Tracking = false, params string[] includelist)
        {
            return _repo.Get(filter, Tracking, includelist);
        }

        public List<AdminMenu> GetAll(Expression<Func<AdminMenu, bool>> filter = null, Expression<Func<AdminMenu, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Tracking = false, params string[] includelist)
        {
            return _repo.GetAll(filter, orderby, sorted, Tracking, includelist);
        }

        public List<AdminMenu> GetAllByActive(Expression<Func<AdminMenu, bool>> filter = null, Expression<Func<AdminMenu, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Aktif = true, bool Tracking = false, params string[] includelist)
        {
            return _repo.GetAllByActive(filter, orderby, sorted, Aktif, Tracking, includelist);

        }

        public PagingResult<AdminMenu> GetAllPaging(int Page, int PageSize, Expression<Func<AdminMenu, bool>> filter = null, Expression<Func<AdminMenu, object>> orderby = null, Sorted sorted = Sorted.ASC, params string[] includelist)
        {
            return _repo.GetAllPaging(Page, PageSize, filter, orderby, sorted, includelist);
        }

        public AdminMenu GetById(int Id, bool Tracking = false, params string[] includelist)
        {
            return _repo.GetById(Id, Tracking, includelist);
        }

        public int GetCount(Expression<Func<AdminMenu, bool>> filter = null, params string[] includelist)
        {
            return _repo.GetCount(filter, includelist);
        }

        public AdminMenu Insert(AdminMenu entity)
        {
            return _repo.Insert(entity);
        }

        public AdminMenu Update(AdminMenu entity)
        {
            return _repo.Update(entity);
        }
    }
}
