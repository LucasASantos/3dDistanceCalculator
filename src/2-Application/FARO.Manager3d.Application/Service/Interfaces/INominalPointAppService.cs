using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FARO.Manager3d.Application.ViewModels;

namespace FARO.Manager3d.Application.Service.Interfaces
{
    public interface INominalPointAppService
    {
        Task<NominalPointViewModel> AddAsync(double x, double y, double z, CancellationToken cancellationToken);
        Task<NominalPointViewModel> UpdateAsync(Guid id, double x, double y, double z, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<NominalPointViewModel>> GetAsync(CancellationToken cancellationToken);
        Task<NominalPointViewModel> GetByIDAsync(Guid id, CancellationToken cancellationToken);
    }
}