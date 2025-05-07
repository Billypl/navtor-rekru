using Xunit;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using NavtorShiper.Entities;
using NavtorShiper.Services;
using static NavtorShiper.Tests.TestConstants;

namespace NavtorShiper.Tests.Services
{
    public class PassengerShipServiceTests
    {
        private const int PassengerId = 1;
        private readonly Passenger passenger;
        private readonly PassengerShip emptyShip;
        private readonly PassengerShip shipWithPassenger;

        private readonly PassengerShipService _service;
        private readonly Mock<IShipService> _shipServiceMock;

        public PassengerShipServiceTests()
        {
            _shipServiceMock = new Mock<IShipService>();
            _service = new PassengerShipService(_shipServiceMock.Object);
            passenger = new Passenger(PassengerId, "John", "Doe");
            emptyShip = new PassengerShip(ValidImo1, "Empty passenger ship", 100, 20);
            shipWithPassenger = new PassengerShip(ValidImo2, "Passenger ship with passenger", 100, 20, new List<Passenger> { passenger });
        }

        [Fact]
        public void AddPassenger_WhenPassengerDoesNotExist_AddsPassenger()
        {
            _shipServiceMock.Setup(s => s.GetById(ValidImo1)).Returns(emptyShip);

            _service.AddPassenger(ValidImo1, passenger);

            emptyShip.Passengers.Should().ContainSingle(p => p.Id == PassengerId);
        }

        [Fact]
        public void AddPassenger_WhenPassengerAlreadyExists_ThrowsArgumentException()
        {
            _shipServiceMock.Setup(s => s.GetById(ValidImo1)).Returns(shipWithPassenger);

            var act = () => _service.AddPassenger(ValidImo1, passenger);

            act.Should().Throw<ArgumentException>()
               .WithMessage($"Passenger with id {PassengerId} already exists.");
        }

        [Fact]
        public void RemovePassenger_WhenPassengerExists_RemovesPassenger()
        {
            _shipServiceMock.Setup(s => s.GetById(ValidImo1)).Returns(shipWithPassenger);

            _service.RemovePassenger(ValidImo1, PassengerId);

            shipWithPassenger.Passengers.Should().BeEmpty();
        }

        [Fact]
        public void RemovePassenger_WhenPassengerNotFound_ThrowsArgumentException()
        {
            _shipServiceMock.Setup(s => s.GetById(ValidImo1)).Returns(emptyShip);

            var act = () => _service.RemovePassenger(ValidImo1, PassengerId);

            act.Should().Throw<ArgumentException>()
               .WithMessage($"Passenger with id {PassengerId} not found.");
        }
    }
}
