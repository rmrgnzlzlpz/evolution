using Infraestructura.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Infraestructura.Utils
{
    public class PruebaContext : IDbContext
    {
        public readonly SqlConnection _sqlConnection;

        public PruebaContext(string stringConnection)
        {
            _sqlConnection = new SqlConnection(stringConnection);
        }

        public bool Close()
        {
            try
            {
                _sqlConnection.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool Open()
        {
            try
            {
                _sqlConnection.Open();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public int ModifierQuery(SqlCommand sqlCommand)
        {
            sqlCommand.Connection = _sqlConnection;
            sqlCommand.Transaction = _sqlConnection.BeginTransaction();
            int count = sqlCommand.ExecuteNonQuery();
            sqlCommand.Transaction.Commit();
            return count;
        }

        public IEnumerable<IDataRecord> Select(SqlCommand sqlCommand)
        {
            sqlCommand.Connection = _sqlConnection;
            IDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                yield return reader;
            }
        }
    }
}
