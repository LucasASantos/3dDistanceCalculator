using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NetDevPack.Domain;

namespace FARO.Manager3d.Domain.Repository.Interfaces
{
    public interface IBaseRepository<T>: IDisposable where T : Entity
    {
        Task<IEnumerable<T>> GetAsync(CancellationToken cancellationToken);
        Task<T> GetByIDAsync(Guid id, CancellationToken cancellationToken);
        void Add(T obj);
        void Delete(T obj);
        void DeleteRange(IEnumerable<T> objs);
        void Update(T obj);
        Task SaveAsync(CancellationToken cancellationToken);
    }
}