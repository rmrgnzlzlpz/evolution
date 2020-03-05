using Aplicacion.Base;
using Domain.Entities;
using Infraestructura.Interfaces;
using Infraestructura.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Services
{
    public class RoleService : Service<Role>
    {
        public RoleService(UnitOfWork unitOfWork, IRepository<Role> repository) : base(unitOfWork, repository)
        {
        }

        public override Role Create(Role entity)
        {
            if (entity == null) throw new Exception("User Entity empty or null");
            if (_repository.FindBy(nameof(entity.Name), entity.Name).Count > 0)
            {
                return null;
            }
            return base.Create(entity);
        }
    }
}
