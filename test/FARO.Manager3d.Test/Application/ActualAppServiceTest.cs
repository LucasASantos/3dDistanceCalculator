using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FARO.Manager3d.Application.Map;
using FARO.Manager3d.Application.Service;
using FARO.Manager3d.Application.Service.Interfaces;
using FARO.Manager3d.Application.ViewModels;
using FARO.Manager3d.Domain.DomainService.Interfaces;
using FARO.Manager3d.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace FARO.Manager3d.Test.Application
{
    [TestClass]
    public class ActualAppServiceTest
    {
        private CancellationToken _cancellationToken;

        private IActualPointAppService _actualPointAppService;
        private IActualDomainService _actualDomainService;
        private IMapper _mapper;

        private ActualPointViewModel _actualPointViewModel;
        private ActualPoint _actualPoint;
        private NominalPointViewModel _nominalPointViewModel;
        private NominalPoint _nominalPoint;

        private Guid _actualPointId;
        private Guid _nominalPointId;



        [TestInitialize]
        public void Initializer()
        {
            _cancellationToken = CancellationToken.None;
            _actualDomainService = Substitute.For<IActualDomainService>();

            _mapper = Substitute.For<IMapper>();

            _actualPointAppService = new ActualPointAppService(_actualDomainService, _mapper);

            MockEntities();
        }

        [TestMethod]
        [TestCategory("Application - Models Services"), Description("Base add one actual point should return success")]
        public async Task AddActualPointShouldReturnSuccessAsync()
        {
            //Arrange
            MockActualDomain();
            MockMapper();
            //Act and Assert
            await _actualPointAppService.AddAsync(1.0001, 2.0002, 3.0003, _nominalPointViewModel, _cancellationToken);


        }

        [TestMethod]
        [TestCategory("Application - Models Services"), Description("Base update one actual point should return success")]
        public async Task UpdateActualPointShouldReturnSuccessAsync()
        {
            //Arrange
            MockActualDomain();
            MockMapper();

            //Act and Assert
            await _actualPointAppService.UpdateAsync(_actualPointId, 1.0001, 2.0002, 3.0003, _cancellationToken);


        }

        [TestMethod]
        [TestCategory("Application - Models Services"), Description("Base delete one actual point should return success")]
        public async Task DeleteActualPointShouldReturnSuccessAsync()
        {
            //Arrange
            MockActualDomain();
            MockMapper();

            //Act and Assert
            await _actualPointAppService.DeleteAsync(_actualPointId, _cancellationToken);

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

        public void MockActualDomain()
        {
            var listReturns = new List<ActualPoint>();
            listReturns.Add(_actualPoint);
            _actualDomainService.GetAsync(_cancellationToken).Returns(listReturns);
            _actualDomainService.GetByIDAsync(_actualPointId, _cancellationToken).Returns(_actualPoint);
            _actualDomainService.GetByNominalPointAsync(_nominalPointId, _cancellationToken).Returns(listReturns);
            _actualDomainService.CreateNewAsync(1.0001, 2.0002, 3.0003, _nominalPoint, _cancellationToken).Returns(_actualPoint);
            _actualDomainService.When(a => a.AddAsync(_actualPoint, _cancellationToken));
            _actualDomainService.When(a => a.UpdateAsync(_actualPoint, _cancellationToken));
            _actualDomainService.When(a => a.DeleteAsync(_actualPoint, _cancellationToken));
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