using System.Collections.Generic;
using System.Linq;
using Dragonfly.Core.Readers;

namespace Dragonfly.FlatFileHelper
{
    public class CsvReader<T> : IReader<T> where T : new()
    {
        #region Private Fields
        private readonly IRowReader _rowReader;
        private readonly IParser<T> _parser;
        #endregion

        #region Constructors
        public CsvReader(IRowReader rowReader, IParser<T> parser)
        {
            _rowReader = rowReader;
            _parser = parser;
        }
        #endregion

        #region IReader Implementation
        public IEnumerable<T> Read()
        {
            foreach (var row in _rowReader.GetRows())
            {
                if (IsHeaderRow(row)) 
                {
                    _parser.SetHeaders(row);
                    continue;
                }
                var record = _parser.ParseRow(row).Record;
                yield return record;
            }
        }

        public IEnumerable<T> Read(int skip)
        {
            return Read().Skip(skip);
        }

        public IEnumerable<T> Read(int skip, int batch)
        {
            return Read().Skip(skip).Take(batch);
        }
        #endregion

        #region Private Methods
        private bool IsHeaderRow(RowInfo row)
        {
            if (!row.IsHeader) return false;
            _parser.SetHeaders(row);
            return true;
        }
        #endregion
    }
}
