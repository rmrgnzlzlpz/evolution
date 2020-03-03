using Domain;
using Domain.Entities;
using Domain.Interfaces;
using Infraestructura.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Infraestructura.Utils
{
    public class PruebaContext : IDbContext
    {
        public readonly IDbConnection _sqlConnection;

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

        public int ModifierQuery(IDbCommand sqlCommand)
        {
            sqlCommand.Connection = _sqlConnection;
            sqlCommand.Transaction = _sqlConnection.BeginTransaction();
            int count = sqlCommand.ExecuteNonQuery();
            sqlCommand.Transaction.Commit();
            return count;
        }

        private IEnumerable<IDataRecord> Get(IDbCommand sqlCommand)
        {
            sqlCommand.Connection = _sqlConnection;
            IDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                yield return reader;
            }
        }

        public int Delete(IDbCommand sqlCommand)
        {
            sqlCommand.Connection = _sqlConnection;
            sqlCommand.Transaction = _sqlConnection.BeginTransaction();
            int count = sqlCommand.ExecuteNonQuery();
            sqlCommand.Transaction.Commit();
            return count;
        }

        public IDataRecord Insert(IDbCommand sqlCommand)
        {
            return Get(sqlCommand).FirstOrDefault();
        }

        public IDataRecord Update(IDbCommand sqlCommand)
        {
            return Get(sqlCommand).FirstOrDefault();
        }

        public IDataRecord Find(IDbCommand sqlCommand)
        {
            return Get(sqlCommand).FirstOrDefault();
        }

        public IEnumerable<IDataRecord> Select(IDbCommand sqlCommand)
        {
            return Get(sqlCommand);
        }
    }
}
