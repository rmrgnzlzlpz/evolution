using Aplicacion.Base;
using Domain.Entities;
using Infraestructura.Interfaces;
using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infraestructura.Utils;

namespace Aplicacion.Services
{
    public class UserService : Service<User>
    {
        public UserService(UnitOfWork unitOfWork, IRepository<User> repository) : base(unitOfWork, repository)
        {
        }

        public override User Create(User user)
        {
            if (user == null) throw new Exception("User Entity empty or null");
            if (_repository.FindBy(nameof(user.Username), user.Username).Count > 0)
            {
                return null;
            }
            user.Password = new PasswordHasher<User>().HashPassword(user, user.Password);
            user.State = UserState.Active;
            return base.Create(user);
        }

        public bool ValidatePassword(User user, User entity)
        {
            var verifier = new PasswordHasher<User>().VerifyHashedPassword(user, entity.Password, user.Password);
            return verifier == PasswordVerificationResult.Success;
        }

        public User GetUser(User user)
        {
            User entity = ((_repository.FindBy("username", user.Username)) ?? new List<User>()).FirstOrDefault();
            if (entity == null) return null;
            return ValidatePassword(user, entity) ? entity : null;
        }

        public bool HasPermission(User user, string permission)
        {
            if (user == null) return false;
            PermissionService permissionService = new PermissionService(_unitOfWork, _unitOfWork.PermissionRepository);
            return permissionService.IsOfRole(user.RoleId, permission);
        }
    }
}
