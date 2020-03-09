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
        public readonly SqlConnection _sqlConnection;

        public PruebaContext(string stringConnection)
        {
            _sqlConnection = new SqlConnection(stringConnection);
        }

        public bool Close()
        {
            try
            {
                if (_sqlConnection.State == ConnectionState.Closed) return true;
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
                if (_sqlConnection.State == ConnectionState.Open) return true;
                _sqlConnection.Open();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        private IEnumerable<IDataRecord> Get(SqlCommand sqlCommand)
        {
            if (!Open()) return null;
            sqlCommand.Connection = _sqlConnection;
            SqlDataReader reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
            IEnumerable<IDataRecord> dataRecord = reader.Cast<IDataRecord>().ToList();
            reader.Close();
            return dataRecord;
        }

        public int Delete(SqlCommand sqlCommand)
        {
            try
            {
                if (!Open()) return 0;
                sqlCommand.Connection = _sqlConnection;
                sqlCommand.Transaction = _sqlConnection.BeginTransaction();
                int count = sqlCommand.ExecuteNonQuery();
                sqlCommand.Transaction.Commit();
                sqlCommand.Dispose();
                return count;
            }
            catch (Exception e)
            {
                sqlCommand.Transaction.Rollback();
                sqlCommand.Dispose();
                Console.WriteLine(e.Message);
                return 0;
            }
            finally
            {
                Close();
            }
        }

        public IDataRecord Insert(SqlCommand sqlCommand)
        {
            return Get(sqlCommand).FirstOrDefault();
        }

        public IDataRecord Update(SqlCommand sqlCommand)
        {
            return Get(sqlCommand).FirstOrDefault();
        }

        public IDataRecord Find(SqlCommand sqlCommand)
        {
            return Get(sqlCommand).FirstOrDefault();
        }

        public IEnumerable<IDataRecord> Select(SqlCommand sqlCommand)
        {
            return Get(sqlCommand);
        }
    }
}
