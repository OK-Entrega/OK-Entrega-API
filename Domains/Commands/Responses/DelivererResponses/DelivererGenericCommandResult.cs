using System;

namespace Domain.Commands.DelivererResponses
{
    public class DelivererGenericCommandResult
    {
        
        public string Name { get; set; }
        public string CellphoneNumber { get; set; }
        public string Password { get; set; }

        public DelivererGenericCommandResult(string name, string cellphoneNumber, string password)
        {
            Name = name;
            CellphoneNumber = cellphoneNumber;
            Password = password;
        }
    }
}