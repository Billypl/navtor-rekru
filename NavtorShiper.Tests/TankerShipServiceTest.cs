using FluentAssertions;
using Moq;
using NavtorShiper.Entities;
using NavtorShiper.Services;
using static NavtorShiper.Tests.TestConstants;

namespace NavtorShiper.Tests.Services
{
    public class TankerShipServiceTests
    {
        private readonly Mock<IShipService> _shipServiceMock;
        private readonly TankerShipService _service;

        public TankerShipServiceTests()
        {
            _shipServiceMock = new Mock<IShipService>();
            _service = new TankerShipService(_shipServiceMock.Object);
        }

        [Fact]
        public void RefuelTank_WhenTankNotFound_ThrowsException()
        {
            var tankerShip = CreateTestTankerShip();
            _shipServiceMock.Setup(x => x.GetById(ValidImo1)).Returns(tankerShip);

            Action action = () => _service.RefuelTank(ValidImo1, NonExistingTankId, FuelType.Diesel, HalfCapacityRefuelAmount);

            action.Should()
                .Throw<ArgumentException>()
                .WithMessage($"Tank with ID {NonExistingTankId} not found in ship with IMO {ValidImo1}.");
        }

        [Fact]
        public void RefuelTank_FirstTime_SetsTypeAndIncreasesLevel()
        {
            var tank = CreateTestTank();
            var tankerShip = CreateTestTankerShip(tank);
            _shipServiceMock.Setup(x => x.GetById(ValidImo1)).Returns(tankerShip);

            _service.RefuelTank(ValidImo1, ExistingTankId, DefaultFuelType, HalfCapacityRefuelAmount);

            tank.Type.Should().Be(DefaultFuelType);
            tank.CurrentLevel.Should().Be(HalfCapacityRefuelAmount);
        }

        [Fact]
        public void RefuelTank_WithSameType_AddsFuel()
        {
            var tank = CreateTestTank();
            var tankerShip = CreateTestTankerShip(tank);
            _shipServiceMock.Setup(x => x.GetById(ValidImo1)).Returns(tankerShip);

            _service.RefuelTank(ValidImo1, ExistingTankId, DefaultFuelType, HalfCapacityRefuelAmount);
            _service.RefuelTank(ValidImo1, ExistingTankId, DefaultFuelType, HalfCapacityRefuelAmount);

            tank.CurrentLevel.Should().Be(HalfCapacityRefuelAmount + HalfCapacityRefuelAmount);
        }

        [Fact]
        public void RefuelTank_WithDifferentFuelType_ThrowsException()
        {
            var tank = CreateTestTank();
            var tankerShip = CreateTestTankerShip(tank);
            _shipServiceMock.Setup(x => x.GetById(ValidImo1)).Returns(tankerShip);

            _service.RefuelTank(ValidImo1, ExistingTankId, DefaultFuelType, HalfCapacityRefuelAmount);
            Action action = () => _service.RefuelTank(ValidImo1, ExistingTankId, OtherFuelType, HalfCapacityRefuelAmount);
            action.Should()
                .Throw<InvalidOperationException>()
                .WithMessage($"Tank with ID {tank.Id} cannot be refueled with {OtherFuelType}. Current type is {tank.Type}.");
        }

        [Fact]
        public void RefuelTank_AboveMaxCapacity_ThrowsException()
        {
            var tank = CreateTestTank();
            var tankerShip = CreateTestTankerShip(tank);
            _shipServiceMock.Setup(x => x.GetById(ValidImo1)).Returns(tankerShip);

            _service.RefuelTank(ValidImo1, ExistingTankId, DefaultFuelType, HalfCapacityRefuelAmount);
            Action action = () => _service.RefuelTank(ValidImo1, ExistingTankId, DefaultFuelType, MoreThanHalfCapacityRefuelAmount);
            action.Should()
                .Throw<InvalidOperationException>()
                .WithMessage($"Tank with ID {tank.Id} cannot be refueled with {MoreThanHalfCapacityRefuelAmount} units. Capacity exceeded.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(NegativeRefuelAmount)]
        public void RefuelTank_WithBadAmount_ThrowsException(double amount)
        {
            var tank = CreateTestTank();
            var tankerShip = CreateTestTankerShip(tank);
            _shipServiceMock.Setup(x => x.GetById(ValidImo1)).Returns(tankerShip);

            Action action = () => _service.RefuelTank(ValidImo1, ExistingTankId, DefaultFuelType, amount);
            action.Should()
                .Throw<ArgumentException>()
                .WithMessage($"Wrong amount of fuel: {amount}.");
        }

        [Fact]
        public void EmptyTank_ResetsTypeAndDecreasesLevel()
        {
            var tank = CreateTestTank();
            var tankerShip = CreateTestTankerShip(tank);
            _shipServiceMock.Setup(x => x.GetById(ValidImo1)).Returns(tankerShip);

            _service.EmptyTank(ValidImo1, ExistingTankId);

            tank.Type.Should().Be(FuelType.None);
            tank.CurrentLevel.Should().Be(0);
        }

        private Tank CreateTestTank()
        {
            return new Tank(ExistingTankId, DefaultCapacity);
        }

        private TankerShip CreateTestTankerShip(Tank? tank = null)
        {
            var tankerShip = new TankerShip(ValidImo1, "Oil Tanker", 150, 30);
            if (tank != null)
            {
                tankerShip.Tanks.Add(tank);
            }
            return tankerShip;
        }
    }
}