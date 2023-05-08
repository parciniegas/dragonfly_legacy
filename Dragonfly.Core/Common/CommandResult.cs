// ReSharper disable MemberCanBeProtected.Global
namespace Dragonfly.Core.Common
{
    public class CommandResult : ICommandResult
    {
        #region Properties
        public bool Success { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        #endregion Properties

        #region Constructor

        public CommandResult(bool success)
        {
            Code = "0xNA";
            Success = success;
        }

        public CommandResult(bool success, string code)
        {
            Success = success;
            Code = code;
        }

        public CommandResult(bool success, string code, string message)
        {
            Success = success;
            Code = code;
            Message = message;
        }

        public CommandResult(string errorMessage)
        {
            Success = false;
            Code = "0xNA";
            Message = errorMessage;
        }

        public CommandResult(string errorMessage, string code)
        {
            Success = false;
            Code = code;
            Message = errorMessage;
        }
        #endregion Constructor
    }

    public class CommandResult<T> : CommandResult, ICommandResult<T>
    {
        #region Properties
        public T Result { get; set; }
        #endregion Properties

        #region Constructor
        public CommandResult(bool success, T result)
            : base(success)
        {
            Result = result;
        }

        public CommandResult(bool success, string code, T result)
            : base(success, code)
        {
            Result = result;
        }

        public CommandResult(bool success, string code, string message, T result)
            : base (success, code, message)
        {
            Result = result;
        }

        public CommandResult(string errorMessage, T result)
            : base(errorMessage)
        {
            Result = result;
        }

        public CommandResult(string errorMessage, string code, T result)
            : base(errorMessage, code)
        {
            Result = result;
        }
        #endregion Constructor
    }
}

