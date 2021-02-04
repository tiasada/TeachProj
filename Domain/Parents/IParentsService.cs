using System;
using Domain.Common;

namespace Domain.Parents
{
    public interface IParentsService : IService<Parent>
    {
        CreatedEntityDTO Create(string name, string cpf, string phoneNumber, DateTime birthDate, string registration);
    }
}