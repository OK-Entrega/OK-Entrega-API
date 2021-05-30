namespace Commom.Commands
{
    public class GenericCommandResult : ICommandResult
    {
        public int StatusCode { get; set; }
        public object Status { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public GenericCommandResult(int statusCode, string message, object data)
        {
            StatusCode = statusCode;
            Success = statusCode == 200;
            Message = message;
            Data = data;
        }
    }
}
