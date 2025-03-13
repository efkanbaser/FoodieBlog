using Infrastructure.Data.Abstract;
using Infrastructure.Entity;
using Infrastructure.Enumarations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Infrastructure.Data.Concrete.EntityFramework
{
    public class EfRepositoryBase<TEntity, TContext> : IRepository<TEntity>
        where TEntity : BaseEntity, new()
        where TContext : DbContext, new()
        // UnitOfWork
    {
     
        public TEntity Delete(TEntity entity)
        {
            try
            {
                using TContext ctx = new TContext();
                ctx.Set<TEntity>().Remove(entity);
                ctx.SaveChanges();
                return entity;
            }
            catch (Exception)
            {


                throw;
            }

        }

        public TEntity DeleteById(int Id)
        {
            try
            {
                using TContext ctx = new TContext();
                TEntity entity = ctx.Set<TEntity>().SingleOrDefault(x => x.Id == Id);
                if (entity != null)
                {
                    ctx.Set<TEntity>().Remove(entity);
                    ctx.SaveChanges();
                }
                return entity;

            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter, bool Tracking = false, params string[] includelist)
        {
            try
            {
                using TContext ctx = new TContext();
                IQueryable<TEntity> query = ctx.Set<TEntity>();
                if (includelist != null && includelist.Length > 0)
                {
                    for (int i = 0; i < includelist.Length; i++)
                    {
                        query = query.Include(includelist[i]);
                    }
                }
                if (Tracking)
                    return await query.SingleOrDefaultAsync(filter);
                else
                    return await query.AsNoTracking().SingleOrDefaultAsync(filter);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Tracking = false, params string[] includelist)
        {

            using TContext ctx = new TContext();
            IQueryable<TEntity> query = ctx.Set<TEntity>();

            if (Tracking)
            {
                if (filter != null)
                {
                    query = query.Where(filter);
                }
            }
            else
            {
                if (filter != null)
                {
                    query = query.Where(filter).AsNoTracking();
                }
                else
                {
                    query = query.AsNoTracking();
                }
            }

            if (includelist != null && includelist.Length > 0)
            {
                for (int i = 0; i < includelist.Length; i++)
                {
                    query = query.Include(includelist[i]);
                }
            }
            if (orderby != null)
            {
                if (sorted == Sorted.ASC)

                    query = query.OrderBy(orderby);
                else
                    query = query.OrderByDescending(orderby);
            }


            return await query.ToListAsync();

        }

        public async Task<List<TEntity>> GetAllByActive(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Active = true, bool Tracking = false, params string[] includelist)
        {
            try
            {
                using TContext ctx = new TContext();
                IQueryable<TEntity> query = ctx.Set<TEntity>();


                if (Tracking)
                {
                    if (filter != null)

                        query = query.Where(x => x.Active == Active).Where(filter);
                    else

                        query = query.Where(x => x.Active == Active);

                }
                else
                {
                    if (filter != null)

                        query = query.Where(x => x.Active == Active).Where(filter).AsNoTracking();
                    else
                        query = query.Where(x => x.Active == Active).AsNoTracking();


                }

                if (includelist != null && includelist.Length > 0)
                {
                    for (int i = 0; i < includelist.Length; i++)
                    {
                        query = query.Include(includelist[i]);
                    }
                }
                if (orderby != null)
                {
                    if (sorted == Sorted.ASC)

                        query = query.OrderBy(orderby);
                    else
                        query = query.OrderByDescending(orderby);
                }

                return await query.ToListAsync();

            }
            catch (Exception)
            {

                throw;
            }




        }

        public async Task<PagingResult<TEntity>> GetAllPaging(int Page, int PageSize, Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>> orderby = null, Sorted sorted = Sorted.ASC, params string[] includelist)
        {
            try
            {
                using TContext ctx = new TContext();
                IQueryable<TEntity> query = ctx.Set<TEntity>();

                if (includelist != null && includelist.Length > 0)
                {
                    for (int i = 0; i < includelist.Length; i++)
                    {
                        query = query.Include(includelist[i]);
                    }
                }

                if (orderby != null)
                {
                    if (sorted == Sorted.ASC)

                        query = query.OrderBy(orderby);
                    else
                        query = query.OrderByDescending(orderby);
                }
                int TotalItemCount = query.Count();

                // query.Take(10);// ilk 10 kayıt getir
                //query.Skip(20);// 20 kayıt atla
                if (filter != null)
                {
                    query = query.Where(filter);
                }
                //var queryTest = query.ToQueryString();

                int totalCount = query.Count();

                query = query.Skip((Page - 1) * PageSize).Take(PageSize);

                return new PagingResult<TEntity>(await query.ToListAsync(), TotalItemCount, totalCount);
            }
            catch (Exception)
            {

                throw;
            }



        }

        public async Task<TEntity> GetById(int Id, bool Tracking = false, params string[] includelist)
        {
            try
            {
                using TContext ctx = new TContext();
                IQueryable<TEntity> query = ctx.Set<TEntity>();
                if (includelist != null && includelist.Length > 0)
                {
                    for (int i = 0; i < includelist.Length; i++)
                    {
                        query = query.Include(includelist[i]);
                    }
                }
                if (Tracking)
                    return await query.SingleOrDefaultAsync(x => x.Id == Id);
                else
                    return await query.AsNoTracking().SingleOrDefaultAsync(x => x.Id == Id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> GetCount(Expression<Func<TEntity, bool>> filter = null, params string[] includelist)
        {
            try
            {
                using TContext ctx = new TContext();
                IQueryable<TEntity> query = ctx.Set<TEntity>();

                // Apply includes if any
                if (includelist != null && includelist.Length > 0)
                {
                    for (int i = 0; i < includelist.Length; i++)
                    {
                        query = query.Include(includelist[i]);
                    }
                }

                // Apply filter only if it's not null
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                // Get count
                return await query.CountAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            try
            {
                using TContext ctx = new TContext();

                // entity.OlusturulmaTarihi = DateTime.UtcNow;

                await ctx.Set<TEntity>().AddAsync(entity);
                await ctx.SaveChangesAsync();

                return entity;

            }
            catch (Exception)
            {

                throw;
            }

        }
       

        public async Task<TEntity> Update(TEntity entity)
        {
            try
            {
                using TContext ctx = new TContext();
                //  entity.DegistirilmeTarihi = DateTime.UtcNow;
                ctx.Set<TEntity>().Attach(entity);
                ctx.Entry(entity).State = EntityState.Modified;
                await ctx.SaveChangesAsync();
                return entity;

            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
