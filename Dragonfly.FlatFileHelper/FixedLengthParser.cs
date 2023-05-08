using System;
using Dragonfly.Core.Readers;

namespace Dragonfly.FlatFileHelper
{
    public class FixedLengthParser<T> : IParser<T> where T : new()
    {
        public RecordInfo<T> ParseRow(RowInfo row)
        {
            throw new NotImplementedException();
        }

        public void SetHeaders(RowInfo row)
        {
            throw new NotImplementedException();
        }
    }
}
