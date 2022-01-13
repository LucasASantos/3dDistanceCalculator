using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FARO.Manager3d.Data.Context;
using FARO.Manager3d.Domain.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Domain;

namespace FARO.Manager3d.Data.Repository
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        protected readonly ApplicationContext Db;
        public readonly DbSet<T> DbSet;

        public BaseRepository(ApplicationContext context)
        {
            Db = context;
            DbSet = Db.Set<T>();
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await Db.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAsync(CancellationToken cancellationToken)
        {
            return await DbSet.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<T> GetByIDAsync(Guid id, CancellationToken cancellationToken)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public virtual void Add(T obj)
        {
            DbSet.Add(obj);
        }

        public void Delete(T obj)
        {
            DbSet.Remove(obj);
        }

        public void Update(T obj)
        {
            DbSet.Update(obj);
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

        public void DeleteRange(IEnumerable<T> objs)
        {
            DbSet.RemoveRange(objs);
        }
    }
}