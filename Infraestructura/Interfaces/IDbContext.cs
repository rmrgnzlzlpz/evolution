using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Infraestructura.Interfaces
{
    public interface IDbContext
    {
        bool Open();
        bool Close();
        IDataRecord Insert(IDbCommand sqlCommand);
        IDataRecord Update(IDbCommand sqlCommand);
        IDataRecord Find(IDbCommand sqlCommand);
        int Delete(IDbCommand sqlCommand);
        IEnumerable<IDataRecord> Select(IDbCommand sqlCommand);
    }
}
