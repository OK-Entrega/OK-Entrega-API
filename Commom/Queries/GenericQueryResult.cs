namespace Commom.Queries
{
    public class GenericQueryResult : IQueryResult
    {
        public int StatusCode { get; set; }
        public object Status { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public GenericQueryResult(int statusCode, string message, object data)
        {
            StatusCode = statusCode;
            Success = statusCode == 200;
            Message = message;
            Data = data;
        }
    }
}
