using FluentAssertions;
using NUnit.Framework;
using System.Linq;

namespace GovUkApiTests
{
    [TestFixture]
    public class GovUkApiUnitTests
    {
        [Test]
        public void GetUsersCityShouldNotReturnAnEmptyArrayForDefaultCity()
        {
            // Arrange
            var objectUnderTest = new GovUkApi.GovUkApi();

            // Act
            var result = objectUnderTest.GetUsersInArea().Result;

            // Assert
            result.Count().Should().NotBe(0);
        }
    }
}
