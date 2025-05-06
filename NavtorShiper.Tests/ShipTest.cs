using FluentAssertions;
using NavtorShiper.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NavtorShiper.Tests
{
    public class ShipTest
    {
        private const string ValidImoNumber = "1234567";
        private const string InvalidImoNumber = "7654321";
        private const double ValidLength = 100.0;
        private const double ValidWidth = 100.0;
        private const double InvalidLength = -100.0;
        private const double InvalidWidth = -100.0;
        private const string ShipName = "Test Ship";

        [Fact]
        public void Constructor_WithValidParameters_CreatesShip()
        {
            var ship = new TankerShip(ValidImoNumber, ShipName, ValidLength, ValidWidth);

            ship.ImoNumber.Should().Be(ValidImoNumber);
            ship.Name.Should().Be(ShipName);
            ship.Length.Should().Be(ValidLength);
            ship.Width.Should().Be(ValidWidth);
        }

        [Fact]
        public void Constructor_WithInvalidImoNumber_ThrowsArgumentException()
        {
            Action act = () => new TankerShip(InvalidImoNumber, ShipName, ValidLength, ValidWidth);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"Invalid IMO number: {InvalidImoNumber}");
        }

        [Theory]
        [InlineData(InvalidLength, ValidWidth)]
        [InlineData(ValidLength, InvalidWidth)]
        [InlineData(InvalidLength, InvalidWidth)]
        public void Constructor_WithInvalidSize_ThrowsArgumentException(int length, int width)
        {
            Action act = () => new TankerShip(ValidImoNumber, ShipName, length, width);

            act.Should().Throw<ArgumentException>()
                .WithMessage("Length and width must be positive numbers.");
        }
    }
}
