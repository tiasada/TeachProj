using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Infra;

namespace Domain.Users
{
    public class UsersRepository : Repository<User>, IUsersRepository
    {
    }
}