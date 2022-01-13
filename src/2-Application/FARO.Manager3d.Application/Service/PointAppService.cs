using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FARO.Manager3d.Application.Factory.Interfaces;
using FARO.Manager3d.Application.Service.Interfaces;
using FARO.Manager3d.Application.ViewModels;
using FARO.Manager3d.Domain.Models;

namespace FARO.Manager3d.Application.Service
{
    public class PointAppService: IPointAppService
    {
        private readonly IDistanceCalculatorFactory _distanceCalculatorFactory;
        private readonly IMapper _mapper;


        public PointAppService(IDistanceCalculatorFactory distanceCalculatorFactory, IMapper mapper)
        {
            _distanceCalculatorFactory = distanceCalculatorFactory;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ActualPointViewModel>> CalculateDistance(NominalPointViewModel nominalPoint, string method, CancellationToken cancellationToken)
        {

            var result =  await _distanceCalculatorFactory
                .GetCalculator(method)
                .CalculateAsync(_mapper.Map<NominalPoint>(nominalPoint), cancellationToken);

            var mapResult = new List<ActualPointViewModel>();
            foreach (var item in result)
            {
                var actual = _mapper.Map<ActualPointViewModel>(item.Item1);
                actual.Distance = item.Item2;
                mapResult.Add(actual);
            }

            return mapResult;
        }

        public NominalPointViewModel CalculateAvg(NominalPointViewModel nominalPoint, IEnumerable<ActualPointViewModel> actualPoints, CancellationToken cancellationToken)
        {
            var count = actualPoints.Count();
            if(count > 0)
            {
                var xSum = actualPoints.Sum(c=> c.X);
                var ySum = actualPoints.Sum(c=> c.Y);
                var zSum = actualPoints.Sum(c=> c.Z);
                nominalPoint.AddAvg(RoundAvg(xSum,count),RoundAvg(ySum,count),RoundAvg(zSum,count));
            }

            return nominalPoint;
        }

        private double RoundAvg (double sum, int count) => Math.Round(sum/count, 6);
        
    }
}