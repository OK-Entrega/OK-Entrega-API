using Domains.Entities;

namespace Domains.Commands.Responses.OrderResponses
{
    public class PrintOrdersResponse
    {
        public Order Order { get; set; }
        public string BarCode { get; set; }

        public PrintOrdersResponse(
            Order order,
            string barCode
        )
        {
            Order = order;
            BarCode = barCode;
        }
    }
}
