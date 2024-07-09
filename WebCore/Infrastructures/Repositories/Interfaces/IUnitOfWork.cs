

using System.Threading.Tasks;
using WebCore.Domain;
using WebCore.Infrastructures.Repositories.Infrastructure.Core.Repositories;

namespace WebCore.Infrastructures.Repositories
{
    public interface IUnitOfWork : IUnitOfWorkBase<IAggregateRoot, int>
    {
        Repository<T> Repository<T>() where T : class;
        GenericRepository<T> RepositoryDbWrite<T>() where T : class, IAggregateRoot;
    }

    public interface IUnitOfWorkBase<TEntity, TId>
        where TEntity : class, IAggregateRoot<TId>
        where TId : struct
    {
        GenericRepositoryBase<TEntity, TId> RepositoryDbWrite<TEntity, TId>() 
            where TEntity : class, IAggregateRoot<TId>
            where TId : struct;

        Repository<T> Repository<T>() where T : class;

        int CommitDbWrite();
        int SaveChanges();
    }
}
