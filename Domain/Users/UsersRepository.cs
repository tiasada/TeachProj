using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Infra;

namespace Domain.Users
{
    class UsersRepository
    {
        public void Add(User user)
        {
            using (var db = new TeachContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public User GetByID(Guid id)
        {
            using (var db = new TeachContext())
            {
                return db.Users.FirstOrDefault(x => x.Id == id);
            }
        }

        public User GetByCPF(string cpf)
        {
            using (var db = new TeachContext())
            {
                return db.Users.FirstOrDefault(x => x.CPF == cpf);
            }
        }

        public IEnumerable<User> GetAll()
        {
            using (var db = new TeachContext())
            {
                return db.Users.ToList();
            }
        }
    }
}