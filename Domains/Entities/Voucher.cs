using Commom.Entities;
using System;

namespace Domains.Entities
{
    public class Voucher : Entity
    {
        public string Path { get; private set; }
        public decimal Score { get; private set; }
        public decimal HasData { get; private set; }
        public decimal HasSignature { get; private set; }
        public decimal HasNumberAndSeries { get; private set; }
        public FinishOrder FinishOrder { get; private set; }
        public Guid FinishOrderId { get; private set; }

        public Voucher(
            string path,
            decimal hasData,
            decimal hasSignature,
            decimal hasNumberAndSeries
        )
        {
            Path = path;
            HasData = hasData;
            HasSignature = hasSignature;
            HasNumberAndSeries = hasNumberAndSeries;
            Score = (hasData + hasSignature + hasNumberAndSeries) / 3;
        }
    }
}
