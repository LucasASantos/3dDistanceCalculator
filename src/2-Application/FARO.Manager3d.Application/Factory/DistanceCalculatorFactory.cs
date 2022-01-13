using System;
using FARO.Manager3d.Application.Factory.Interfaces;
using FARO.Manager3d.Application.Tasks;
using FARO.Manager3d.Application.Tasks.Interfaces;
using FARO.Manager3d.Domain.DomainService.Interfaces;

namespace FARO.Manager3d.Application.Factory
{
    public class DistanceCalculatorFactory : IDistanceCalculatorFactory
    {
        private readonly IActualDomainService _actualDomainService;

        public DistanceCalculatorFactory(IActualDomainService actualDomainService)
        {
            _actualDomainService = actualDomainService;
        }

        public IDistanceCalculator GetCalculator(string method)
        {
            switch (method)
            {
                case "sync":
                    return new SyncDistanceCalculator(_actualDomainService);
                case "async":
                    return new AsyncDistanceCalculator(_actualDomainService);
                default:
                    throw new ArgumentException("Error to macth method in calculator factory");
            }
        }
    }
}