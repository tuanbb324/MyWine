using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using WebCore.Domain;
using WebCore.Infrastructures.Services;

namespace WebCore.Infrastructures.Repositories
{
    namespace Infrastructure.Core.Repositories
    {
        public class GenericRepository<TEntity> : GenericRepositoryBase<TEntity, int>
            where TEntity : class, IAggregateRoot<int>
        {
            public GenericRepository(ApplicationDbContext context) : base(context)
            {
            }
        }

        public class GenericRepositoryBase<TEntity, TId> 
            where TEntity : class, IAggregateRoot<TId>
            where TId : struct
        {
            protected readonly ApplicationDbContext Context;
            protected readonly DbSet<TEntity> DbSet;
            public GenericRepositoryBase(ApplicationDbContext context)
            {
                Context = context;
                DbSet = context.Set<TEntity>();
            }

            public void Add(TEntity obj)
            {
                DbSet.Add(obj);
            }

            public async Task<EntityEntry<TEntity>> AddAsync(TEntity obj)
            {
                return await DbSet.AddAsync(obj);
            }

            public void Dispose()
            {
                Context.Dispose();
            }

            public IQueryable<TEntity> GetAll(bool isAsNoTracking = true)
            {
                if (isAsNoTracking)
                {
                    return DbSet.AsNoTracking();
                }
                return DbSet;
            }

            public virtual TEntity GetById(TId id)
            {
                return DbSet.SingleOrDefault(x => x.Id.Equals(id));
            }

            public virtual async Task<TEntity> GetByIdAsync(TId id)
            {
                return await DbSet.SingleOrDefaultAsync(x => x.Id.Equals(id));
            }

            public void Remove(TId id)
            {
                var entity = DbSet.SingleOrDefault(x => x.Id.Equals(id));
                DbSet.Attach(entity);
                Context.Entry(entity).State = EntityState.Deleted;
            }

            public void Update(TEntity obj)
            {
                DbSet.Attach(obj);
                Context.Entry(obj).State = EntityState.Modified;
            }

            public void UpdateRange(List<TEntity> objs)
            {
                DbSet.UpdateRange(objs);
            }
            
            public Task AddRangeAsync(List<TEntity> objs)
            {
                return DbSet.AddRangeAsync(objs);
            }
        }

        public class Repository<TEntity> where TEntity : class
        {
            protected readonly ApplicationDbContext Context;
            protected readonly DbSet<TEntity> DbSet;
            public Repository(ApplicationDbContext context)
            {
                Context = context;
                DbSet = context.Set<TEntity>();
            }

            public void Add(TEntity obj)
            {
                DbSet.Add(obj);
            }

            public async Task<EntityEntry<TEntity>> AddAsync(TEntity obj)
            {
                return await DbSet.AddAsync(obj);
            }

            public void Dispose()
            {
                Context.Dispose();
            }

            public IQueryable<TEntity> GetAll(bool isAsNoTracking = true)
            {
                if (isAsNoTracking)
                {
                    return DbSet.AsNoTracking();
                }
                return DbSet;
            }

            public void Update(TEntity obj)
            {
                DbSet.Attach(obj);
                Context.Entry(obj).State = EntityState.Modified;
            }

            public void UpdateRange(List<TEntity> objs)
            {
                DbSet.UpdateRange(objs);
            }

            public Task AddRangeAsync(List<TEntity> objs)
            {
                return DbSet.AddRangeAsync(objs);
            }
        }
    }
}
