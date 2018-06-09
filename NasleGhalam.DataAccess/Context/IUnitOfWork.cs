using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using NasleGhalam.Common;

namespace NasleGhalam.DataAccess.Context
{
    public interface IUnitOfWork
    {
        //IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        //int SaveChanges();


        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        //ResultStatus CommitChanges(bool invalidateCacheDependencies = true);
        //ResultStatus CommitChangesAsync(bool invalidateCacheDependencies = true);
        //ParvazCoreDataContext GetCurrentContextInstance();
        int SaveChanges();

        MessageResult CommitChanges(CrudType type = CrudType.None, string fieldName = "");

        Task<int> SaveChangesAsync();
        void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class;
        void MarkAsDeleted<TEntity>(TEntity entity) where TEntity : class;

        void Dispose();
        //IList<T> GetRows<T>(string sql, params object[] parameters) where T : class;
        //void ForceDatabaseInitialize();
        //int SaveAllChanges(bool invalidateCacheDependencies = true);
        //Task<int> SaveAllChangesAsync(bool invalidateCacheDependencies = true);
        //void AutoDetectChangesEnabled(bool flag = true);
        //ResultStatus RejectChanges();


        //void Update<TEntity>(TEntity entity, object key) where TEntity : class;
        //void ExcludeFieldFromUpdate<TEntity>(TEntity entity, params Expression<Func<TEntity, object>>[] updatedProperties)
        //    where TEntity : class;
        //void Log();
    }
}
