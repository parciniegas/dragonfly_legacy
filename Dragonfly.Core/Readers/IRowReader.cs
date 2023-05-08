using System.Collections.Generic;

namespace Dragonfly.Core.Readers
{
    public interface IRowReader
    {
        IReaderConfiguration ReaderConfiguration { get; set; }
        RowInfo GetRow();
        IEnumerable<RowInfo> GetRows(); 
    }
}
