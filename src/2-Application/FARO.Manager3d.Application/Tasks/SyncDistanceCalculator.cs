using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FARO.Manager3d.Application.Tasks.Interfaces;
using FARO.Manager3d.Domain.DomainService.Interfaces;
using FARO.Manager3d.Domain.Models;

namespace FARO.Manager3d.Application.Tasks
{
    public class SyncDistanceCalculator : IDistanceCalculator
    {
        private readonly IActualDomainService _actualDomainService;

        public SyncDistanceCalculator(IActualDomainService actualDomainService)
        {
            _actualDomainService = actualDomainService;
        }

        public async Task<IEnumerable<(ActualPoint, double)>> CalculateAsync(NominalPoint nominalPoint, CancellationToken cancellationToken)
        {
            var actualPoints = await _actualDomainService.GetByNominalPointAsync(nominalPoint.Id, cancellationToken);
            var distances = new List<(ActualPoint, double)>();
            foreach (var item in actualPoints)
            {
                distances.Add((item, CalculateDistance(nominalPoint, item)));
            }

            return distances;
        }

        public double CalculateDistance(NominalPoint nominalPoint, ActualPoint actualPoint)
        {
            double deltaX = nominalPoint.X - actualPoint.X;
            double deltaY = nominalPoint.Y - actualPoint.Y;
            double deltaZ = nominalPoint.Z - actualPoint.Z;

            return (double)Math.Round(Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ), 6);
        }


    }
}