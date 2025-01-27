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

        public Interaction Get(Expression<Func<Interaction, bool>> filter, bool Tracking = false, params string[] includelist)
        {
            return _repo.Get(filter, Tracking, includelist);
        }

        public List<Interaction> GetAll(Expression<Func<Interaction, bool>> filter = null, Expression<Func<Interaction, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Tracking = false, params string[] includelist)
        {
            return _repo.GetAll(filter, orderby, sorted, Tracking, includelist);
        }

        public List<Interaction> GetAllByActive(Expression<Func<Interaction, bool>> filter = null, Expression<Func<Interaction, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Aktif = true, bool Tracking = false, params string[] includelist)
        {
            return _repo.GetAllByActive(filter, orderby, sorted, Aktif, Tracking, includelist);

        }

        public PagingResult<Interaction> GetAllPaging(int Page, int PageSize, Expression<Func<Interaction, bool>> filter = null, Expression<Func<Interaction, object>> orderby = null, Sorted sorted = Sorted.ASC, params string[] includelist)
        {
            return _repo.GetAllPaging(Page, PageSize, filter, orderby, sorted, includelist);
        }

        public Interaction GetById(int Id, bool Tracking = false, params string[] includelist)
        {
            return _repo.GetById(Id, Tracking, includelist);
        }

        public int GetCount(Expression<Func<Interaction, bool>> filter = null, params string[] includelist)
        {
            return _repo.GetCount(filter, includelist);
        }

        public Interaction Insert(Interaction entity)
        {
            return _repo.Insert(entity);
        }

        public Interaction Update(Interaction entity)
        {
            return _repo.Update(entity);
        }
    }
}
