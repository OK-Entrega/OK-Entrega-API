namespace Commom.Responses
{
    public class Alert
    {
        public string Message { get; set; }

        public Alert(string message)
        {
            Message = message;
        }
    }
}
