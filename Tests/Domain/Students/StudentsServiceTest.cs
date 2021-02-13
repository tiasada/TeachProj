using Xunit;
using Moq;
using Domain.Students;
using Domain.Common;
using System;
using Domain.Users;

namespace Tests.Domain.Students
{
    public class StudentsServiceTest
    {
        private Mock<IStudentsRepository> _studentsRepository;
        private Mock<IUsersService> _usersService;
        private StudentsService _studentsService;
        
        public StudentsServiceTest()
        {
            _studentsRepository = new Mock<IStudentsRepository>();
            _usersService = new Mock<IUsersService>();
            _studentsService = new StudentsService(_studentsRepository.Object, _usersService.Object);
        }

        [Fact]
        public void should_not_create_when_student_already_exists()
        {
            var cpf = "61668507005";
            var registration = "4544";
            _studentsRepository.Setup(x => x.Get(It.IsAny<Func<Student,bool>>())).Returns(new Student("", cpf, "", DateTime.Now, "", registration));
            
            var resp = _studentsService.Create("Jonas Ramos", cpf, "47999992222", DateTime.Now, "jonas.ramos@yahoo.com", registration);

            Assert.False(resp.IsValid);

            _studentsRepository.Verify(
                x => x.Add(It.IsAny<Student>()),
                Times.Never()
            );
        }

        [Fact]
        public void should_not_create_student_when_email_already_in_use()
        {
            var email = "jonas.ramos@yahoo.com";
            _studentsRepository.SetupSequence(x => x.Get(It.IsAny<Func<Student,bool>>()))
                .Returns(null as Student)
                .Returns(new Student("", "", "", DateTime.Now, email, ""));
            
            var resp = _studentsService.Create("Jonas Ramos", "61668507005", "47999992222", DateTime.Now, "jonas.ramos@yahoo.com", "4444");

            Assert.False(resp.IsValid);

            _studentsRepository.Verify(
                x => x.Add(It.IsAny<Student>()),
                Times.Never()
            );
        }

        [Fact]
        public void should_not_create_student_when_has_validation_errors()
        {
            var resp = _studentsService.Create("", "", "", DateTime.Now, "", "");

            Assert.False(resp.IsValid);

            _studentsRepository.Verify(
                x => x.Add(It.IsAny<Student>()),
                Times.Never()
            );
        }
        
        [Fact]
        public void should_create_student_when_is_valid()
        {
            _usersService.Setup(x => x.Create(Profile.Student, "444444", "61668507005")).Returns(new CreatedEntityDTO(Guid.NewGuid()));
            _usersService.Setup(x => x.Get(It.IsAny<Guid>())).Returns(new User(Profile.Student, "444444", "61668507005"));
            
            var resp = _studentsService.Create("Jonas Ramos", "61668507005", "47999992222", DateTime.Now.Date, "jonas.ramos@yahoo.com", "444444");

            Assert.True(resp.IsValid);
            _studentsRepository.Verify(x => x.Add(It.Is<Student>(x => 
                x.Name == "Jonas Ramos" &&
                x.CPF == "61668507005" &&
                x.PhoneNumber == "47999992222" &&
                x.BirthDate == DateTime.Now.Date &&
                x.Email == "jonas.ramos@yahoo.com" &&
                x.Registration == "444444"
            )), Times.Once());
        }

        [Fact]
        public void should_create_and_link_user_to_student()
        {
            _usersService.Setup(x => x.Create(Profile.Student, "444444", "61668507005")).Returns(new CreatedEntityDTO(Guid.NewGuid()));
            _usersService.Setup(x => x.Get(It.IsAny<Guid>())).Returns(new User(Profile.Student, "444444", "61668507005"));
            
            var resp = _studentsService.Create("Jonas Ramos", "61668507005", "47999992222", DateTime.Now.Date, "jonas.ramos@yahoo.com", "444444");

            Assert.True(resp.IsValid);
            _usersService.Verify(x => x.Create(Profile.Student, "444444", "61668507005"), Times.Once);
            _studentsRepository.Verify(x => x.Add(It.Is<Student>(x => 
                x.UserId != null
            )), Times.Once());
        }
    }
}
