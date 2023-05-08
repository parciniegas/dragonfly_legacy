using System;
using System.Data.SqlClient;
using Dragonfly.Core.Configuration;

namespace Dragonfly.Core.Sequencer
{
    public class SqlSequencer : ISequencer
    {
        #region Constants
        private const string ConfigConnection = "ConfigConnection";
        private const string GetSetSequenceQuery = "update Sequences with (updlock, rowlock) set Value = Value + 1 output inserted.Prefix, inserted.Value, inserted.Suffix where Name = @name";
        #endregion

        #region Private Fields
        private readonly string _connectionString;
        #endregion

        #region Constructors
        public SqlSequencer(IConfigurator configurator)
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[configurator.GetKey(ConfigConnection)].ConnectionString;
        }

        #endregion

        #region ISequenceProvider Implementation
        public string GetNext(Type type)
        {
            var typeName = type.Name;
            return GetNext(typeName);

        }

        public string GetNext(string type)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                var sequence = cn.ExecuteEntity<Sequence>(GetSetSequenceQuery, new { name = type });
                return $"{sequence.Prefix ?? ""}{sequence.Value}{sequence.Suffix ?? ""}";
            }
        }
        #endregion
    }

    internal class Sequence
    {
        public string Prefix { get; set; }
        public string Value { get; set; }
        public string Suffix { get; set; }
    }
}
