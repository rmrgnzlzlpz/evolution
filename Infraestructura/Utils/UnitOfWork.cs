using Infraestructura.Interfaces;
using Infraestructura.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructura.Utils
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContext context = new PruebaContext("Data Source=localhost;Initial Catalog=evolution;Integrated Security=True");

        private RoleRepository roleRepository = null;
        private UserRepository userRepository = null;

        public RoleRepository RoleRepository
        {
            get
            {
                if (roleRepository == null)
                {
                    roleRepository = new RoleRepository(context);
                }
                return roleRepository;
            }
        }
        public UserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(context);
                }
                return userRepository;
            }
        }

        public bool Save()
        {
            return context.Close();
        }

        public bool Init()
        {
           return context.Open();
        }
    }
}
