using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dragonfly.Core;
using Dragonfly.Core.Writers;

namespace Dragonfly.TestApps.TestBatch
{
    public class SqlWriter : IWriter<OutputLog>, IDisposable
    {
        private readonly SqlConnection _sqlConnection;
        private readonly Action<OutputLog> _action;

        public SqlWriter(Action<OutputLog> action = null)
        {
            _action = action;
            _sqlConnection = new SqlConnection("server=localhost;database=ybroker;Trusted_Connection=True");
            _sqlConnection.Open();
        }

        public void Write(OutputLog item)
        {
            _sqlConnection.ExecuteNonQuery($"insert into Log(Date, Level, Message, Logger, Callsite) values ('{item.Date.ToIso()}', '{item.Level}','{item.Message}', '{item.Logger}', '{item.Callsite}')");

            _action?.Invoke(item);
        }

        public void Write(IEnumerable<OutputLog> items)
        {
            using (var tx = _sqlConnection.BeginTransaction())
            {
                try
                {
                    foreach (var item in items)
                    {
                        _sqlConnection.ExecuteNonQuery($"insert into Log(Date, Level, Message, Logger, Callsite) values ('{item.Date.ToIso()}', '{item.Level}','{item.Message}', '{item.Logger}', '{item.Callsite}')", CommandType.Text, tx);
                    }
                    tx.Commit();

                }
                catch
                {
                    tx.Rollback();
                }
            }
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _sqlConnection?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~SqlWriter()
        {
            Dispose(false);
        }
    }
}
