using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FARO.Manager3d.Domain.Models;

namespace FARO.Manager3d.Domain.DomainService.Interfaces
{
    public interface IActualDomainService: IDisposable
    {
        Task<ActualPoint> CreateNewAsync(double x, double y, double z, NominalPoint nominalPoint, CancellationToken cancellationToken);
        Task AddAsync(ActualPoint actualPoint, CancellationToken cancellationToken);
        Task UpdateAsync(ActualPoint actualPoint, CancellationToken cancellationToken);
        Task DeleteAsync(ActualPoint actualPoint, CancellationToken cancellationToken);
        Task<IEnumerable<ActualPoint>> GetAsync(CancellationToken cancellationToken);
        Task<ActualPoint> GetByIDAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<ActualPoint>> GetByNominalPointAsync(Guid nominalPointId, CancellationToken cancellationToken);

    }
}