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
        public decimal NumberAndSeriesIsCorrect { get; private set; }
        public decimal DataIsCorrect { get; private set; }
        public FinishOrder FinishOrder { get; private set; }
        public Guid FinishOrderId { get; private set; }

        public Voucher(
            string path
        )
        {
            Path = path;
            Score = 0;
            HasData = 0;
            HasSignature = 0;
            HasNumberAndSeries = 0;
            NumberAndSeriesIsCorrect = 0;
            DataIsCorrect = 0;
        }
    }
}
