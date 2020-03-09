using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Infraestructura.Interfaces
{
    public interface IDbContext
    {
        bool Open();
        bool Close();
        IDataRecord Insert(SqlCommand sqlCommand);
        IDataRecord Update(SqlCommand sqlCommand);
        IDataRecord Find(SqlCommand sqlCommand);
        int Delete(SqlCommand sqlCommand);
        IEnumerable<IDataRecord> Select(SqlCommand sqlCommand);
    }
}
