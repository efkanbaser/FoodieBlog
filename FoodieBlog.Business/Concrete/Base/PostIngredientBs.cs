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
    public class PostIngredientBs : IPostIngredientBs
    {
        private readonly IPostIngredientRepository _repo;

        public PostIngredientBs(IPostIngredientRepository repo)
        {
            _repo = repo;
        }

        public PostIngredient Delete(PostIngredient entity)
        {
            return _repo.Delete(entity);
        }

        public PostIngredient DeleteById(int Id)
        {
            return _repo.DeleteById(Id);
        }

        public async Task<PostIngredient> Get(Expression<Func<PostIngredient, bool>> filter, bool Tracking = false, params string[] includelist)
        {
            return await _repo.Get(filter, Tracking, includelist);
        }

        public async Task<List<PostIngredient>> GetAll(Expression<Func<PostIngredient, bool>> filter = null, Expression<Func<PostIngredient, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetAll(filter, orderby, sorted, Tracking, includelist);
        }

        public async Task<List<PostIngredient>> GetAllByActive(Expression<Func<PostIngredient, bool>> filter = null, Expression<Func<PostIngredient, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Aktif = true, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetAllByActive(filter, orderby, sorted, Aktif, Tracking, includelist);

        }

        public async Task<PagingResult<PostIngredient>> GetAllPaging(int Page, int PageSize, Expression<Func<PostIngredient, bool>> filter = null, Expression<Func<PostIngredient, object>> orderby = null, Sorted sorted = Sorted.ASC, params string[] includelist)
        {
            return await _repo.GetAllPaging(Page, PageSize, filter, orderby, sorted, includelist);
        }

        public async Task<PostIngredient> GetById(int Id, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetById(Id, Tracking, includelist);
        }

        public async Task<int> GetCount(Expression<Func<PostIngredient, bool>> filter = null, params string[] includelist)
        {
            return await _repo.GetCount(filter, includelist);
        }

        public async Task<PostIngredient> Insert(PostIngredient entity)
        {
            return await _repo.Insert(entity);
        }

        public async Task<PostIngredient> Update(PostIngredient entity)
        {
            return await _repo.Update(entity);
        }
    }
}
