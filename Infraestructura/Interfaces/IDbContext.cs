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
        int ModifierQuery(SqlCommand sql);
        IEnumerable<IDataRecord> Select(SqlCommand sql);
    }
}
