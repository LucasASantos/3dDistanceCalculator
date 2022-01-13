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
    public class NominalDomainServiceTest
    {
        private INominalDomainService _nominalDomainService;
        private IValidator<NominalPoint> _validator;
        private INominalPointRepository _nominalPointRepository;
        private IActualPointRepository _actualPointRepository;
        private CancellationToken _cancellationToken;

        private NominalPoint _nominalPoint;
        private NominalPoint _newNominalPoint;
        private Guid _nominalPointId;

        [TestInitialize]
        public void Initializer()
        {
            _cancellationToken = CancellationToken.None;

            _actualPointRepository = Substitute.For<IActualPointRepository>();
            _validator = Substitute.For<IValidator<NominalPoint>>();
            _nominalPointRepository = Substitute.For<INominalPointRepository>();


            _nominalDomainService = new NominalDomainService(_validator, _nominalPointRepository, _actualPointRepository);

            MockNominalPoint();

        }

        [TestMethod]
        [TestCategory("DomainServices"), Description("Base get nominal points should return all nominal points")]
        public async Task GetAllNominalPointsShouldReturnSuccessAsync()
        {
            //Arrange
            MockNominalRepository();

            //Act
            var result = await _nominalDomainService.GetAsync(_cancellationToken);

            //Assert
            Assert.IsTrue(result.Any());

        }

        [TestMethod]
        [TestCategory("DomainServices"), Description("Base get one nominal point by id should return that nominal point correctly")]
        public async Task GetNominalPointByIdShouldReturnSuccessAsync()
        {
            //Arrange
            MockNominalRepository();

            //Act
            var result = await _nominalDomainService.GetByIDAsync(_nominalPointId, _cancellationToken);

            //Assert
            Assert.IsTrue(result.Id == _nominalPointId);

        }

        [TestMethod]
        [TestCategory("DomainServices"), Description("Base create a new nominal point should return a new object of nominal point with all props created correctly")]
        public async Task CreateNewNominalPointShouldReturnSuccessAsync()
        {
            //Arrange
            MockValidator();
            var x = 7;
            var y = 8;
            var z = 9;

            //Act
            var result = await _nominalDomainService.CreateNewAsync(x, y, z, _cancellationToken);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NominalPoint));
            Assert.IsNotNull(result.Id);
            Assert.AreEqual(result.X, x);
            Assert.AreEqual(result.Y, y);
            Assert.AreEqual(result.Z, z);
        }

        [TestMethod]
        [TestCategory("DomainServices"), Description("Base add one nominal point should return success")]
        public async Task AddNominalPointShouldReturnSuccessAsync()
        {
            //Arrange
            MockNewNominalPoint();
            MockNominalRepository();
            MockValidator();

            //Act and Assert
            await _nominalDomainService.AddAsync(_newNominalPoint, _cancellationToken);

        }

        [TestMethod]
        [TestCategory("DomainServices"), Description("Base update one nominal point should return success")]
        public async Task UpdateNominalPointShouldReturnSuccessAsync()
        {
            //Arrange
            MockNominalRepository();
            MockValidator();

            _nominalPoint.X = 15;
            _nominalPoint.Y = 14;
            _nominalPoint.Z = 16;

            //Act and Assert
            await _nominalDomainService.UpdateAsync(_nominalPoint, _cancellationToken);

        }

        [TestMethod]
        [TestCategory("DomainServices"), Description("Base delete one nominal point should return success")]
        public async Task DeleteNominalPointShouldReturnSuccessAsync()
        {
            //Arrange
            MockNominalRepository();
            MockValidator();
            MockActualRepository();

            //Act and Assert
            await _nominalDomainService.DeleteAsync(_nominalPoint, _cancellationToken);

        }

        public void MockNominalPoint()
        {
            _nominalPoint = new NominalPoint()
            {
                X = 1,
                Y = 2,
                Z = 3
            };

            _nominalPointId = _nominalPoint.Id;

        }

        public void MockNewNominalPoint()
        {
            _newNominalPoint = new NominalPoint()
            {
                X = 4,
                Y = 5,
                Z = 6
            };
        }

        public void MockNominalRepository()
        {
            var listReturn = new List<NominalPoint>();
            listReturn.Add(_nominalPoint);
            _nominalPointRepository.GetAsync(_cancellationToken).Returns(listReturn);

            _nominalPointRepository.GetByIDAsync(_nominalPointId, _cancellationToken).Returns(_nominalPoint);

            _nominalPointRepository.When(x => x.Add(_newNominalPoint));

            _nominalPointRepository.When(x => x.Update(_newNominalPoint));

            _nominalPointRepository.When(x => x.Delete(_newNominalPoint));

            _nominalPointRepository.SaveAsync(_cancellationToken).Returns(Task.FromResult(1));
        }

        public void MockActualRepository()
        {
            var listReturn = new List<ActualPoint>();
            listReturn.Add(new ActualPoint()
            {
                X = 35,
                Y = 36,
                Z = 37,
                NominalPoint = _nominalPoint
            });

            _actualPointRepository.GetByNominalIdAsync(_nominalPointId, _cancellationToken).Returns(listReturn);


            _actualPointRepository.When(x => x.DeleteRange(listReturn));
        }

        public void MockValidator()
        {
            _validator.ValidateAndThrowAsync(_newNominalPoint, _cancellationToken).Returns(Task.FromResult(new ValidationResult()));
        }

    }
}