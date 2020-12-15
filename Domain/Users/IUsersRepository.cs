using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Infra;

namespace Domain.Users
{
    public interface IUsersRepository : IRepository<User>
    {
    }
}