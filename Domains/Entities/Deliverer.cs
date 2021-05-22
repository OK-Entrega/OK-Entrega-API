using Commom.Entities;
using Flunt.Validations;
using System;
using System.Collections.Generic;

namespace Domains.Entities
{
    public class Deliverer : Entity
    {
        public string CellphoneNumber { get; private set; }
        public string VerifyingCode { get; private set; }
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public ICollection<FinishOrder> FinishedOrders { get; private set; }
        public ICollection<OccurrenceOrder> OccurrencesOrders { get; private set; }

        public Deliverer(
            string cellphoneNumber,
            Guid userId
        )
        {
            cellphoneNumber = cellphoneNumber.Trim().Replace("+", "").Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");

            AddNotifications(new Contract<Deliverer>()
                .Requires()
                .IsTrue(cellphoneNumber.Length == 11, "Número de telefone celular", "O número de telefone celular deve conter 11 dígitos!")
            );

            if (IsValid)
            {
                CellphoneNumber = cellphoneNumber;
                UserId = userId;
                FinishedOrders = new List<FinishOrder>();
                OccurrencesOrders = new List<OccurrenceOrder>();
                VerifyingCode = GenerateVerifyingCode();
            }
        }

        private string GenerateVerifyingCode()
        {
            string caracters = "0123456789";
            string verifyingCode = "";

            Random random = new Random();

            for (int c = 0; c <= 4; c++)
            {
                verifyingCode += caracters.Substring(random.Next(0, caracters.Length - 1), 1);
            }

            return verifyingCode;
        }
    }
}
