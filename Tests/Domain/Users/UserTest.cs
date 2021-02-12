using Xunit;
using Domain.Users;

namespace Tests.Domain.Users
{
    public class UserTest
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("Escola", "   ")]
        [InlineData(null, "senha123")]
        public void Should_return_false_when_username_or_password_invalid(string username, string password)
        {
            var user = new User(Profile.School, username, password);
            
            var userIsValid = user.Validate().isValid;

            Assert.False(userIsValid);
        }

        [Theory]
        [InlineData("Escola", "senha123")]
        [InlineData("35C0L4 FR3D3R1C0 R31", "S3nh4!@#%")]
        [InlineData("12345678910", "12345678910")]
        public void Should_return_true_when_username_and_password_valid(string username, string password)
        {
            var user = new User(Profile.School, username, password);
            
            var userIsValid = user.Validate().isValid;

            Assert.True(userIsValid);
        }
    }
}
