namespace Dragonfly.Core.ErrorMessages
{
    public interface IErrorMessageProvider
    {
        IErrorMessage GetErrorMessage(string code, object parameters);
    }
}
