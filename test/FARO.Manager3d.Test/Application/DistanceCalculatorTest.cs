using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FARO.Manager3d.Application.Factory;
using FARO.Manager3d.Application.Factory.Interfaces;
using FARO.Manager3d.Application.Tasks;
using FARO.Manager3d.Application.Tasks.Interfaces;
using FARO.Manager3d.Domain.DomainService.Interfaces;
using FARO.Manager3d.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace FARO.Manager3d.Test.Application
{
    [TestClass]
    public class DistanceCalculatorTest
    {
        private CancellationToken _cancellationToken;

        private IDistanceCalculatorFactory _distanceCalculatorFactory;
        private IActualDomainService _actualDomainService;

        private NominalPoint _nominalPoint;
        private Guid _nominalPointId;
        private List<ActualPoint> _actualPoints;

        [TestInitialize]
        public void Initializer()
        {
            _cancellationToken = CancellationToken.None;
            _actualDomainService = Substitute.For<IActualDomainService>();
            _distanceCalculatorFactory= new DistanceCalculatorFactory(_actualDomainService);
        }

        [TestMethod]
        [TestCategory("Application - Distance Calculator"), Description("Given that sent sync as parameter to get an instance of calculator should return the instance correctly")]
        public void GetInstaceSyncCalculatorShouldSuccess()
        {
            //Arrange
            var method = "sync";
            //Act
            var instance = _distanceCalculatorFactory.GetCalculator(method);
            //Assert
            Assert.IsInstanceOfType(instance, typeof(SyncDistanceCalculator));
        }

        [TestMethod]
        [TestCategory("Application - Distance Calculator"), Description("Given that sent async as parameter to get an instance of calculator should return the instance correctly")]
        public void GetInstaceAsyncCalculatorShouldSuccess()
        {
            //Arrange
            var method = "async";
            //Act
            var instance = _distanceCalculatorFactory.GetCalculator(method);
            //Assert
            Assert.IsInstanceOfType(instance, typeof(AsyncDistanceCalculator));
        }

        [TestMethod]
        [TestCategory("Application - Distance Calculator"), Description("Given that sent a unexist method as parameter to get an instance of calculator should throw a new error")]
        public void GetUnexistInstaceCalculatorShouldError()
        {
            //Arrange
            var method = "unexist";
            //Act and Assert
            Assert.ThrowsException<ArgumentException>(() => _distanceCalculatorFactory.GetCalculator(method));
        }

        [TestMethod]
        [TestCategory("Application - Distance Calculator"), Description("Given sent a nominal point with sync method should return all actual points with their respective distances")]
        [DataTestMethod]
        [DataRow(1.0,2.0,3,3,0,2,3)]
        [DataRow(123,1,0,123.0901,1,0.00093001, 0.090105)]
        [DataRow(45.5,244,184.8,46.199,243.9999,185, 0.72705)]
        public async Task CalculateDistanceFromSyncMethodShouldSuccess(double nominalX,double nominalY,double nominalZ,double actualX,double actualY,double actualZ, double distance)
        {
            //Arrange
            MockNominalPoint( nominalX, nominalY, nominalZ);
            AddMockActualDomainService(actualX, actualY, actualZ);
            MockActualDomainService();
    
            var method = "sync";
            var instance = _distanceCalculatorFactory.GetCalculator(method);
            //Act
            var result = await instance.CalculateAsync(_nominalPoint, _cancellationToken);
            //Assert
            Assert.AreEqual(result.First().Item2, distance);
        }

        [TestMethod]
        [TestCategory("Application - Distance Calculator"), Description("Given sent a nominal point with async method should return all actual points with their respective distances")]
        [DataTestMethod]
        [DataRow(1.0,2.0,3,3,0,2,3)]
        [DataRow(123,1,0,123.0901,1,0.00093001, 0.090105)]
        [DataRow(45.5,244,184.8,46.199,243.9999,185, 0.72705)]
        public async Task CalculateDistanceFromAsyncMethodShouldSuccess(double nominalX,double nominalY,double nominalZ,double actualX,double actualY,double actualZ, double distance)
        {
            //Arrange
            MockNominalPoint(nominalX, nominalY, nominalZ);
            AddMockActualDomainService(actualX, actualY, actualZ);
            MockActualDomainService();
    
            var method = "async";
            var instance = _distanceCalculatorFactory.GetCalculator(method);
            //Act
            var result = await instance.CalculateAsync(_nominalPoint, _cancellationToken);
            //Assert
            Assert.AreEqual(result.First().Item2, distance);

            Console.WriteLine("AHHHHHHHHHHHHH  :   " + result.First().Item2);
        }

        public void MockActualDomainService()
        {
            _actualDomainService
                .GetByNominalPointAsync(_nominalPointId,_cancellationToken)
                .Returns(_actualPoints);

        }

        public void MockNominalPoint(double x, double y, double z)
        {
            _nominalPoint = new NominalPoint()
            {
                X =x,
                Y =y,
                Z =z
            };

            _nominalPointId = _nominalPoint.Id;

        }

        public void AddMockActualDomainService(double x, double y, double z)
        {
            if (_actualPoints == null)
            {
                _actualPoints = new List<ActualPoint>();
            }
            _actualPoints.Add(new ActualPoint()
            {
                X =x,
                Y =y,
                Z =z
            });

        }

    }
}