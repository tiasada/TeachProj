using Xunit;
using Domain.Teachers;
using System;

namespace Tests.Domain.Teachers
{
    public class TeacherTest
    {
        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData(null)]
        [InlineData("A B C")]
        [InlineData("Jonas 1326")]
        [InlineData("J0NAS")]
        public void Should_return_false_when_teacher_name_invalid(string name)
        {
            var teacher = new Teacher(name, "61668507005", "47999992222", DateTime.Now, "jonas.ramos@yahoo.com");
            
            var teacherIsValid = teacher.Validate().isValid;

            Assert.False(teacherIsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData(null)]
        [InlineData("4444444444")]
        [InlineData("123")]
        [InlineData("616.685.070-05")]
        public void Should_return_false_when_teacher_cpf_invalid(string cpf)
        {
            var teacher = new Teacher("Jonas Ramos", cpf, "47999992222", DateTime.Now, "jonas.ramos@yahoo.com");
            
            var teacherIsValid = teacher.Validate().isValid;

            Assert.False(teacherIsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData("jonas")]
        [InlineData("jonas.ramos")]
        [InlineData("jonas.ramos@")]
        [InlineData("jonas.ramoscom")]
        [InlineData("jonas.ramos.com")]
        [InlineData("jonas.ramos@.com")]
        [InlineData("jonas.ramos@com")]
        [InlineData("jonasramosyahoocombr")]
        public void Should_return_false_when_teacher_email_invalid(string email)
        {
            var teacher = new Teacher("Jonas Ramos", "61668507005", "47999992222", DateTime.Now, email);
            
            var teacherIsValid = teacher.Validate().isValid;

            Assert.False(teacherIsValid);
        }
    }
}
