using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FARO.Manager3d.Application.Tasks.Interfaces;
using FARO.Manager3d.Domain.DomainService.Interfaces;
using FARO.Manager3d.Domain.Models;


namespace FARO.Manager3d.Application.Tasks
{
    public class AsyncDistanceCalculator : IDistanceCalculator
    {
        private readonly IActualDomainService _actualDomainService;

        public AsyncDistanceCalculator(IActualDomainService actualDomainService)
        {
            _actualDomainService = actualDomainService;
        }

        public async Task<IEnumerable<(ActualPoint, double)>> CalculateAsync(NominalPoint nominalPoint, CancellationToken cancellationToken)
        {
            var mapActualPoint = new List<(int, ActualPoint)>();
            var tasks = new List<Task<(ActualPoint, double)>>();

            var actualPoints = await _actualDomainService.GetByNominalPointAsync(nominalPoint.Id, cancellationToken);

            foreach (var item in actualPoints)
            {
                tasks.Add(CalculateDistanceAsync(nominalPoint, item));
            }

            return (await Task.WhenAll(tasks)).ToList();
        }

        public Task<(ActualPoint, double)> CalculateDistanceAsync(NominalPoint nominalPoint, ActualPoint actualPoint)
        {
            return Task<(ActualPoint, double)>.Run(() =>
            {
                double deltaX = nominalPoint.X - actualPoint.X;
                double deltaY = nominalPoint.Y - actualPoint.Y;
                double deltaZ = nominalPoint.Z - actualPoint.Z;

                return (actualPoint, (double)Math.Round(Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ), 6));
            }
            );
        }
    }
}