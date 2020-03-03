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
                Console.WriteLine(e.Message);
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
                Console.WriteLine(e.Message);
                return false;
            }
        }

        private IEnumerable<IDataRecord> Get(IDbCommand sqlCommand)
        {
            if (!Open()) yield return null;
            sqlCommand.Connection = _sqlConnection;
            IDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                yield return reader;
            }
            Close();
        }

        public int Delete(IDbCommand sqlCommand)
        {
            try
            {
                if (!Open()) return 0;
                sqlCommand.Connection = _sqlConnection;
                sqlCommand.Transaction = _sqlConnection.BeginTransaction();
                int count = sqlCommand.ExecuteNonQuery();
                sqlCommand.Transaction.Commit();
                return count;
            }
            catch (Exception e)
            {
                sqlCommand.Transaction.Rollback();
                Console.WriteLine(e.Message);
                return 0;
            }
            finally
            {
                Close();
            }
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
