﻿using Commom.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace Domains.Commands.Requests.DelivererRequests
{
    public class ChangeDelivererCommand : Notifiable<Notification>, ICommand
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string CellphoneNumber { get; set; }
        public string Password { get; set; }

        public ChangeDelivererCommand(string name, string cellphoneNumber, string password)
        {
            Name = name.Trim();
            CellphoneNumber = cellphoneNumber.Trim();
            if (CellphoneNumber.Length != 11)
                CellphoneNumber = "1";
            Password = password.Trim();
        }

        public void Validate()
        {
            AddNotifications(new Contract<ChangeDelivererCommand>()
                .Requires()
                .IsTrue((Name.Length > 2) && (Name.Length < 41), "Nome", "O nome deve ter de 3 a 40 caracteres!")
                .IsTrue((Password.Length > 5) && (Password.Length < 21), "Senha", "A senha deve ter de 6 a 20 caracteres!")
                .IsTrue(CellphoneNumber.Length == 11, "Número de telefone celular", "Número de telefone celular inválido!")
            );
        }
    }
}