using System;
using System.Collections.Generic;
using System.IO;
using Dragonfly.Core.ArgumentGuard;
using Dragonfly.Core.Readers;

namespace Dragonfly.FlatFileHelper
{
    public class FlatFileReader : IRowReader, IDisposable
    {
        #region Private Fields
        private long _currentRowNumber;
        private RowInfo _currentRowData;
        private StreamReader _sr;
        #endregion

        #region Constructors
        public FlatFileReader(IReaderConfiguration readerConfiguration = null)
        {
            ReaderConfiguration = readerConfiguration ?? new DefaultReaderConfiguration();
        }
        #endregion

        #region IFileReader Implementation
        public IReaderConfiguration ReaderConfiguration { get; set; }

        public bool Read()
        {
            NextRow();
            return _currentRowData != null;
        }

        public RowInfo GetRow()
        {
            return _currentRowData;
        }

        public IEnumerable<RowInfo> GetRows()
        {
            var rowReaded = Read();
            while (rowReaded)
            {
                yield return GetRow();
                rowReaded = Read();
            }
        }
        #endregion

        #region Public Methods
        public FlatFileReader FileName(string fileName)
        {
            ReaderConfiguration.ResourceName = fileName;
            return this;
        }

        public FlatFileReader HasHeaders(bool hasHeaders)
        {
            ReaderConfiguration.HasHeaders = hasHeaders;
            if (hasHeaders) _currentRowNumber = -1;
            return this;
        }
        #endregion

        #region Private Methods
        private StreamReader GetReader()
        {
            if (_sr != null) return _sr;

            Guard.Check(ReaderConfiguration.ResourceName, nameof(ReaderConfiguration.ResourceName));
            _sr = new StreamReader(ReaderConfiguration.ResourceName);
            return _sr;
        }

        private void NextRow()
        {
            _currentRowNumber++;
            var row = GetReader().ReadLine();
            _currentRowData = (row == null) ? null : new RowInfo { Number = _currentRowNumber, Row = row, IsHeader = ReaderConfiguration.HasHeaders && _currentRowNumber == 0};
        }

        public void Dispose()
        {
            _sr?.Dispose();
        }
        #endregion
    }
}
