
using WebCore.Infrastructures.Repositories;
using WebCore.Domain;
using WebCore.Infrastructures.Services;
using WebCore.Infrastructures.Repositories.Infrastructure.Core.Repositories;

namespace WebCore.Infrastructures.Repositories
{
    public class BaseUnitOfWork<TEntity, TId> : IUnitOfWorkBase<TEntity, TId>
        where TEntity : class, IAggregateRoot<TId>
        where TId : struct
    {
        protected readonly ApplicationDbContext _contextDbRead;


        public BaseUnitOfWork(ApplicationDbContext contextDbRead )
        {
            _contextDbRead = contextDbRead;
        }

        public int CommitDbWrite()
        {
            return _contextDbRead.SaveChanges();
        }

        public Repository<T> Repository<T>() where T : class
        {
            var result = (Repository<T>)Activator.CreateInstance(typeof(Repository<T>), _contextDbRead);
            if (result != null)
            {
                return result;
            }
            return null;
        }


        public GenericRepositoryBase<TEntity, TId> RepositoryDbWrite<TEntity, TId>()
           where TEntity : class, IAggregateRoot<TId>
           where TId : struct
        {
            var result = (GenericRepositoryBase<TEntity, TId>)Activator.CreateInstance(typeof(GenericRepositoryBase<TEntity, TId>), _contextDbRead);
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public int SaveChanges()
        {
            var res = _contextDbRead.SaveChanges();
            return res;
        }
    }
}