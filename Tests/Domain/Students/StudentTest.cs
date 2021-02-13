using Xunit;
using Domain.Students;
using System;

namespace Tests.Domain.Students
{
    public class StudentTest
    {
        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData(null)]
        [InlineData("A B C")]
        [InlineData("Jonas 1326")]
        [InlineData("J0NAS")]
        public void Should_return_false_when_student_name_invalid(string name)
        {
            var student = new Student(name, "61668507005", "47999992222", DateTime.Now, "jonas.ramos@yahoo.com", "444444");
            
            var studentIsValid = student.Validate().isValid;

            Assert.False(studentIsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData(null)]
        [InlineData("4444444444")]
        [InlineData("123")]
        [InlineData("616.685.070-05")]
        public void Should_return_false_when_student_cpf_invalid(string cpf)
        {
            var student = new Student("Jonas Ramos", cpf, "47999992222", DateTime.Now, "jonas.ramos@yahoo.com", "444444");
            
            var studentIsValid = student.Validate().isValid;

            Assert.False(studentIsValid);
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
        public void Should_return_false_when_student_email_invalid(string email)
        {
            var student = new Student("Jonas Ramos", "61668507005", "47999992222", DateTime.Now, email, "444444");
            
            var studentIsValid = student.Validate().isValid;

            Assert.False(studentIsValid);
        }
    }
}
