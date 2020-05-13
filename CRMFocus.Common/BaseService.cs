using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq.Expressions;
using System.Transactions;

namespace CRMFocus.Common
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        private readonly IBaseRepository<TEntity> _repository;

        public BaseService(IBaseRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual TEntity GetById(int id)
        {
            return _repository.GetById(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _repository.GetAll();
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.Find(predicate);
        }

        public int GetTotalCount()
        {
            return _repository.GetTotalCount();
        }

        public virtual TEntity Create(TEntity entity)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    entity =_repository.Create(entity);                  
                    ts.Complete();
                }
            }
            catch (TransactionAbortedException ex)
            {
                throw ex;
            }
            catch (OptimisticConcurrencyException ex)
            {
                throw ex;
            }
            catch (UpdateException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return entity;
        }

        public virtual void CreateBulk(IEnumerable<TEntity> entities)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    _repository.CreateBulk(entities);
                    ts.Complete();
                }
            }
            catch (TransactionAbortedException ex)
            {
                throw ex;
            }
            catch (OptimisticConcurrencyException ex)
            {
                throw ex;
            }
            catch (UpdateException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual TEntity Update(int id, TEntity newEntity)
        {

            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    var oldEntity = _repository.GetById(id);

                    Mapper.Map(newEntity, oldEntity);
                    newEntity = _repository.Update(oldEntity);

                    ts.Complete();
                }
            }
            catch (TransactionAbortedException ex)
            {
                throw ex;
            }
            catch (OptimisticConcurrencyException ex)
            {
                throw ex;
            }
            catch (UpdateException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return newEntity;
        }

        public virtual TEntity Delete(int id)
        {
            var entity = new TEntity();

            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    entity = _repository.GetById(id);
                    entity = _repository.Delete(entity);
                    ts.Complete();
                }
            }
            catch (TransactionAbortedException ex)
            {
                throw ex;
            }
            catch (OptimisticConcurrencyException ex)
            {
                throw ex;
            }
            catch (UpdateException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return entity;
        }
    }
}
