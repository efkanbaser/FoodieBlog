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
            catch (Exception ex)
            {


                return null;
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

        public TEntity Get(Expression<Func<TEntity, bool>> filter, bool Tracking = false, params string[] includelist)
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
                    return query.SingleOrDefault(filter);
                else
                    return query.AsNoTracking().SingleOrDefault(filter);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Tracking = false, params string[] includelist)
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
                    query.Where(filter).AsNoTracking();
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


            return query.ToList();

        }

        public List<TEntity> GetAllByActive(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>> orderby = null, Sorted sorted = Sorted.ASC, bool Active = true, bool Tracking = false, params string[] includelist)
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

                return query.ToList();

            }
            catch (Exception)
            {

                throw;
            }




        }

        public PagingResult<TEntity> GetAllPaging(int Page, int PageSize, Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>> orderby = null, Sorted sorted = Sorted.ASC, params string[] includelist)
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
                query = query.Where(filter);
                int totalCount = query.Count();

                query = query.Skip((Page - 1) * PageSize).Take(PageSize);

                return new PagingResult<TEntity>(query.ToList(), TotalItemCount, totalCount);
            }
            catch (Exception)
            {

                throw;
            }



        }

        public TEntity GetById(int Id, bool Tracking = false, params string[] includelist)
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
                    return query.SingleOrDefault(x => x.Id == Id);
                else
                    return query.AsNoTracking().SingleOrDefault(x => x.Id == Id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCount(Expression<Func<TEntity, bool>> filter = null, params string[] includelist)
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

                return query.Where(filter).Count();
            }
            catch (Exception)
            {

                throw;
            }








        }

        public TEntity Insert(TEntity entity)
        {
            try
            {
                using TContext ctx = new TContext();

                // entity.OlusturulmaTarihi = DateTime.UtcNow;

                ctx.Set<TEntity>().Add(entity);
                ctx.SaveChanges();

                return entity;

            }
            catch (Exception)
            {

                throw;
            }

        }
       

        public TEntity Update(TEntity entity)
        {
            try
            {
                using TContext ctx = new TContext();
                //  entity.DegistirilmeTarihi = DateTime.UtcNow;
                ctx.Set<TEntity>().Attach(entity);
                ctx.Entry(entity).State = EntityState.Modified;
                ctx.SaveChanges();
                return entity;

            }
            catch (Exception)
            {
                throw;
            }
        }

     
    }
}
