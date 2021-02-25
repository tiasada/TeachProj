using Xunit;
using Moq;
using Domain.Parents;
using Domain.Common;
using System;
using Domain.Users;
using Domain.Students;

namespace Tests.Domain.Parents
{
    public class ParentsServiceTest
    {
        private Student mockStudent = new Student("Filho", "91949385043", "47988885555", DateTime.Now, "filho@gmail.com", "444444");
        
        private Mock<IParentsRepository> _parentsRepository;
        private Mock<IUsersService> _usersService;
        private Mock<IStudentsService> _studentsService;
        private ParentsService _parentsService;
        
        public ParentsServiceTest()
        {
            _parentsRepository = new Mock<IParentsRepository>();
            _usersService = new Mock<IUsersService>();
            _studentsService = new Mock<IStudentsService>();
            _parentsService = new ParentsService(_parentsRepository.Object, _usersService.Object, _studentsService.Object);
        }

        [Fact]
        public void should_not_create_when_parent_already_exists()
        {
            var cpf = "61668507005";
            _parentsRepository.Setup(x => x.Get(It.IsAny<Func<Parent,bool>>())).Returns(new Parent("", cpf, "", DateTime.Now, "", mockStudent));
            
            var resp = _parentsService.Create("Jonas Ramos", cpf, "47999992222", DateTime.Now, "jonas.ramos@yahoo.com", mockStudent.Registration);

            Assert.False(resp.IsValid);

            _parentsRepository.Verify(
                x => x.Add(It.IsAny<Parent>()),
                Times.Never()
            );
        }

        [Fact]
        public void should_not_create_parent_when_email_already_in_use()
        {
            var email = "jonas.ramos@yahoo.com";
            _parentsRepository.SetupSequence(x => x.Get(It.IsAny<Func<Parent,bool>>()))
                .Returns(null as Parent)
                .Returns(new Parent("", "", "", DateTime.Now, email, mockStudent));
            
            var resp = _parentsService.Create("Jonas Ramos", "61668507005", "47999992222", DateTime.Now, "jonas.ramos@yahoo.com", mockStudent.Registration);

            Assert.False(resp.IsValid);

            _parentsRepository.Verify(
                x => x.Add(It.IsAny<Parent>()),
                Times.Never()
            );
        }

        [Fact]
        public void should_not_create_parent_when_student_not_found()
        {
            _parentsRepository.SetupSequence(x => x.Get(It.IsAny<Func<Parent,bool>>()))
                .Returns(null as Parent)
                .Returns(null as Parent);
            
            _studentsService.Setup(x => x.Get(It.IsAny<Func<Student, bool>>())).Returns(null as Student);
            
            var resp = _parentsService.Create("Jonas Ramos", "61668507005", "47999992222", DateTime.Now, "jonas.ramos@yahoo.com", mockStudent.Registration);

            Assert.False(resp.IsValid);

            _parentsRepository.Verify(
                x => x.Add(It.IsAny<Parent>()),
                Times.Never()
            );
        }

        [Fact]
        public void should_not_create_parent_when_has_validation_errors()
        {
            var resp = _parentsService.Create("", "", "", DateTime.Now, "", mockStudent.Registration);

            Assert.False(resp.IsValid);

            _parentsRepository.Verify(
                x => x.Add(It.IsAny<Parent>()),
                Times.Never()
            );
        }
        
        [Fact]
        public void should_create_parent_when_is_valid()
        {
            _usersService.Setup(x => x.Create(Profile.Parent, "61668507005", DateTime.Now.Date.ToString("ddMMyyyy"))).Returns(new CreatedEntityDTO(Guid.NewGuid()));
            _usersService.Setup(x => x.Get(It.IsAny<Guid>())).Returns(new User(Profile.Parent, "61668507005", DateTime.Now.Date.ToString("ddMMyyyy")));
            
            _studentsService.Setup(x => x.Get(It.IsAny<Func<Student, bool>>())).Returns(mockStudent);

            var resp = _parentsService.Create("Jonas Ramos", "61668507005", "47999992222", DateTime.Now.Date, "jonas.ramos@yahoo.com", mockStudent.Registration);

            Assert.True(resp.IsValid);
            _parentsRepository.Verify(x => x.Add(It.Is<Parent>(x => 
                x.Name == "Jonas Ramos" &&
                x.CPF == "61668507005" &&
                x.PhoneNumber == "47999992222" &&
                x.BirthDate == DateTime.Now.Date &&
                x.Email == "jonas.ramos@yahoo.com"
            )), Times.Once());
        }

        [Fact]
        public void should_create_and_link_user_to_parent()
        {
            _usersService.Setup(x => x.Create(Profile.Parent, "61668507005", DateTime.Now.Date.ToString("ddMMyyyy"))).Returns(new CreatedEntityDTO(Guid.NewGuid()));
            _usersService.Setup(x => x.Get(It.IsAny<Guid>())).Returns(new User(Profile.Parent, "61668507005", DateTime.Now.Date.ToString("ddMMyyyy")));
            
            _studentsService.Setup(x => x.Get(It.IsAny<Func<Student, bool>>())).Returns(mockStudent);

            var resp = _parentsService.Create("Jonas Ramos", "61668507005", "47999992222", DateTime.Now.Date, "jonas.ramos@yahoo.com", mockStudent.Registration);

            Assert.True(resp.IsValid);
            _usersService.Verify(x => x.Create(Profile.Parent, "61668507005", DateTime.Now.Date.ToString("ddMMyyyy")), Times.Once);
            _parentsRepository.Verify(x => x.Add(It.Is<Parent>(x => 
                x.UserId != null
            )), Times.Once());
        }
    }
}
