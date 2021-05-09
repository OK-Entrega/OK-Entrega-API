using System.Collections.Generic;

namespace Domains.Entities
{
    public class Deliverer : User
    {
        public string CellphoneNumber { get; private set; }
        public ICollection<FinishOrder> FinishedOrders { get; private set; }
        public ICollection<OccurrenceOrder> OccurrencesOrders { get; private set; }

        public Deliverer(
            string name,
            string password,
            string cellphoneNumber
        ) : base(
            name,
            password
        )
        {
            CellphoneNumber = cellphoneNumber;
            FinishedOrders = new List<FinishOrder>();
            OccurrencesOrders = new List<OccurrenceOrder>();
        }
    }
}
