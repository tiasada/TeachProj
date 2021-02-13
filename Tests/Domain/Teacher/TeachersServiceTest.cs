using Xunit;
using Moq;
using Domain.Teachers;
using Domain.Common;
using System;
using Domain.Users;

namespace Tests.Domain.Teachers
{
    public class TeachersServiceTest
    {
        private Mock<ITeachersRepository> _teachersRepository;
        private Mock<IUsersService> _usersService;
        private TeachersService _teachersService;
        
        public TeachersServiceTest()
        {
            _teachersRepository = new Mock<ITeachersRepository>();
            _usersService = new Mock<IUsersService>();
            _teachersService = new TeachersService(_teachersRepository.Object, _usersService.Object);
        }

        [Fact]
        public void should_not_create_when_teacher_already_exists()
        {
            var cpf = "61668507005";
            _teachersRepository.Setup(x => x.Get(It.IsAny<Func<Teacher,bool>>())).Returns(new Teacher("", cpf, "", DateTime.Now, ""));
            
            var resp = _teachersService.Create("Jonas Ramos", cpf, "47999992222", DateTime.Now, "jonas.ramos@yahoo.com");

            Assert.False(resp.IsValid);

            _teachersRepository.Verify(
                x => x.Add(It.IsAny<Teacher>()),
                Times.Never()
            );
        }

        [Fact]
        public void should_not_create_teacher_when_email_already_in_use()
        {
            var email = "jonas.ramos@yahoo.com";
            _teachersRepository.SetupSequence(x => x.Get(It.IsAny<Func<Teacher,bool>>()))
                .Returns(null as Teacher)
                .Returns(new Teacher("", "", "", DateTime.Now, email));
            
            var resp = _teachersService.Create("Jonas Ramos", "61668507005", "47999992222", DateTime.Now, "jonas.ramos@yahoo.com");

            Assert.False(resp.IsValid);

            _teachersRepository.Verify(
                x => x.Add(It.IsAny<Teacher>()),
                Times.Never()
            );
        }

        [Fact]
        public void should_not_create_teacher_when_has_validation_errors()
        {
            var resp = _teachersService.Create("", "", "", DateTime.Now, "");

            Assert.False(resp.IsValid);

            _teachersRepository.Verify(
                x => x.Add(It.IsAny<Teacher>()),
                Times.Never()
            );
        }
        
        [Fact]
        public void should_create_teacher_when_is_valid()
        {
            _usersService.Setup(x => x.Create(Profile.Teacher, "61668507005", DateTime.Now.Date.ToString("ddMMyyyy"))).Returns(new CreatedEntityDTO(Guid.NewGuid()));
            _usersService.Setup(x => x.Get(It.IsAny<Guid>())).Returns(new User(Profile.Teacher, "61668507005", DateTime.Now.Date.ToString("ddMMyyyy")));
            
            var resp = _teachersService.Create("Jonas Ramos", "61668507005", "47999992222", DateTime.Now.Date, "jonas.ramos@yahoo.com");

            Assert.True(resp.IsValid);
            _teachersRepository.Verify(x => x.Add(It.Is<Teacher>(x => 
                x.Name == "Jonas Ramos" &&
                x.CPF == "61668507005" &&
                x.PhoneNumber == "47999992222" &&
                x.BirthDate == DateTime.Now.Date &&
                x.Email == "jonas.ramos@yahoo.com"
            )), Times.Once());
        }

        [Fact]
        public void should_create_and_link_user_to_teacher()
        {
            _usersService.Setup(x => x.Create(Profile.Teacher, "61668507005", DateTime.Now.Date.ToString("ddMMyyyy"))).Returns(new CreatedEntityDTO(Guid.NewGuid()));
            _usersService.Setup(x => x.Get(It.IsAny<Guid>())).Returns(new User(Profile.Teacher, "61668507005", DateTime.Now.Date.ToString("ddMMyyyy")));
            
            var resp = _teachersService.Create("Jonas Ramos", "61668507005", "47999992222", DateTime.Now.Date, "jonas.ramos@yahoo.com");

            Assert.True(resp.IsValid);
            _usersService.Verify(x => x.Create(Profile.Teacher, "61668507005", DateTime.Now.Date.ToString("ddMMyyyy")), Times.Once);
            _teachersRepository.Verify(x => x.Add(It.Is<Teacher>(x => 
                x.UserId != null
            )), Times.Once());
        }
    }
}
