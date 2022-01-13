using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FARO.Manager3d.Application.ViewModels;

namespace FARO.Manager3d.Application.Service.Interfaces
{
    public interface IActualPointAppService
    {
        Task<ActualPointViewModel> AddAsync(double x, double y, double z, NominalPointViewModel nominalPoint, CancellationToken cancellationToken);
        Task<ActualPointViewModel> UpdateAsync(Guid id, double x, double y, double z, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<ActualPointViewModel>> GetAsync(CancellationToken cancellationToken);
        Task<ActualPointViewModel> GetByIDAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<ActualPointViewModel>> GetByNominalPointAsync(Guid nominalPointId, CancellationToken cancellationToken);
    }
}