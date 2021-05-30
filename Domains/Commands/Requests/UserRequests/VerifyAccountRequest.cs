using Commom.Commands;
using Flunt.Validations;
using System;
using System.Text.Json.Serialization;

namespace Domains.Commands.Requests.UserRequests
{
    public class VerifyAccountRequest : CommandRequest
    {
        public Guid? ShipperId { get; set; }
        public Guid? DelivererId { get; set; }
        public string VerifyingCode { get; set; }
        [JsonIgnore]
        public string Discriminator { get; set; }

        public VerifyAccountRequest(
            Guid? shipperId,
            Guid? delivererId,
            string verifyingCode
        )
        {
            ShipperId = shipperId;
            DelivererId = delivererId;
            VerifyingCode = verifyingCode;

            if (ShipperId != null && DelivererId != null)
                Discriminator = null;
            else if (shipperId != null)
                Discriminator = "Shipper";
        }

        public override void Validate()
        {
            if(Discriminator == "Deliverer")
                AddNotifications(new Contract<VerifyAccountRequest>()
                    .Requires()
                    .IsTrue(VerifyingCode.Length == 4, "Código de verificação", "O código de verificação deve conter 4 dígitos!")
                );
        }
    }
}
