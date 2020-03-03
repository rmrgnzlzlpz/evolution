using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructura.Interfaces
{
    public interface IUnitOfWork
    {
        public bool Init();
        public bool Save();
    }
}
