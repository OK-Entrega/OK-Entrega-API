using Commom.Entities;
using Commom.Enum;
using System;
using System.Collections.Generic;

namespace Domains.Entities
{
    public class Order : Entity
    {
        public string Description { get; private set; }
        public EnOrderType Type { get; private set; }
        public string NFE { get; private set; }
        public string ToCNPJ { get; private set; }
        public DateTime? FinishedAt { get; private set; }
        public int AttemptsNumber { get; private set; }
        public List<string> UrlsEvidencesImages { get; private set; }
        public Local Local { get; private set; }
        public Guid CompanyId { get; private set; }
        public Company Company { get; private set; }

        public Order(
            string description,
            EnOrderType type,
            string nfe,
            string toCNPJ,
            int attemptsNumber,
            Local local,
            Company company
        )
        {
            Description = description;
            Type = type;
            NFE = nfe;
            ToCNPJ = toCNPJ;
            FinishedAt = null;
            AttemptsNumber = 0;
            UrlsEvidencesImages = new List<string>();
            Local = local;
            CompanyId = company.Id;
            Company = company;
        }
    }
}
