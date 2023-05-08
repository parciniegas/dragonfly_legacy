using System;

namespace Dragonfly.Core.Readers
{
    public interface IParser<T> where T : new()
    {
        void SetHeaders(RowInfo row);
        RecordInfo<T> ParseRow(RowInfo row);
    }
}
