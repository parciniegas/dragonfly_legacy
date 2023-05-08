namespace Dragonfly.Batch
{
    public class ErrorActionReport<TInput, TOutput>
    {
        #region Private Fields
        private readonly DataFlow<TInput, TOutput> _dataFlow;
        private readonly ErrorAction<TInput, TOutput> _errorAction;
        #endregion

        #region Constructors
        public ErrorActionReport(DataFlow<TInput, TOutput> dataFlow, ErrorAction<TInput, TOutput> errorAction)
        {
            _dataFlow = dataFlow;
            _errorAction = errorAction;
        }
        #endregion

        #region Public Methods
        public ErrorAction<TInput, TOutput> AndThen()
        {
            return _errorAction;
        }
        #endregion
    }
}
