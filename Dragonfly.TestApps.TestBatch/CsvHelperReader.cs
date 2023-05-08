using System.Collections.Generic;
using CsvHelper;
using Dragonfly.Core.Readers;

namespace Dragonfly.TestApps.TestBatch
{
    public class CsvHelperReader<T> : IReader<T>
    {
        #region Properties
        private readonly ICsvReader _csvReader;
        #endregion

        #region Constructors
        public CsvHelperReader(ICsvReader csvReader)
        {
            _csvReader = csvReader;
        }
        #endregion

        #region IReader Implementation
        public IEnumerable<T> Read()
        {
            while (_csvReader.Read())
            {
                yield return _csvReader.GetRecord<T>();
            }
        }

        public IEnumerable<T> Read(int skip)
        {
            for (var i = 0; i < skip; i++)
            {
                if (!_csvReader.Read())
                    break;
            }

            while (_csvReader.Read())
            {
                yield return _csvReader.GetRecord<T>();
            }
        }

        public IEnumerable<T> Read(int skip, int batch)
        {
            for (var i = 0; i < skip; i++)
            {
                if (!_csvReader.Read())
                    break;
            }

            for (var i = 0; i < batch; i++)
            {
                if (!_csvReader.Read())
                    break;
                yield return _csvReader.GetRecord<T>();
            }
        }
        #endregion
    }
}
