using FluentAssertions;
using Moq;
using NavtorShiper.Entities;
using NavtorShiper.Repositories;
using NavtorShiper.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace NavtorShiper.Tests.Services
{
    public class ShipServiceTests
    {
        private const string ValidImo1 = "1234567";
        private const string ValidImo2 = "8814275";
        private const string InvalidImo = "7654321";
        private readonly Mock<IShipRepository> _shipRepositoryMock;
        private readonly ShipService _shipService;
        private readonly Ship testShip1;
        private readonly Ship testShip2;

        public ShipServiceTests()
        {
            _shipRepositoryMock = new Mock<IShipRepository>();
            _shipService = new ShipService(_shipRepositoryMock.Object);
            testShip1 = new TankerShip(ValidImo1, "Test Ship", 100, 20);
            testShip2 = new PassengerShip(ValidImo2, "Test Ship", 100, 20);
        }

        [Fact]
        public void GetAll_ReturnsAllShipsFromRepository()
        {
            var expectedShips = new List<Ship> { testShip1, testShip2 };
            _shipRepositoryMock.Setup(x => x.GetAll()).Returns(expectedShips);

            var result = _shipService.GetAll();

            result.Should().BeEquivalentTo(expectedShips);
            _shipRepositoryMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public void GetById_WithExistingShip_ReturnsShip()
        {
            _shipRepositoryMock.Setup(x => x.GetById(ValidImo1)).Returns(testShip1);

            var result = _shipService.GetById(ValidImo1);

            result.Should().Be(testShip1);
            _shipRepositoryMock.Verify(x => x.GetById(ValidImo1), Times.Once);
        }

        [Fact]
        public void GetById_WithNonExistingShip_ThrowsArgumentException()
        {
            _shipRepositoryMock.Setup(x => x.GetById(InvalidImo)).Returns(null as Ship);

            Action act = () => _shipService.GetById(InvalidImo);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"Ship with IMO {InvalidImo} not found.");
            _shipRepositoryMock.Verify(x => x.GetById(InvalidImo), Times.Once);
        }

        [Fact]
        public void Add_WithNewShip_AddsToRepository()
        {
            _shipRepositoryMock.Setup(x => x.GetById(ValidImo1)).Returns((Ship)null);

            _shipService.Add(testShip1);

            _shipRepositoryMock.Verify(x => x.Add(testShip1), Times.Once);
            _shipRepositoryMock.Verify(x => x.GetById(ValidImo1), Times.Once);
        }

        [Fact]
        public void Add_WithExistingShip_ThrowsArgumentException()
        {
            _shipRepositoryMock.Setup(x => x.GetById(ValidImo1)).Returns(testShip1);

            Action act = () => _shipService.Add(testShip1);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"Ship with IMO {ValidImo1} already exists.");
            _shipRepositoryMock.Verify(x => x.Add(It.IsAny<Ship>()), Times.Never);
        }

        [Fact]
        public void Delete_WithExistingShip_DeletesFromRepository()
        {
            _shipRepositoryMock.Setup(x => x.GetById(ValidImo1)).Returns(testShip1);

            _shipService.Delete(ValidImo1);

            _shipRepositoryMock.Verify(x => x.Delete(testShip1), Times.Once);
        }

        [Fact]
        public void Delete_WithNonExistingShip_ThrowsArgumentException()
        {
            _shipRepositoryMock.Setup(x => x.GetById(InvalidImo)).Returns(null as Ship);

            Action act = () => _shipService.Delete(InvalidImo);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"Ship with IMO {InvalidImo} not found.");
            _shipRepositoryMock.Verify(x => x.Delete(It.IsAny<Ship>()), Times.Never);
        }
    }
}