using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FARO.Manager3d.Domain.Models;

namespace FARO.Manager3d.Domain.Repository.Interfaces
{
    public interface IActualPointRepository:IBaseRepository<ActualPoint>
    {
        Task<IEnumerable<ActualPoint>> GetByNominalIdAsync(Guid nominalId, CancellationToken cancellationToken);
         
    }
}