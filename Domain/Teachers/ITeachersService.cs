using System;
using Domain.Common;

namespace Domain.Teachers
{
    public interface ITeachersService : IService<Teacher>
    {
        CreatedEntityDTO Create(string name, string cpf, string phoneNumber, DateTime birthDate, byte[] picture);
    }
}