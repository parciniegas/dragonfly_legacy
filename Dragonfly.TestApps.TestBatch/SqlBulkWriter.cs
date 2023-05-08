using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dragonfly.Core;
using Dragonfly.Core.Writers;

namespace Dragonfly.TestApps.TestBatch
{
    public class SqlBulkWriter : IWriter<OutputLog>, IDisposable
    {
        private readonly SqlConnection _sqlConnection;

        public SqlBulkWriter()
        {
            _sqlConnection = new SqlConnection("server=localhost;database=ybroker;Trusted_Connection=True");
            _sqlConnection.Open();
        }

        public void Write(OutputLog item)
        {
            throw new NotImplementedException();
        }

        public void Write(IEnumerable<OutputLog> items)
        {
            var dt = items.ToDataTable();
            var bulkCopy =
                new SqlBulkCopy
                (_sqlConnection,
                    SqlBulkCopyOptions.TableLock |
                    SqlBulkCopyOptions.FireTriggers |
                    SqlBulkCopyOptions.UseInternalTransaction,
                    null
                ) {DestinationTableName = "Log"};
            bulkCopy.WriteToServer(dt);
            dt.Clear();
        }

        public void Dispose()
        {
            _sqlConnection?.Dispose();
        }
    }
}
