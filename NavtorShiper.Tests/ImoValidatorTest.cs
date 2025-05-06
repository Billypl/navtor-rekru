using FluentAssertions;
using NavtorShiper.Utils;

namespace NavtorShiper.Tests
{
    public class ImoValidatorTests
    {
        [Theory]
        [InlineData("9074729", true)]   // Valid IMO number
        [InlineData("7654321", false)]  // Invalid checksum
        [InlineData("IMO1234", false)]  // Non-numeric characters
        [InlineData("123", false)]      // Too short
        [InlineData("12345678", false)] // Too long
        [InlineData("", false)]         // Empty string
        public void IsValidImoNumber_ValidatesCorrectly(string imoNumber, bool expected)
        {
            var result = ImoValidator.IsValidImoNumber(imoNumber);
            result.Should().Be(expected);
        }
    }
}