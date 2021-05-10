using Commom.Entities;
using System;
using System.Collections.Generic;

namespace Domains.Entities
{
    public class Deliverer : Entity
    {
        public string CellphoneNumber { get; private set; }
        public User User { get; private set; }
        public Guid UserId { get; private set; }
        public ICollection<FinishOrder> FinishedOrders { get; private set; }
        public ICollection<OccurrenceOrder> OccurrencesOrders { get; private set; }

        public Deliverer(string cellphoneNumber, Guid userId)
        {
            CellphoneNumber = cellphoneNumber;
            UserId = userId;
            FinishedOrders = new List<FinishOrder>();
            OccurrencesOrders = new List<OccurrenceOrder>();
        }
    }
}
