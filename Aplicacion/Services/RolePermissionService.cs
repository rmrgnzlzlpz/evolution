using Aplicacion.Base;
using Domain.Entities;
using Infraestructura.Interfaces;
using Infraestructura.Utils;

namespace Aplicacion.Services
{
    public class RolePermissionService : Service<RolePermission>
    {
        public RolePermissionService(UnitOfWork unitOfWork, IRepository<RolePermission> repository) : base(unitOfWork, repository)
        {
        }
    }
}