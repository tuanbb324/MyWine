

using WebCore.Domain;
using WebCore.Infrastructures.Repositories.Infrastructure.Core.Repositories;
using WebCore.Infrastructures.Services;

namespace WebCore.Infrastructures.Repositories
{
    public class UnitOfWork : BaseUnitOfWork<IAggregateRoot, int>, IUnitOfWork
    {
        public UnitOfWork(ApplicationDbContext contextDbRead) 
            : base(contextDbRead)
        {
        }

        // private readonly ContextDbRead _contextDbRead;
        // private readonly ContextDbWrite _contextDbWrite;   
        // public UnitOfWork(ContextDbRead contextDbRead, ContextDbWrite contextDbWrite)
        // {
        //     _contextDbRead = contextDbRead;
        //     _contextDbWrite = contextDbWrite;
        // }
        // public Repository<T> Repository<T>() where T : class
        // {
        //     var result = (Repository<T>)Activator.CreateInstance(typeof(Repository<T>), _contextDbWrite);
        //     if (result != null)
        //     {
        //         return result;
        //     }
        //     return null;
        // }
        public GenericRepository<T> RepositoryDbWrite<T>() where T : class, IAggregateRoot
        {
            var result = (GenericRepository<T>)Activator.CreateInstance(typeof(GenericRepository<T>), _contextDbRead);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        // public int CommitDbWrite()
        // {
        //     return _contextDbWrite.SaveChanges();
        // }

        public int SaveChanges()
        {

            using (var dbContextTransaction = _contextDbRead.Database.BeginTransaction())
            {
                try
                {
                    var intSave =  _contextDbRead.SaveChanges();
                    dbContextTransaction.Commit();
                    return intSave;

                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    return 0;
                }
            }

        }
    }
}
