namespace Commom.Queries
{
    public class GenericQueryResult : IQueryResult
    {
        public int StatusCode { get; set; }
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public object Dados { get; set; }

        public GenericQueryResult(int statusCode, string mensagem, object dados)
        {
            StatusCode = statusCode;
            Sucesso = statusCode == 200;
            Mensagem = mensagem;
            Dados = dados;
        }
    }
}
