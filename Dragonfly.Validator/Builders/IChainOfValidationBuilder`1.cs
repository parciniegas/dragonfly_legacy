
namespace Dragonfly.Validator.Builders
{
    public interface IChainOfValidationBuilder<T>
    {
        IValidationBuilder<T> Next { get; set; }
        IValidationBuilder<T> Previous { get; set; }
        bool StopChainOnError { get; set; }
        string ChainName { get; set; }
    }
}
