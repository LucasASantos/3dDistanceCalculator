using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FARO.Manager3d.Domain.DomainService;
using FARO.Manager3d.Domain.DomainService.Interfaces;
using FARO.Manager3d.Domain.Models;
using FARO.Manager3d.Domain.Repository.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace FARO.Manager3d.Test.Domain
{
    [TestClass]
    public class ActualDomainServiceTest
    {
        private IActualDomainService _actualDomainService;

        private IValidator<ActualPoint> _validator;
        private IActualPointRepository _actualPointRepository;

        private CancellationToken _cancellationToken;
        private Guid _nominalPointId;
        private NominalPoint _nominalPoint;

        private Guid _actualPointId;
        private ActualPoint _actualPoint;
        private ActualPoint _newActualPoint;

        [TestInitialize]
        public void Initializer()
        {
            _cancellationToken = CancellationToken.None;

            _actualPointRepository = Substitute.For<IActualPointRepository>();
            _validator = Substitute.For<IValidator<ActualPoint>>();


            _actualDomainService = new ActualDomainService(_validator,_actualPointRepository);

            MockActualPoint();
        }

        [TestMethod]
        [TestCategory("DomainServices"), Description("Base get actual points should return all actual points")]
        public async Task GetAllActualPointsShouldReturnSuccessAsync()
        {
            //Arrange
            MockActualRepository();

            //Act
            var result = await _actualDomainService.GetAsync(_cancellationToken);

            //Assert
            Assert.IsTrue(result.Any());

        }

        [TestMethod]
        [TestCategory("DomainServices"), Description("Base get actual points by nominal id should return all actual points referente that nominal id")]
        public async Task GetAllActualPointsByNominalIdShouldReturnSuccessAsync()
        {
            //Arrange
            MockActualRepository();

            //Act
            var result = await _actualDomainService.GetByNominalPointAsync(_nominalPointId, _cancellationToken);

            //Assert
            Assert.IsTrue(result.Any());
            Assert.AreEqual(result.First().NominalPoint.Id, _nominalPointId);

        }

        [TestMethod]
        [TestCategory("DomainServices"), Description("Base get one actual point by id should return that actual point correctly")]
        public async Task GetActualPointByIdShouldReturnSuccessAsync()
        {
            //Arrange
            MockActualRepository();

            //Act
            var result = await _actualDomainService.GetByIDAsync(_actualPointId, _cancellationToken);

            //Assert
            Assert.IsTrue(result.Id == _actualPointId);

        }

        [TestMethod]
        [TestCategory("DomainServices"), Description("Base create a new actual point should return a new object of actual point with all props created correctly")]
        public async Task CreateNewActualPointShouldReturnSuccessAsync()
        {
            //Arrange
            MockValidator();
            var x = 7;
            var y = 8;
            var z = 9;

            //Act
            var result = await _actualDomainService.CreateNewAsync(x, y, z, _nominalPoint, _cancellationToken);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActualPoint));
            Assert.IsNotNull(result.Id);
            Assert.AreEqual(result.X, x);
            Assert.AreEqual(result.Y, y);
            Assert.AreEqual(result.Z, z);
        }

        [TestMethod]
        [TestCategory("DomainServices"), Description("Base add one actual point should return success")]
        public async Task AddActualPointShouldReturnSuccessAsync()
        {
            //Arrange
            MockActualRepository();
            MockNewActualPoint();
            MockValidator();

            //Act and Assert
            await _actualDomainService.AddAsync(_newActualPoint, _cancellationToken);

        }

        [TestMethod]
        [TestCategory("DomainServices"), Description("Base update one actual point should return success")]
        public async Task UpdateActualPointShouldReturnSuccessAsync()
        {
            //Arrange
            MockActualRepository();
            MockValidator();

            _actualPoint.X = 15;
            _actualPoint.Y = 14;
            _actualPoint.Z = 16;

            //Act and Assert
            await _actualDomainService.UpdateAsync(_actualPoint, _cancellationToken);

        }

        [TestMethod]
        [TestCategory("DomainServices"), Description("Base delete one actual point should return success")]
        public async Task DeleteActualPointShouldReturnSuccessAsync()
        {
            //Arrange
            MockActualRepository();
            MockValidator();
            MockActualRepository();

            //Act and Assert
            await _actualDomainService.DeleteAsync(_actualPoint, _cancellationToken);

        }

        public void MockActualPoint()
        {
            _nominalPoint = new NominalPoint()
            {
                X = 1,
                Y = 2,
                Z = 3
            };

            _nominalPointId = _nominalPoint.Id;

            _actualPoint = new ActualPoint()
            {
                X = 1,
                Y = 2,
                Z = 3,
                NominalPoint = _nominalPoint

            };

            _actualPointId = _actualPoint.Id;

        }

        public void MockNewActualPoint()
        {
            _newActualPoint = new ActualPoint()
            {
                X = 4,
                Y = 5,
                Z = 6,
                NominalPoint = _nominalPoint
            }; 
        }

        public void MockActualRepository()
        {
            var listReturn = new List<ActualPoint>();
            listReturn.Add(_actualPoint);
            listReturn.Add(_newActualPoint);

            _actualPointRepository.GetAsync(_cancellationToken).Returns(listReturn);

            _actualPointRepository.GetByIDAsync(_actualPointId, _cancellationToken).Returns(_actualPoint);

            _actualPointRepository.When(x => x.Add(_newActualPoint));

            _actualPointRepository.When(x => x.Update(_newActualPoint));

            _actualPointRepository.When(x => x.Delete(_newActualPoint));

            _actualPointRepository.SaveAsync(_cancellationToken).Returns(Task.FromResult(1));
        
            _actualPointRepository.GetByNominalIdAsync(_nominalPointId, _cancellationToken).Returns(listReturn);

        }

        public void MockValidator()
        {
            _validator.ValidateAndThrowAsync(_newActualPoint, _cancellationToken).Returns(Task.FromResult(new ValidationResult()));
        }


    }
}