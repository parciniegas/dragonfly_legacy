namespace Dragonfly.Core.ErrorMessages
{
    public class ErrorMessage : IErrorMessage
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
