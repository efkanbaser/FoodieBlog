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
    public class MenuAuthorizationBs : IMenuAuthorizationBs
    {
        private readonly IMenuAuthorizationRepository _repo;

        public MenuAuthorizationBs(IMenuAuthorizationRepository repo)
        {
            _repo = repo;
        }

        public MenuAuthorization Delete(MenuAuthorization entity)
        {
            return _repo.Delete(entity);
        }

        public MenuAuthorization DeleteById(int Id)
        {
            return _repo.DeleteById(Id);
        }

        public async Task<MenuAuthorization> Get(Expression<Func<MenuAuthorization, bool>> filter, bool Tracking = false, params string[] includelist)
        {
            return await _repo.Get(filter, Tracking, includelist);
        }

        public async Task<List<MenuAuthorization>> GetAll(Expression<Func<MenuAuthorization, bool>> filter = null, Expression<Func<MenuAuthorization, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetAll(filter, orderby, sorted, Tracking, includelist);
        }

        public async Task<List<MenuAuthorization>> GetAllByActive(Expression<Func<MenuAuthorization, bool>> filter = null, Expression<Func<MenuAuthorization, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Aktif = true, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetAllByActive(filter, orderby, sorted, Aktif, Tracking, includelist);

        }

        public async Task<PagingResult<MenuAuthorization>> GetAllPaging(int Page, int PageSize, Expression<Func<MenuAuthorization, bool>> filter = null, Expression<Func<MenuAuthorization, object>> orderby = null, Sorted sorted = Sorted.ASC, params string[] includelist)
        {
            return await _repo.GetAllPaging(Page, PageSize, filter, orderby, sorted, includelist);
        }

        public async Task<MenuAuthorization> GetById(int Id, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetById(Id, Tracking, includelist);
        }

        public async Task<int> GetCount(Expression<Func<MenuAuthorization, bool>> filter = null, params string[] includelist)
        {
            return await _repo.GetCount(filter, includelist);
        }

        public async Task<MenuAuthorization> Insert(MenuAuthorization entity)
        {
            return await _repo.Insert(entity);
        }

        public async Task<MenuAuthorization> Update(MenuAuthorization entity)
        {
            return await _repo.Update(entity);
        }
    }
}
