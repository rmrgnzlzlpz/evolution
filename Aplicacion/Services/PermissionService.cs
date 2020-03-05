using Aplicacion.Base;
using Aplicacion.Interface;
using Domain.Entities;
using Infraestructura.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Infraestructura.Utils;

namespace Aplicacion.Services
{
    public class PermissionService : Service<Permission>
    {
        public PermissionService(UnitOfWork unitOfWork, IRepository<Permission> repository) : base(unitOfWork, repository)
        {
        }

        public override Permission Create(Permission entity)
        {
            if (entity == null) throw new Exception("User Entity empty or null");
            return base.Create(entity);
        }

        public bool IsOfRole(long roleId, string permissionName)
        {
            Permission permission = (FindBy("name", permissionName) ?? new List<Permission>()).FirstOrDefault();
            if (permission == null) return false;
            List<RolePermission> entities = _unitOfWork.RolePermissionRepository.FindBy("role_id", roleId).ToList();
            return (entities.Where(per => per.PermissionId == permission.Id).ToList().Count > 0) ? true : false;
        }
    }
}