using Xunit;
using Moq;
using Domain.Users;
using Domain.Common;

namespace Tests.Domain.Users
{
    public class UsersServiceTest
    {
        private Mock<IUsersRepository> _usersRepository;
        private Mock<ICrypt> _crypt;
        private UsersService _usersService;
        
        public UsersServiceTest()
        {
            _usersRepository = new Mock<IUsersRepository>();
            _crypt = new Mock<ICrypt>();
            _usersService = new UsersService(_usersRepository.Object, _crypt.Object);
        }

        [Fact]
        public void should_not_create_user_when_has_validation_errors()
        {
            var resp = _usersService.Create(Profile.School, "", "");

            Assert.False(resp.IsValid);

            _usersRepository.Verify(
                x => x.Add(It.IsAny<User>()),
                Times.Never()
            );
        }
        
        [Fact]
        public void should_create_user_when_is_valid()
        {
            const string password = "senha123";
            const string cryptedPass = "cryptedPass";
            
            _crypt.Setup(x => x.CreateMD5(password)).Returns(cryptedPass);

            var resp = _usersService.Create(Profile.School, "Escola", password);

            Assert.True(resp.IsValid);
            _usersRepository.Verify(x => x.Add(It.Is<User>(x => 
                x.Profile == Profile.School &&
                x.Username == "Escola" &&
                x.Password == cryptedPass
            )), Times.Once());
        }
    }
}
