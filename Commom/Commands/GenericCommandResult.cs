namespace Commom.Commands
{
    public class GenericCommandResult : ICommandResult
    {
        public int StatusCode { get; set; }
        public object Status { get; set; }
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public object Dados { get; set; }

        public GenericCommandResult(int statusCode, string mensagem, object dados)
        {
            StatusCode = statusCode;
            Sucesso = statusCode == 200;
            Mensagem = mensagem;
            Dados = dados;
        }
    }
}
