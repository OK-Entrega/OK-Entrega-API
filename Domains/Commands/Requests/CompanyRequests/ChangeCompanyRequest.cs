﻿using Commom.Commands;
using Flunt.Validations;
using System;

namespace Domains.Commands.Requests.Company
{
    public class ChangeCompanyRequest : CommandRequest
    {
        public Guid CompanyId { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }

        public ChangeCompanyRequest(string name, string cnpj)
        {
            Name = name.Trim();
            CNPJ = cnpj.Trim();
        }
        public override void Validate()
        {
            AddNotifications(new Contract<ChangeCompanyRequest>()
                 .Requires()
                 .IsTrue((Name.Length > 2), "Name", "O nome da empresa deve ter ao menos 3 caracteres!")
                 .IsTrue((CNPJ.Length < 15), "CNPJ", "O CNPJ da empresa deve conter 14 caracteres!")
            );
        }
    }
}