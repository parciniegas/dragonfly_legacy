namespace Dragonfly.Core.Readers
{
    public class DefaultReaderConfiguration : IReaderConfiguration
    {
        public long RowsToSkip { get; set; } = 0;
        public long BatchSize { get; set; } = int.MaxValue;
        private bool _hasHeaders;
        public bool HasHeaders
        {
            get { return _hasHeaders; }
            set
            {
                _hasHeaders = value;
                if (!_hasHeaders)
                    ReturnHeaders = false;
            }
        }
        public string ResourceName { get; set; } = string.Empty;
        public bool ReturnHeaders { get; set; }
        public string Delimiter { get; set; } = ",";
        public bool IsFixedLength { get; set; } = false;
    }
}
