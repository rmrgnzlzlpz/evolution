using Infraestructura.Interfaces;
using Infraestructura.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructura.Utils
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContext context;

        public UnitOfWork(string connection = "Data Source=localhost;Initial Catalog=evolution;Integrated Security=True")
        {
            context = new PruebaContext(connection);
        }

        private RoleRepository roleRepository = null;
        private UserRepository userRepository = null;
        private PermissionRepository permissionRepository = null;
        private RolePermissionRepository rolePermissionRepository = null;

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

        public PermissionRepository PermissionRepository
        {
            get
            {
                if (permissionRepository == null)
                {
                    permissionRepository = new PermissionRepository(context);
                }
                return permissionRepository;
            }
        }

        public RolePermissionRepository RolePermissionRepository
        {
            get
            {
                if (rolePermissionRepository == null)
                {
                    rolePermissionRepository = new RolePermissionRepository(context);
                }
                return rolePermissionRepository;
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
