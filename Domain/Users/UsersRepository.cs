using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Infra;

namespace Domain.Users
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IRepository<User> _repository;
        public UsersRepository(IRepository<User> repository)
        {
            _repository = repository;
        }

        public void Add(User user)
        {
            _repository.Add(user);
        }

        public Guid? Remove(Guid id)
        {
            return _repository.Remove(id);
        }

        public User Get(Func<User, bool> predicate)
        {
            return _repository.Get(predicate);
        }

        public IEnumerable<User> GetAll()
        {
            return _repository.GetAll();
        }
    }
}