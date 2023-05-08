using System;
using System.Collections.Generic;
using System.Linq;
using Dragonfly.Batch.Filters;
using Dragonfly.Batch.Processer;
using Dragonfly.Core.ArgumentGuard;
using Dragonfly.Core.Readers;
using Dragonfly.Core.Writers;

namespace Dragonfly.Batch
{
    public class DataFlow<TInput, TOutput> : IDataFlow
    {
        #region Private Fields
        private readonly List<IFilter<TInput>> _preFilters;
        private readonly List<IFilter<TOutput>> _postFilters;
        private IProcesser<TInput, TOutput> _processer;
        private IReader<TInput> _reader;
        private readonly List<IWriter<TOutput>> _writers;
        //internal ErrorAction<TInput, TOutput> ErrorAction;
        internal List<Action<Exception>> ErrorActions;
        #endregion

        #region Constructors
        public DataFlow()
        {
            _preFilters = new List<IFilter<TInput>>();
            _postFilters = new List<IFilter<TOutput>>();
            _writers = new List<IWriter<TOutput>>();
        }

        public DataFlow(IEnumerable<IFilter<TInput>> preFilters, IProcesser<TInput, TOutput> processer, IEnumerable<IWriter<TOutput>> writers, IReader<TInput> reader, IEnumerable<IFilter<TOutput>> postFilters)
        {
            _preFilters = preFilters.ToList();
            _processer = processer;
            _writers = writers.ToList();
            _reader = reader;
            _postFilters = postFilters.ToList();
        }
        #endregion

        #region IFlow Implementation
        public void Execute()
        {
            Guard.Check(_processer, nameof(_processer)).IsNotNull();
            Guard.Check(_reader, nameof(_reader)).IsNotNull();
            Guard.Check(_writers, nameof(_writers)).IsNotNull();

            foreach (var row in _reader.Read())
            {
                try
                {
                    var preFiltered = ApplyPreFilterers(row);
                    if (preFiltered == null) continue;
                    var processed = _processer.Process(preFiltered);
                    var postFiltered = ApplyPostFilterers(processed);
                    if (postFiltered == null) continue;
                    _writers.ForEach(w => w.Write(postFiltered));
                }
                catch (Exception ex)
                {
                    if (ErrorActions == null) throw;
                    foreach (var errorAction in ErrorActions)
                    {
                        errorAction(ex);
                    }
                }
            }
        }

        public void Execute(int rowsToSkip)
        {
            Guard.Check(_processer, nameof(_processer)).IsNotNull();
            Guard.Check(_reader, nameof(_reader)).IsNotNull();
            Guard.Check(_writers, nameof(_writers)).IsNotNull();

            foreach (var row in _reader.Read(rowsToSkip))
            {
                var preFiltered = ApplyPreFilterers(row);
                if (preFiltered == null) continue;
                var processed = _processer.Process(preFiltered);
                var postFiltered = ApplyPostFilterers(processed);
                if (postFiltered == null) continue;
                _writers.ForEach(w => w.Write(postFiltered));
            }
        }

        public void Execute(int rowsToSkip, int batchSize)
        {
            Guard.Check(_processer, nameof(_processer)).IsNotNull();
            Guard.Check(_reader, nameof(_reader)).IsNotNull();
            Guard.Check(_writers, nameof(_writers)).IsNotNull();

            var counter = 0;
            var processedRows = new List<TOutput>();
            foreach (var row in _reader.Read())
            {
                var preFiltered = ApplyPreFilterers(row);
                if (preFiltered == null) continue;
                var processed = _processer.Process(preFiltered);
                var postFiltered = ApplyPostFilterers(processed);
                if (postFiltered == null) continue;
                processedRows.Add(postFiltered);
                counter++;
                if (counter < batchSize) continue;
                _writers.ForEach(w => w.Write(processedRows));
                processedRows.Clear();
                counter = 0;
            }
            if (counter > 0)
                _writers.ForEach(w => w.Write(processedRows));
        }

        public DataFlow<TInput, TOutput> AddPreFilter(IFilter<TInput> filter)
        {
            Guard.Check(filter, nameof(filter)).IsNotNull();
            _preFilters.Add(filter);
            return this;
        }

        public DataFlow<TInput, TOutput> AddPostFilter(IFilter<TOutput> filter)
        {
            Guard.Check(filter, nameof(filter)).IsNotNull();
            _postFilters.Add(filter);
            return this;
        }

        public DataFlow<TInput, TOutput> SetReader(IReader<TInput> reader)
        {
            Guard.Check(reader, nameof(reader)).IsNotNull();
            _reader = reader;
            return this;
        }

        public DataFlow<TInput, TOutput> SetWriter(IWriter<TOutput> writer)
        {
            Guard.Check(writer, nameof(writer)).IsNotNull();
            _writers.Add(writer);
            return this;
        }

        public DataFlow<TInput, TOutput> SetProcesser(IProcesser<TInput, TOutput> processer)
        {
            Guard.Check(processer, nameof(processer)).IsNotNull();
            _processer = processer;
            return this;
        }

        public ErrorAction<TInput, TOutput> OnError()
        {
            ErrorActions = new List<Action<Exception>>();
            return new ErrorAction<TInput, TOutput>(this);
        }
        #endregion

        #region Private Methods
        private TInput ApplyPreFilterers(TInput item)
        {
            foreach (var preFilter in _preFilters)
            {
                item = preFilter.Filter(item);
                if (item == null)
                    break;
            }
            //Prueba 1.1
            return item;
        }

        private TOutput ApplyPostFilterers(TOutput item)
        {
            foreach (var filter in _postFilters)
            {
                item = filter.Filter(item);
                if (item == null)
                    break;
            }

            return item;
        }
        #endregion
    }
}
