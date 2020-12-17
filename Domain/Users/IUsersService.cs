using System;
using System.Collections.Generic;

namespace Domain.Users
{
    public interface IUsersService
    {
        CreatedUserDTO Create(Profile profile, string username, string password);
        
        User Get(Func<User, bool> predicate);

        IEnumerable<User> GetAll();
    }
}