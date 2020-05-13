using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace CRMFocus.Common
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public TEntity GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public int GetTotalCount()
        {
            return _context.Set<TEntity>().Count();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        public TEntity Create(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            ModifiedTimestamps();
            _context.SaveChanges();

            return entity;
        }

        public void CreateBulk(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
            _context.SaveChanges();
        }

        public TEntity Update(TEntity entity)
        {
            ModifiedTimestamps();
            _context.SaveChanges();

            return entity;
        }

        public TEntity Delete(TEntity entity)
        {
            var entities = _context.ChangeTracker.Entries()
               .Where(e => e.Entity is BaseClass && e.State == EntityState.Unchanged);

            foreach (var item in entities)
            {
                if (item.State == EntityState.Unchanged)
                {
                    ((BaseClass)item.Entity).IsDeleted = true;
                    ((BaseClass)item.Entity).LastModifiedTime = DateTime.Now;
                    ((BaseClass)item.Entity).LastModifiedBy = GetUserName();
                }
            }
            
            _context.SaveChanges();

            return entity;
        }

        private void ModifiedTimestamps()
        {
            var entities = _context.ChangeTracker.Entries()
                .Where(e => e.Entity is BaseClass && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseClass)entity.Entity).CreatedTime = DateTime.Now;
                    ((BaseClass)entity.Entity).CreatedBy = GetUserName();
                }
                else if (entity.State == EntityState.Modified)
                {
                    ((BaseClass)entity.Entity).LastModifiedTime = DateTime.Now;
                    ((BaseClass)entity.Entity).LastModifiedBy = GetUserName();
                }
            }
        }

        private string GetUserName()
        {
            return System.Security.Principal.GenericPrincipal.Current.Identity.Name;
        }
    }
}
