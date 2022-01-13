using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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
    public class NominalAppServiceTest
    {
        
        private CancellationToken _cancellationToken;

        private INominalPointAppService _nominalPointAppService;
        private INominalDomainService _nominalDomainService;
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
            _nominalDomainService = Substitute.For<INominalDomainService>();

            _mapper = Substitute.For<IMapper>();

            _nominalPointAppService = new NominalPointAppService(_nominalDomainService, _mapper);

            MockEntities();
        }

        [TestMethod]
        [TestCategory("Application - Models Services"), Description("Base add one nominal point should return success")]
        public async Task AddNominalPointShouldReturnSuccessAsync()
        {
            //Arrange
            MockNominalDomain();
            MockMapper();
            //Act and Assert
            await _nominalPointAppService.AddAsync(1, 2, 3, _cancellationToken);


        }

        [TestMethod]
        [TestCategory("Application - Models Services"), Description("Base update one nominal point should return success")]
        public async Task UpdateNominalPointShouldReturnSuccessAsync()
        {
            //Arrange
            MockNominalDomain();
            MockMapper();

            //Act and Assert
            await _nominalPointAppService.UpdateAsync(_nominalPointId, 1, 2, 3, _cancellationToken);


        }

        [TestMethod]
        [TestCategory("Application - Models Services"), Description("Base delete one nominal point should return success")]
        public async Task DeleteNominalPointShouldReturnSuccessAsync()
        {
            //Arrange
            MockNominalDomain();
            MockMapper();

            //Act and Assert
            await _nominalPointAppService.DeleteAsync(_nominalPointId, _cancellationToken);

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

        public void MockNominalDomain()
        {
            var listReturns = new List<NominalPoint>();
            listReturns.Add(_nominalPoint);
            _nominalDomainService.GetAsync(_cancellationToken).Returns(listReturns);
            _nominalDomainService.GetByIDAsync(_nominalPointId, _cancellationToken).Returns(_nominalPoint);
            _nominalDomainService.CreateNewAsync(1, 2, 3, _cancellationToken).Returns(_nominalPoint);
            _nominalDomainService.When(a => a.AddAsync(_nominalPoint, _cancellationToken));
            _nominalDomainService.When(a => a.UpdateAsync(_nominalPoint, _cancellationToken));
            _nominalDomainService.When(a => a.DeleteAsync(_nominalPoint, _cancellationToken));
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