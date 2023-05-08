namespace Dragonfly.Batch
{
    public interface IDataFlow
    {
        void Execute();
        void Execute(int rowsToSkip, int batchSize);

    }
}
