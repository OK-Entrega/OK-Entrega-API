using Commom.Entities;
using Commom.Enum;
using Flunt.Validations;
using System;
using System.Collections.Generic;

namespace Domains.Entities
{
    public class Order : Entity
    {
        //Dados da NFE
        public EnModelNFE ModelNFE { get; private set; }
        public string Series { get; private set; }
        public string Number { get; private set; }
        public string AccessKey { get; private set; }
        public string NatureOperation { get; private set; }
        public EnIssueType IssueType { get; private set; }
        public string CFOP { get; private set; }
        public string NumericCode { get; private set; }
        public decimal TotalValue { get; private set; }
        public decimal Weight { get; private set; }
        public DateTime IssuedAt { get; private set; }
        public DateTime DispatchedAt { get; private set; }

        //Dados do destinatário
        public EnReceiverType ReceiverType { get; set; }
        public string ReceiverName { get; private set; }
        public string ReceiverDocument { get; private set; }
        public string ReceiverEmail { get; private set; }

        //Dados da transportadora
        public string CarrierName { get; private set; }
        public string CarrierCNPJ { get; private set; }

        //Informações de destino
        public Local Destination { get; private set; }

        //Informações adicionais
        public FinishOrder FinishOrder { get; private set; }
        public ICollection<OccurrenceOrder> Occurrences { get; private set; }

        //Dados do embarcador
        public Guid CompanyId { get; private set; }
        public Company Company { get; private set; }
        public string CompanyUF { get; private set; }

        public Order(
            EnModelNFE modelNFE,
            string series,
            string number,
            string natureOperation,
            EnIssueType issueType,
            string cfop,
            decimal totalValue,
            decimal weight,
            DateTime issuedAt,
            DateTime dispatchedAt,
            EnReceiverType receiverType,
            string receiverName,
            string receiverDocument,
            string receiverEmail,
            string carrierName,
            string carrierCNPJ,
            Local destination,
            Company company,
            string companyUF,
            string numericCode = null
        )
        {
            series = series.Trim().Replace(".", "");
            number = number.Trim().Replace(".", "");
            natureOperation = natureOperation.Trim();
            cfop = cfop.Trim().Replace(".", "");
            totalValue = Math.Round(totalValue, 2);
            weight = Math.Round(weight, 2);
            receiverName = receiverName.Trim();
            receiverDocument = receiverDocument.Trim().Replace("-", "").Replace(".", "").Replace("/", "");
            receiverEmail = receiverEmail.Trim().ToLower();
            carrierName = carrierName.Trim();
            carrierCNPJ = carrierCNPJ.Trim().Replace("-", "").Replace(".", "").Replace("/", "");
            companyUF = companyUF.Trim();
            if (numericCode != null)
                numericCode = numericCode.Trim();

            AddNotifications(new Contract<Order>()
                .Requires()
                .IsTrue((series.Length > 0 && series.Length < 4), "Série", "A série da NFe deve ter entre 1 à 3 caracteres!")
                .IsTrue(number.Length == 9, "Número", "O número da NFe deve conter 9 caracteres!")
                .IsTrue((natureOperation.Length > 3 && natureOperation.Length < 31), "Natureza da operação", "A natureza da operação da NFe deve ter entre 4 à 30 caracteres!")
                .IsTrue(cfop.Length == 4, "CFOP", "O CFOP da NFe deve conter 4 caracteres!")
                .IsEmail(receiverEmail, "Email do destinatário", "O email do destinatário informado é inválido!")
                .IsTrue((carrierName.Length > 2 && carrierName.Length < 41), "Nome da transportadora", "O nome da transportadora deve ter entre 3 à 40 caracteres!")
                .IsTrue(carrierCNPJ.Length == 14, "CNPJ da transportadora", "O CNPJ da transportadora deve conter 14 caracteres!")
                .IsTrue(companyUF.Length == 2, "UF da empresa emitente", "A UF da empresa emitente deve conter 2 caracteres!")
            );

            if (receiverType == EnReceiverType.JuridicalPerson)
            {
                AddNotifications(new Contract<Order>()
                    .Requires()
                    .IsTrue((receiverName.Length > 2 && receiverName.Length < 41), "Nome da empresa do destinatário", "O nome da empresa do destinatário deve ter entre 3 à 40 caracteres!")
                    .IsTrue(receiverDocument.Length == 14, "CNPJ", "O CNPJ da empresa do destinatário deve conter 14 caracteres!")
                );
            }
            else
            {
                AddNotifications(new Contract<Order>()
                    .Requires()
                    .IsTrue((receiverName.Length > 2 && receiverName.Length < 41), "Nome do destinatário", "O nome do destinatário deve ter entre 3 à 40 caracteres!")
                    .IsTrue(receiverDocument.Length == 8, "CPF", "O CPF do destinatário deve conter 8 caracteres!")
                );
            }

            if (numericCode != null)
                AddNotifications(new Contract<Order>()
                    .Requires()
                    .IsTrue(numericCode.Length == 8, "Código numérico", "O código numérico da NFe deve conter 8 caracteres!")
                );

            if (IsValid)
            {
                Series = series;
                Number = number;
                if (numericCode != null)
                    NumericCode = numericCode;
                else
                    NumericCode = GenerateNumericCode();
                AccessKey = $"{companyUF}{issuedAt.Year.ToString().Substring(2, 2)}{issuedAt.Month}{company.CNPJ}{modelNFE}{series}{number}{issueType}{NumericCode}";
                AccessKey += GenerateVerifyingDigit();
                NatureOperation = natureOperation;
                IssueType = issueType;
                CFOP = cfop;
                NumericCode = numericCode;
                TotalValue = totalValue;
                Weight = weight;
                IssuedAt = issuedAt;
                DispatchedAt = dispatchedAt;
                ReceiverType = receiverType;
                ReceiverName = receiverName;
                ReceiverDocument = receiverDocument;
                ReceiverEmail = receiverEmail;
                CarrierName = carrierName;
                CarrierCNPJ = carrierCNPJ;
                Destination = destination;
                Occurrences = new List<OccurrenceOrder>();
                Company = company;
                CompanyUF = companyUF;
            }

            Occurrences = new List<OccurrenceOrder>();
        }

        private string GenerateNumericCode()
        {
            string caracters = "0123456789";
            string numericCode = "";

            Random random = new Random();

            for (int c = 0; c <= 8; c++)
            {
                numericCode += caracters.Substring(random.Next(0, caracters.Length - 1), 1);
            }

            return numericCode;
        }

        private string GenerateVerifyingDigit()
        {
            int sum = 0;
            int mod = -1;
            int vd = -1;
            int weight = 2;

            for (int i = AccessKey.Length - 1; i != -1; i--)
            {
                int ch = Convert.ToInt32(AccessKey[i]);
                sum += ch * weight;
                if (weight < 9)
                    weight += 1;
                else
                    weight = 2;
            }

            mod = sum % 11;

            if (mod == 0 || mod == 1)
                vd = 0;
            else
                vd = 11 - mod;

            return vd.ToString();
        }
    }
}
