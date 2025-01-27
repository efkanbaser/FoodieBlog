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
    public class EfRepositoryBaseUOW<TEntity> : IUOWRepository<TEntity>
        where TEntity : BaseEntity, new()

        // UnitOfWork
    {

        DbContext ctx;
        public EfRepositoryBaseUOW(DbContext _ctx)
        {
            ctx = _ctx;
        }

        public TEntity Delete(TEntity entity)
        {
            try
            {

                ctx.Set<TEntity>().Remove(entity);

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

                TEntity entity = ctx.Set<TEntity>().SingleOrDefault(x => x.Id == Id);
                if (entity != null)
                {
                    ctx.Set<TEntity>().Remove(entity);

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


                // entity.OlusturulmaTarihi = DateTime.UtcNow;

                ctx.Set<TEntity>().Add(entity);


                return entity;

            }
            catch (Exception)
            {

                throw;
            }

        }



        public int SaveChanges()
        {
            ctx.Database.BeginTransaction();
            int sonuc = 0;
            try
            {
                sonuc = ctx.SaveChanges();
                ctx.Database.CommitTransaction();

            }
            catch (Exception)
            {
                ctx.Database.RollbackTransaction();
                sonuc = 0;
            }


            return sonuc;


        }




        public TEntity Update(TEntity entity)
        {
            try
            {

                //  entity.DegistirilmeTarihi = DateTime.UtcNow;
                ctx.Set<TEntity>().Attach(entity);
                ctx.Entry(entity).State = EntityState.Modified;

                return entity;

            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
