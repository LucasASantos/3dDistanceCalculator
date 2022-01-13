using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FARO.Manager3d.Application.Factory.Interfaces;
using FARO.Manager3d.Application.Service;
using FARO.Manager3d.Application.Service.Interfaces;
using FARO.Manager3d.Application.Tasks;
using FARO.Manager3d.Application.Tasks.Interfaces;
using FARO.Manager3d.Application.ViewModels;
using FARO.Manager3d.Domain.DomainService.Interfaces;
using FARO.Manager3d.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace FARO.Manager3d.Test.Application
{
    [TestClass]
    public class PointAppServiceTest
    {
        private CancellationToken _cancellationToken;
        private IPointAppService _pointAppService;
        private IMapper _mapper;
        private IDistanceCalculatorFactory _distanceCalculatorFactory;
        
        private IDistanceCalculator _distanceCalculator;

        private ActualPointViewModel _actualPointViewModel;
        private ActualPoint _actualPoint;
        private NominalPointViewModel _nominalPointViewModel;
        private NominalPoint _nominalPoint;
        private Guid _actualPointId;
        private Guid _nominalPointId;
        private List<ActualPointViewModel> _actualPointViewModels;


        [TestInitialize]
        public void Initializer()
        {
            _cancellationToken = CancellationToken.None;
            _mapper = Substitute.For<IMapper>();
            _distanceCalculatorFactory = Substitute.For<IDistanceCalculatorFactory>();
            _distanceCalculator = Substitute.For<IDistanceCalculator>();

            _pointAppService = new PointAppService(_distanceCalculatorFactory, _mapper);
            MockEntities();
        }

        [TestMethod]
        [TestCategory("Application - Point Services"), Description("Given sent a nominal point as parameter should return all actual points with distance calculated")]
        [DataTestMethod]
        [DataRow("sync")]
        [DataRow("async")]
        public async Task GetActualPointDistancebyNominalPointShouldReturnSuccessAsync(string method)
        {
            //Arrange
            MockDistanceCalculatorFactory(method);
            MockMapper();

            //Act
            var actualPoints = await _pointAppService.CalculateDistance(_nominalPointViewModel, method, _cancellationToken);

            //Assert
            Assert.AreEqual(actualPoints.First().Distance, 0.076763);

        }


        [TestMethod]
        [TestCategory("Application - Point Services"), Description("Given sent a nominal point as parameter should return the nominal point with respective coordinate average calculated ")]
        public void GetNominalPointAvgShouldReturnSuccess()
        {
            //Arrange
            AddMockActualPoint(1, 2, 3);
            AddMockActualPoint(0.998, 0.889,0.878);
            AddMockActualPoint(2.0001,2.9993,3.0004);

            //Act
            var nominalPointViewModel = _pointAppService.CalculateAvg(_nominalPointViewModel,_actualPointViewModels, _cancellationToken);

            //Assert
            Assert.AreEqual(nominalPointViewModel.XAvg, 1.3327);
            Assert.AreEqual(nominalPointViewModel.YAvg, 1.962767);
            Assert.AreEqual(nominalPointViewModel.ZAvg, 2.2928);

        }



        public void MockDistanceCalculatorFactory(string method)
        {
            var listReturns = new List<ActualPoint>();
            var listCalculatorReturns = new List<(ActualPoint, double)>();

            listReturns.Add(_actualPoint);
            listCalculatorReturns.Add((_actualPoint, 0.076763));

            _distanceCalculator.CalculateAsync(_nominalPoint, _cancellationToken).Returns(listCalculatorReturns);

            _distanceCalculatorFactory.GetCalculator(method)
                .Returns(_distanceCalculator);

        }

        public void AddMockActualPoint(double x, double y, double z)
        {
            if (_actualPointViewModels == null)
            {
                _actualPointViewModels = new List<ActualPointViewModel>();
            }
            _actualPointViewModels.Add(new ActualPointViewModel()
            {
                X =x,
                Y =y,
                Z =z
            });

        }

        public void MockEntities()
        {
            _nominalPoint = new NominalPoint()
            {
                X = 1,
                Y = 2,
                Z = 3

            };
            _nominalPointId = _nominalPoint.Id;
            _nominalPointViewModel = new NominalPointViewModel()
            {
                Id = _nominalPointId,
                X = 1,
                Y = 2,
                Z = 3
            };

            _actualPoint = new ActualPoint()
            {
                X = 1.0001,
                Y = 2.0002,
                Z = 3.0003,
                NominalPoint = _nominalPoint
            };
            _actualPointId = _actualPoint.Id;
            _actualPointViewModel = new ActualPointViewModel()
            {
                Id = _actualPointId,
                X = 1.0001,
                Y = 2.0002,
                Z = 3.0003,
                NominalPoint = _nominalPointViewModel
            };

        }

        public void MockMapper()
        {
            _mapper.Map<ActualPointViewModel>(_actualPoint).Returns(_actualPointViewModel);
            _mapper.Map<ActualPoint>(_actualPointViewModel).Returns(_actualPoint);
            _mapper.Map<NominalPointViewModel>(_nominalPoint).Returns(_nominalPointViewModel);
            _mapper.Map<NominalPoint>(_nominalPointViewModel).Returns(_nominalPoint);
        }

    }
}