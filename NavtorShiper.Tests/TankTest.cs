using System;
using FluentAssertions;
using Microsoft.VisualBasic.FileIO;
using NavtorShiper.Entities;
using Xunit;

namespace NavtorShiper.Tests
{
    public class TankTests
    {
        private const int DefaultTankId = 1;
        private const double DefaultCapacity = 1000;
        private const FuelType DefaultFuelType = FuelType.Diesel;
        private const FuelType OtherFuelType = FuelType.HeavyFuel;
        private const double InitialFuelAmount = 500;
        private const double AdditionalFuelAmount = 300;

        private Tank CreateTestTank() => new Tank(DefaultTankId, DefaultCapacity);

        [Fact]
        public void Refuel_FirstTime_SetsTypeAndIncreasesLevel()
        {
            var tank = CreateTestTank();
            
            tank.Refuel(DefaultFuelType, InitialFuelAmount);

            tank.Type.Should().Be(DefaultFuelType);
            tank.CurrentLevel.Should().Be(InitialFuelAmount);
        }

        [Fact]
        public void Refuel_WithSameType_AddsFuel()
        {
            var tank = CreateTestTank();
            
            tank.Refuel(DefaultFuelType, InitialFuelAmount);
            tank.Refuel(DefaultFuelType, AdditionalFuelAmount);
            
            tank.CurrentLevel.Should().Be(InitialFuelAmount + AdditionalFuelAmount);
        }

        [Fact]
        public void Refuel_WithDifferentType_ThrowsException()
        {
            var tank = CreateTestTank();
            
            tank.Refuel(DefaultFuelType, InitialFuelAmount);
            Action act = () => tank.Refuel(OtherFuelType, AdditionalFuelAmount);

            act.Should().Throw<InvalidOperationException>().WithMessage($"Tank with ID {tank.Id} cannot be refueled with {OtherFuelType}. Current type is {DefaultFuelType}.");
        }

        [Fact]
        public void Refuel_ExceedingCapacity_ThrowsException()
        {
            var tank = CreateTestTank();
            var nearlyFullAmount = DefaultCapacity - 100;
            var overfillAmount = 150;
            tank.Refuel(DefaultFuelType, nearlyFullAmount);
            
            Action act = () => tank.Refuel(DefaultFuelType, overfillAmount);

            act.Should().Throw<InvalidOperationException>().WithMessage($"Tank with ID {tank.Id} cannot be refueled with {overfillAmount} units. Capacity exceeded.");
        }

        [Fact]
        public void Refuel_NegativeAmount_ThrowsException()
        {
            var tank = CreateTestTank();
            var negativeAmount = -100;
            
            Action act = () => tank.Refuel(DefaultFuelType, negativeAmount);

            act.Should().Throw<ArgumentException>().WithMessage("Tank cannot be refueled with a negative amount of fuel.");
        }

        [Fact]
        public void Empty_SetsCurrentLevelToZeroAndResetsType()
        {
            var tank = new Tank(DefaultTankId, DefaultFuelType, DefaultCapacity, InitialFuelAmount);
            tank.Empty();

            tank.CurrentLevel.Should().Be(0);
            tank.Type.Should().Be(FuelType.None);
        }
    }
}