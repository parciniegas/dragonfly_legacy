namespace Dragonfly.Core.Readers
{
    public class RecordInfo<T> where T : new()
    {
        public long Number { get; set; }
        public T Record { get; set; }
    }
}
