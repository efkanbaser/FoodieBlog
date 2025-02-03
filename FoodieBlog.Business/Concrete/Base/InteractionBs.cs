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
    public class InteractionBs : IInteractionBs
    {
        private readonly IInteractionRepository _repo;

        public InteractionBs(IInteractionRepository repo)
        {
            _repo = repo;
        }

        public Interaction Delete(Interaction entity)
        {
            return _repo.Delete(entity);
        }

        public Interaction DeleteById(int Id)
        {
            return _repo.DeleteById(Id);
        }

        public async Task<Interaction> Get(Expression<Func<Interaction, bool>> filter, bool Tracking = false, params string[] includelist)
        {
            return await _repo.Get(filter, Tracking, includelist);
        }

        public async Task<List<Interaction>> GetAll(Expression<Func<Interaction, bool>> filter = null, Expression<Func<Interaction, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetAll(filter, orderby, sorted, Tracking, includelist);
        }

        public async Task<List<Interaction>> GetAllByActive(Expression<Func<Interaction, bool>> filter = null, Expression<Func<Interaction, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Aktif = true, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetAllByActive(filter, orderby, sorted, Aktif, Tracking, includelist);

        }

        public async Task<PagingResult<Interaction>> GetAllPaging(int Page, int PageSize, Expression<Func<Interaction, bool>> filter = null, Expression<Func<Interaction, object>> orderby = null, Sorted sorted = Sorted.ASC, params string[] includelist)
        {
            return await _repo.GetAllPaging(Page, PageSize, filter, orderby, sorted, includelist);
        }

        public async Task<Interaction> GetById(int Id, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetById(Id, Tracking, includelist);
        }

        public async Task<int> GetCount(Expression<Func<Interaction, bool>> filter = null, params string[] includelist)
        {
            return await _repo.GetCount(filter, includelist);
        }

        public async Task<Interaction> Insert(Interaction entity)
        {
            return await _repo.Insert(entity);
        }

        public async Task<Interaction> Update(Interaction entity)
        {
            return await _repo.Update(entity);
        }
    }
}
