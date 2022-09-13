using System;
using System.Linq.Expressions;
using AnimeMovie.Entites;
using Microsoft.EntityFrameworkCore;

namespace AnimeMovie.DataAccess
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableNoTracking { get; }

        IQueryable<TEntity> GetAll();
        int Count();
        IQueryable<TEntity> IncludeMany(params Expression<Func<TEntity, object>>[] includes);
        TEntity get(Expression<Func<TEntity, bool>> expression);

        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);

        bool Delete(Expression<Func<TEntity, bool>> expression);
    }
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly MovieDbContext movieDbContext;
        private DbSet<TEntity> _entites;
        public GenericRepository(MovieDbContext movieDb) => movieDbContext = movieDb;

        public int Count()
        {
            return Table.Count();
        }

        public TEntity Create(TEntity entity)
        {
            movieDbContext.Set<TEntity>().Add(entity);
            movieDbContext.SaveChanges();
            return entity;
        }

        public bool Delete(Expression<Func<TEntity, bool>> expression)
        {
            var model = get(expression);
            if (model != null)
            {
                movieDbContext.Set<TEntity>().Remove(model);
                movieDbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public TEntity get(Expression<Func<TEntity, bool>> expression)
        {
            return movieDbContext.Set<TEntity>().Where(expression).AsNoTracking().SingleOrDefault();
        }

        public IQueryable<TEntity> GetAll()
        {
            return movieDbContext.Set<TEntity>().AsNoTracking();
        }

        public IQueryable<TEntity> IncludeMany(params Expression<Func<TEntity, object>>[] includes)
        {
            return movieDbContext.Set<TEntity>().IncludeMultiple(includes);
        }

        public TEntity Update(TEntity entity)
        {
            movieDbContext.Set<TEntity>().Update(entity);
            movieDbContext.SaveChanges();
            return entity;
        }

        public IQueryable<TEntity> Table => Entities;
        protected virtual DbSet<TEntity> Entities => _entites ?? (_entites = movieDbContext.Set<TEntity>());
        public IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();
    }
}

