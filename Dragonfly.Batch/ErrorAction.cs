using System;

namespace Dragonfly.Batch
{
    public class ErrorAction<TInput, TOutput>
    {
        #region Private Fields
        private readonly DataFlow<TInput, TOutput> _dataFlow;
        #endregion

        #region Constructors
        public ErrorAction(DataFlow<TInput, TOutput> dataFlow)
        {
            _dataFlow = dataFlow;
        }
        #endregion

        #region Public Methods
        public DataFlow<TInput, TOutput> Continue()
        {
            _dataFlow.ErrorActions.Add(exception => {return;});
            return _dataFlow;
        }

        public ErrorActionReport<TInput, TOutput> Report(Action<Exception> action)
        {
            _dataFlow.ErrorActions.Add(action);
            return new ErrorActionReport<TInput, TOutput>(_dataFlow, this);
        }

        public void ThrowException()
        {
            _dataFlow.ErrorActions.Add(exception => { throw exception; });
        }
        #endregion
    }
}
