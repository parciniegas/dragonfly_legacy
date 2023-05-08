using System;
using System.Linq;
using Dragonfly.Core;
using Dragonfly.Core.Readers;

namespace Dragonfly.FlatFileHelper
{
    public class CsvParser<T> : IParser<T> where T : new()
    {
        #region Private Fields
        private string[] _headers;
        private readonly IParserConfiguration _parserConfiguration;
        #endregion

        #region Constructors
        public CsvParser(IParserConfiguration parserConfiguration)
        {
            _parserConfiguration = parserConfiguration;
        }
        #endregion

        #region IParser Implementation
        public void SetHeaders(RowInfo row)
        {
            _headers = row.Row.Split(_parserConfiguration.Delimiter.ToCharArray());
        }

        public RecordInfo<T> ParseRow(RowInfo rowInfo)
        {
            if (_headers == null)
                SetHeadersToIndex(rowInfo.Row);

            return new RecordInfo<T>
            {
                Number = rowInfo.Number,
                Record = SetPropertiesValues(rowInfo.Row)
            };
        }

        private void SetHeadersToIndex(string row)
        {
            var count = row.Split(_parserConfiguration.Delimiter.ToCharArray()).Length;
            _headers = Enumerable.Range(1, count).Select(i => i.ToString()).ToArray();
        }

        private T SetPropertiesValues(string row)
        {
            var entity = new T();
            var values = row.Split(_parserConfiguration.Delimiter.ToCharArray());
            foreach (var keyValue in _headers.Zip(values, (header, value) => new { Header = header, Value = value }))
            {
                var property = entity.GetProperty(keyValue.Header);
                if (property != null)
                {
                    property.SetValue(entity, Convert.ChangeType(keyValue.Value, property.PropertyType), null);
                }
            }

            return entity;
        }
        #endregion
    }
}
