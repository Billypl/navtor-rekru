using System;
using FluentAssertions;
using Moq;
using NavtorShiper.Entities;
using NavtorShiper.Repositories;
using NavtorShiper.Services;
using Xunit;

namespace NavtorShiper.Tests
{
    public class TankerShipServiceTests
    {
        private const string ValidImo = "9074729";
        private const string InvalidImo = "9074728";
        private const int ExistingTankId = 1;
        private const int NonExistingTankId = 999;
        private const double TankCapacity = 1000;
        private const double RefuelAmount = 500;

        private readonly Mock<IShipRepository> _shipRepositoryMock;
        private readonly Mock<ITank> _tankMock;
        private readonly TankerShipService _service;

        public TankerShipServiceTests()
        {
            _shipRepositoryMock = new Mock<IShipRepository>();
            _tankMock = new Mock<ITank>();
            _service = new TankerShipService(_shipRepositoryMock.Object);
        }

        [Fact]
        public void RefuelTank_WhenShipNotFound_ThrowsException()
        {
            _shipRepositoryMock.Setup(x => x.GetById(InvalidImo)).Returns(null as Ship);

            Action action = () => _service.RefuelTank(InvalidImo, ExistingTankId, FuelType.Diesel, RefuelAmount);

            action.Should()
                .Throw<ArgumentException>()
                .WithMessage($"Ship with IMO {InvalidImo} not found.");
        }

        [Fact]
        public void RefuelTank_WhenTankNotFound_ThrowsException()
        {
            var tankerShip = CreateTestTankerShip();
            _shipRepositoryMock.Setup(x => x.GetById(ValidImo)).Returns(tankerShip);

            Action action = () => _service.RefuelTank(ValidImo, NonExistingTankId, FuelType.Diesel, RefuelAmount);

            action.Should()
                .Throw<ArgumentException>()
                .WithMessage($"Tank with ID {NonExistingTankId} not found in ship with IMO {ValidImo}.");
        }

        [Fact]
        public void RefuelTank_WithValidParameters_CallsTankRefuel()
        {
            _tankMock.Setup(t => t.Id).Returns(ExistingTankId);
            var tankerShip = CreateTestTankerShip(_tankMock.Object);
            _shipRepositoryMock.Setup(x => x.GetById(ValidImo)).Returns(tankerShip);

            _service.RefuelTank(ValidImo, ExistingTankId, FuelType.HeavyFuel, RefuelAmount);

            _tankMock.Verify(t => t.Refuel(FuelType.HeavyFuel, RefuelAmount), Times.Once);
        }

        [Fact]
        public void EmptyTank_WithValidTank_CallsTankEmpty()
        {
            _tankMock.Setup(t => t.Id).Returns(ExistingTankId);
            var tankerShip = CreateTestTankerShip(_tankMock.Object);
            _shipRepositoryMock.Setup(x => x.GetById(ValidImo)).Returns(tankerShip);

            _service.EmptyTank(ValidImo, ExistingTankId);

            _tankMock.Verify(t => t.Empty(), Times.Once);
        }

        private TankerShip CreateTestTankerShip(ITank? tank = null)
        {
            var tankerShip = new TankerShip(ValidImo, "Oil Tanker", 150, 30);
            if (tank != null)
            {
                tankerShip.Tanks.Add(tank.Id, tank);
            }
            return tankerShip;
        }
    }
}