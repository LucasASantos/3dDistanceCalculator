using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FARO.Manager3d.Application.ViewModels;
using FARO.Manager3d.Domain.Models;

namespace FARO.Manager3d.Application.Service.Interfaces
{
    public interface IPointAppService
    {
        Task<IEnumerable<ActualPointViewModel>> CalculateDistance(NominalPointViewModel nominalPoint, string method, CancellationToken cancellationToken); 
        NominalPointViewModel CalculateAvg(NominalPointViewModel nominalPoint, IEnumerable<ActualPointViewModel> actualPoints, CancellationToken cancellationToken);
    }
}