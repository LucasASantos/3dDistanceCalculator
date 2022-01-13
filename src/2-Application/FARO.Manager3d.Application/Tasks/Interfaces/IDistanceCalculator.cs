using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FARO.Manager3d.Domain.Models;

namespace FARO.Manager3d.Application.Tasks.Interfaces
{
    public interface IDistanceCalculator
    {
        Task<IEnumerable<(ActualPoint, double)>> CalculateAsync(NominalPoint nominalPoint, CancellationToken cancellationToken);
        
    }
}