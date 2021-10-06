using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Safarset.Datalayer.Context;

namespace Safarset.Service.Services
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        Task<bool> DeleteAsync(bool saveNow, params object[] key);
        ValueTask<TEntity> FindAsync(params object[] key);
        TEntity Find(params object[] key);
        Task<bool> InsertAsync(TEntity entity, bool saveNow);
        Task<bool> UpdateAsync(TEntity entity, bool saveNow);
        Task<int> GetMaxKeyAsync(Expression<Func<TEntity, int>> keySelector);
        Task<long> GetMaxKeyAsync(Expression<Func<TEntity, long>> keySelector);
        Task<TEntity> FindAsNoTrackingAsync(Expression<Func<TEntity, bool>> expression);
        Dictionary<string, object> KeyNameAndValue(TEntity model);
        string[] KeyName();
        Task<List<TEntity>> GetAllAsNoTrackingAsync();
        IQueryable<TEntity> GetAllAsQueryable();
        object[] KeyValue(TEntity model);
    }
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        protected readonly IUnitOfWork Uow;
        protected internal readonly DbSet<TEntity> DbSet;
        private readonly ILogger Logger;

        public BaseService(IUnitOfWork unitOfWork, ILogger<TEntity> logger)
        {
            Uow = unitOfWork;
            DbSet = Uow.Set<TEntity>();
            Logger = logger;

        }

        public virtual async Task<bool> CheckExistAsync(params object[] key)
        {
            return await DbSet.FindAsync(key) != null;
        }


        public virtual async Task<bool> InsertAsync(TEntity entity, bool saveNow)
        {
            DbSet.Add(entity);
            if (saveNow)
            {
                try
                {
                    await Uow.SaveAllChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex,ex.Message);
                    return false;
                }
            }
            return false;
        }

        public virtual async Task<bool> DeleteAsync(bool saveNow, params object[] key)
        {
            TEntity entity = await DbSet.FindAsync(key);
            if (entity == null)
                throw new Exception("Key not foune");

            DbSet.Remove(entity);
            if (saveNow)
            {
                try
                {
                    await Uow.SaveAllChangesAsync();
                    return true;
                }
                catch (DbUpdateException)
                {
                    throw new Exception("Object has relation");
                }
                catch (Exception ex)
                {

                    Logger.LogError(ex,ex.Message);

                }
            }
            return false;
        }

        public virtual async Task<bool> UpdateAsync(TEntity entity, bool saveNow = true)
        {
            DbSet.Update(entity);

            if (saveNow)
            {
                try
                {
                    await Uow.SaveAllChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex,ex.Message);
                    return false;
                }
            }
            return false;
        }

        public ValueTask<TEntity> FindAsync(params object[] key)
        {
            return DbSet.FindAsync(key);
        }

        public Task<TEntity> FindAsNoTrackingAsync(Expression<Func<TEntity, bool>> expression)
        {
            return DbSet.AsNoTracking().SingleOrDefaultAsync(expression);
        }

        public TEntity Find(params object[] key)
        {
            return DbSet.Find(key);
        }

        public async Task<int> GetMaxKeyAsync(Expression<Func<TEntity, int>> keySelector)
        {
            int x;
            try
            {
                x = await DbSet.MaxAsync(keySelector);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                x = 0;
            }
            return x;
        }

        public async Task<long> GetMaxKeyAsync(Expression<Func<TEntity, long>> keySelector)
        {
            long x;
            try
            {
                x = await DbSet.MaxAsync(keySelector);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                x = 0;
            }

            return x;
        }

        public virtual string[] KeyName()
        {
            var keys = Uow.GetKey<TEntity>();
            string[] keyNames = new string[keys.Length];
            for (int i = 0; i < keys.Length; i++)
            {
                keyNames[i] = keys[i].Name;
            }
            return keyNames;
        }

        public async Task<List<TEntity>> GetAllAsNoTrackingAsync()
        {
            List<TEntity> x;
            try
            {
                x = await DbSet.AsQueryable().ToListAsync();
            }
            catch (Exception ex)
            {

                Logger.LogError(ex, ex.Message);
                throw;
            }

            return x;
        }

        public IQueryable<TEntity> GetAllAsQueryable()
        {
            return DbSet.AsQueryable();
        }

        public virtual Dictionary<string, object> KeyNameAndValue(TEntity model)
        {
            var type = model.GetType();
            string[] keyNames = KeyName();
            Dictionary<string, object> obj = new Dictionary<string, object>();
            foreach (var keyName in keyNames)
            {
                obj.Add(keyName, type.GetProperty(keyName)?.GetValue(model));
            }
            return obj;
        }
        public virtual object[] KeyValue(TEntity model)
        {
            var type = model.GetType();
            string[] keyNames = KeyName();
            object[] keyValues = new object[keyNames.Length];
            for (int i = 0; i < keyNames.Length; i++)
            {
                keyValues[i] = type.GetProperty(keyNames[i])?.GetValue(model);
            }
            return keyValues;
        }
    }
}
