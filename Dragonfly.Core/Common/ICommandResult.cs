
namespace Dragonfly.Core.Common
{
    public interface ICommandResult
    {
        bool Success { get; set;  }
        string Code { get; set; }
        string Message { get; set; }
    }

    public interface ICommandResult<T> : ICommandResult
    {
        T Result { get; set;  }
    }
}
