using System;
using System.Collections.Generic;

namespace Domain.Users
{
    public interface IUsersService
    {
        CreatedUserDTO Create(Profile profile, string username, string password);
        
        User GetByID(Guid id);

        IEnumerable<User> GetAll();
    }
}