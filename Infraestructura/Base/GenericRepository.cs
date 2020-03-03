using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructura.Interfaces
{
    public abstract class GenericRepository
    {
        protected readonly IDbContext _context;
        public GenericRepository(IDbContext context)
        {
            _context = context;
        }
    }
}
