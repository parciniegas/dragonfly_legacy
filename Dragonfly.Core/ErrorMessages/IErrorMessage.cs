namespace Dragonfly.Core.ErrorMessages
{
    public interface IErrorMessage
    {
        int Id { get; set; }
        string Code { get; set; }
        string Message { get; set; }
    }
}
