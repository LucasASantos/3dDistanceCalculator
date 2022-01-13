using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FARO.Manager3d.Domain.Models;

namespace FARO.Manager3d.Domain.DomainService.Interfaces
{
    public interface INominalDomainService: IDisposable
    {
        Task<NominalPoint> CreateNewAsync(double x, double y, double z, CancellationToken cancellationToken);
        Task AddAsync(NominalPoint nominalPoint, CancellationToken cancellationToken);
        Task UpdateAsync(NominalPoint nominalPoint, CancellationToken cancellationToken);
        Task DeleteAsync(NominalPoint nominalPoint, CancellationToken cancellationToken);
        Task<IEnumerable<NominalPoint>> GetAsync(CancellationToken cancellationToken);
        Task<NominalPoint> GetByIDAsync(Guid id, CancellationToken cancellationToken);

    }
}