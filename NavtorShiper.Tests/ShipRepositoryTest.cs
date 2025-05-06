using System;
using System.Collections.Generic;
using FluentAssertions;
using NavtorShiper.Entities;
using NavtorShiper.Repositories;
using Xunit;

namespace NavtorShiper.Tests
{
    public class ShipRepositoryTests
    {
        private const string ValidImo1 = "1234567";
        private const string ValidImo2 = "9074729";
        private const string InvalidImo = "7654321";
        private readonly IShipRepository _repository;
        private readonly PassengerShip _testShip1;
        private readonly TankerShip _testShip2;

        public ShipRepositoryTests()
        {
            _repository = new ShipRepository();
            _testShip1 = new PassengerShip(ValidImo1, "Ocean Queen", 200, 30);
            _testShip2 = new TankerShip(ValidImo2, "Oil Baron", 250, 40);
        }

        [Fact]
        public void GetAll_WhenEmpty_ReturnsEmptyCollection()
        {
            var result = _repository.GetAll();
            result.Should().BeEmpty();
        }

        [Fact]
        public void GetAll_WithShips_ReturnsAllShips()
        {
            _repository.Add(_testShip1);
            _repository.Add(_testShip2);

            var result = _repository.GetAll();

            result.Should().HaveCount(2)
                .And.Contain(_testShip1)
                .And.Contain(_testShip2);
        }

        [Fact]
        public void GetById_WhenShipExists_ReturnsShip()
        {
            _repository.Add(_testShip1);

            var result = _repository.GetById(ValidImo1);

            result.Should().Be(_testShip1);
        }

        [Fact]
        public void GetById_WhenShipNotExists_ReturnsNull()
        {
            var result = _repository.GetById(InvalidImo);
            result.Should().BeNull();
        }

        [Fact]
        public void Add_NewValidShip_AddsToRepository()
        {
            _repository.Add(_testShip1);

            var result = _repository.GetById(ValidImo1);
            result.Should().Be(_testShip1);
        }

        [Fact]
        public void Add_DuplicateShip_ThrowsException()
        {
            _repository.Add(_testShip1);

            Action act = () => _repository.Add(_testShip1);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"Ship with IMO {ValidImo1} already exists.");
        }

        [Fact]
        public void Delete_ExistingShip_RemovesFromRepository()
        {
            _repository.Add(_testShip1);

            var result = _repository.Delete(ValidImo1);

            result.Should().BeTrue();
            _repository.GetById(ValidImo1).Should().BeNull();
        }

        [Fact]
        public void Delete_NonExistingShip_ReturnsFalse()
        {
            var result = _repository.Delete(InvalidImo);
            result.Should().BeFalse();
        }

        [Fact]
        public void Repository_WithMultipleInstances_ShouldMaintainSeparateEntities()
        {
            var repo1 = new ShipRepository();
            var repo2 = new ShipRepository();

            repo1.Add(_testShip1);
            repo2.Add(_testShip2);

            repo1.GetById(ValidImo1).Should().Be(_testShip1);
            repo1.GetById(ValidImo2).Should().BeNull();
            repo2.GetById(ValidImo2).Should().Be(_testShip2);
            repo2.GetById(ValidImo1).Should().BeNull();
        }
    }
}