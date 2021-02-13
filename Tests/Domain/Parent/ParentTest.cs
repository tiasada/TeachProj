using Xunit;
using Domain.Parents;
using System;
using Domain.Students;

namespace Tests.Domain.Parents
{
    public class ParentTest
    {
        private Student mockStudent = new Student("Filho", "91949385043", "47988885555", DateTime.Now, "filho@gmail.com", "444444");
        
        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData(null)]
        [InlineData("A B C")]
        [InlineData("Jonas 1326")]
        [InlineData("J0NAS")]
        public void Should_return_false_when_parent_name_invalid(string name)
        {
            var parent = new Parent(name, "61668507005", "47999992222", DateTime.Now, "jonas.ramos@yahoo.com", mockStudent);
            
            var parentIsValid = parent.Validate().isValid;

            Assert.False(parentIsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData(null)]
        [InlineData("4444444444")]
        [InlineData("123")]
        [InlineData("616.685.070-05")]
        public void Should_return_false_when_parent_cpf_invalid(string cpf)
        {
            var parent = new Parent("Jonas Ramos", cpf, "47999992222", DateTime.Now, "jonas.ramos@yahoo.com", mockStudent);
            
            var parentIsValid = parent.Validate().isValid;

            Assert.False(parentIsValid);
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
        public void Should_return_false_when_parent_email_invalid(string email)
        {
            var parent = new Parent("Jonas Ramos", "61668507005", "47999992222", DateTime.Now, email, mockStudent);
            
            var parentIsValid = parent.Validate().isValid;

            Assert.False(parentIsValid);
        }
    }
}
