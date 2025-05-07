using FluentAssertions;
using NavtorShiper.Validators;
using static NavtorShiper.Tests.TestConstants;

namespace NavtorShiper.Tests
{
    public class ImoValidatorTests
    {
        [Theory]
        [InlineData("7654321")]  // Invalid checksum
        [InlineData("IMO1234")]  // Non-numeric characters
        [InlineData("123")]      // Too short
        [InlineData("12345678")] // Too long
        [InlineData("")]         // Empty string

        public void ValidateImoNumber_WithInvalidImo_ShouldThrowException(string imoNumber)
        {
            Action act = () => ImoValidator.ValidateImoNumber(imoNumber);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"Invalid IMO number: {imoNumber}");
        }

        [Fact]
        public void ValidateImoNumber_WithValidImo_ShouldNotThrow()
        {
            Action act = () => ImoValidator.ValidateImoNumber(ValidImo1);

            act.Should().NotThrow();
        }
    }
}