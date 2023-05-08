using System.Collections.Generic;
using Dragonfly.Core.Readers;
using FileHelpers;

namespace Dragonfly.TestApps.TestBatch
{
    public class FileHelpersReader : IReader<Product>
    {
        #region Private Fields
        private readonly string _fileName;
        #endregion

        #region Constructors
        public FileHelpersReader(string fileName)
        {
            _fileName = fileName;
        }
        #endregion

        #region IReader Implementation
        public IEnumerable<Product> Read()
        {
            var engine = new FileHelperAsyncEngine<Product>();
            using (engine.BeginReadFile(_fileName))
            {
                foreach (var prod in engine)
                {
                    yield return prod;
                }
            }
        }

        public IEnumerable<Product> Read(int skip)
        {
            var count = 0;
            var engine = new FileHelperAsyncEngine<Product>();
            using (engine.BeginReadFile(_fileName))
            {
                foreach (var prod in engine)
                {
                    count++;
                    if (count <= skip) continue;
                    yield return prod;
                }
            }
        }

        public IEnumerable<Product> Read(int skip, int batch)
        {
            var count = 0;
            var lbatch = 0;
            var engine = new FileHelperAsyncEngine<Product>();
            using (engine.BeginReadFile(_fileName))
            {
                foreach (var prod in engine)
                {
                    count++;
                    if (count <= skip) continue;
                    lbatch++;
                    if (lbatch > batch) break;
                    yield return prod;
                }
            }
        } 
        #endregion
    }
}
