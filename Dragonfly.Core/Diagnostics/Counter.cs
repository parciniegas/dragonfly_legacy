using System.Diagnostics;

namespace Dragonfly.Core.Diagnostics
{
    public class Counter
    {
        #region Private Fields
        private readonly PerformanceCounter _counter;
        #endregion

        #region Constructors
        /// <summary>
        ///
        /// </summary>
        /// <param name="counter"></param>
        public Counter(PerformanceCounter counter)
        {
            _counter = counter;
        }
        #endregion

        #region Properties
        public string Category => _counter.CategoryName;
        /// <summary>
        ///
        /// </summary>
        public string Name => _counter.CounterName;

        /// <summary>
        ///     
        /// </summary>
        public bool ReadOnly => _counter.ReadOnly;
        /// <summary>
        /// 
        /// </summary>
        public string MachineName => _counter.MachineName;
        /// <summary>
        /// 
        /// </summary>
        public long Value => _counter.RawValue;
        /// <summary>
        /// 
        /// </summary>
        public string Instance => _counter.InstanceName;
        /// <summary>
        /// 
        /// </summary>
        public PerformanceCounterType Type => _counter.CounterType;
        #endregion

        #region Public Methods
        /// <summary>
        ///
        /// </summary>
        public void Increment()
        {
            _counter.IncrementBy(1);
        }

        /// <summary>
        ///
        /// </summary>
        public void Reset()
        {
            _counter.RawValue = 0;
        }
        #endregion
    }
}
