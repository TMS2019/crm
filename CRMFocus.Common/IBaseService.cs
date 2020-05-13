using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CRMFocus.Common
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        int GetTotalCount();

        TEntity Create(TEntity entity);
        void CreateBulk(IEnumerable<TEntity> entities);

        TEntity Update(int id, TEntity newEntity);

        TEntity Delete(int id);
    }
}
