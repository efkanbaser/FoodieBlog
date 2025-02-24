﻿using FoodieBlog.Business.Abstract;
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
    public class TagBs : ITagBs
    {
        private readonly ITagRepository _repo;

        public TagBs(ITagRepository repo)
        {
            _repo = repo;
        }

        public Tag Delete(Tag entity)
        {
            return _repo.Delete(entity);
        }

        public Tag DeleteById(int Id)
        {
            return _repo.DeleteById(Id);
        }

        public async Task<Tag> Get(Expression<Func<Tag, bool>> filter, bool Tracking = false, params string[] includelist)
        {
            return await _repo.Get(filter, Tracking, includelist);
        }

        public async Task<List<Tag>> GetAll(Expression<Func<Tag, bool>> filter = null, Expression<Func<Tag, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetAll(filter, orderby, sorted, Tracking, includelist);
        }

        public async Task<List<Tag>> GetAllByActive(Expression<Func<Tag, bool>> filter = null, Expression<Func<Tag, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Aktif = true, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetAllByActive(filter, orderby, sorted, Aktif, Tracking, includelist);

        }

        public async Task<PagingResult<Tag>> GetAllPaging(int Page, int PageSize, Expression<Func<Tag, bool>> filter = null, Expression<Func<Tag, object>> orderby = null, Sorted sorted = Sorted.ASC, params string[] includelist)
        {
            return await _repo.GetAllPaging(Page, PageSize, filter, orderby, sorted, includelist);
        }

        public async Task<Tag> GetById(int Id, bool Tracking = false, params string[] includelist)
        {
            return await _repo.GetById(Id, Tracking, includelist);
        }

        public async Task<int> GetCount(Expression<Func<Tag, bool>> filter = null, params string[] includelist)
        {
            return await _repo.GetCount(filter, includelist);
        }

        public async Task<Tag> Insert(Tag entity)
        {
            return await _repo.Insert(entity);
        }

        public async Task<Tag> Update(Tag entity)
        {
            return await _repo.Update(entity);
        }
    }
}
