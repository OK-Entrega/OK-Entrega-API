using Commom.Entities;
using System;
using System.Collections.Generic;

namespace Domains.Entities
{
    public class Deliverer : Entity
    {
        public string CellphoneNumber { get; private set; }
        public string VerifyingCode { get; private set; }
        public string CodeCellphoneNumber { get; private set; }
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public ICollection<FinishOrder> FinishedOrders { get; private set; }
        public ICollection<OccurrenceOrder> OccurrencesOrders { get; private set; }

        public Deliverer(
            string cellphoneNumber,
            User user
        )
        {
            CellphoneNumber = cellphoneNumber;
            User = user;
            FinishedOrders = new List<FinishOrder>();
            OccurrencesOrders = new List<OccurrenceOrder>();
            VerifyingCode = GenerateVerifyingCode();
            CodeCellphoneNumber = null;
        }

        public Deliverer(){}

        private static string GenerateVerifyingCode()
        {
            string caracters = "0123456789";
            string verifyingCode = "";

            Random random = new Random();

            for (int c = 0; c < 4; c++)
            {
                verifyingCode += caracters.Substring(random.Next(0, caracters.Length - 1), 1);
            }

            return verifyingCode;
        }

        public void ChangeCellphoneNumber(string cellphoneNumber)
        {
            CellphoneNumber = cellphoneNumber;
        }

        public void TurnVerified()
        {
            VerifyingCode = null;
        }

        public void RequestNewCellphoneNumber(string codeCellphoneNumber)
        {
            CodeCellphoneNumber = codeCellphoneNumber;
        }
    }
}
