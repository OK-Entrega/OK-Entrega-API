using Commom.Enum;
using Commom.Queries;
using Commom.Services;
using Domains.Entities;
using Domains.Queries.Requests.CompanyRequests;
using Domains.Queries.Responses.CompanyResponses;
using Domains.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Handlers.Queries.CompanyHandlers
{
    public class GetShippersHandler : IRequestHandler<GetShippersRequest, GenericQueryResult>
    {
        private IShipperRepository _shipperRepository { get; set; }
        private ICompanyRepository _companyRepository { get; set; }

        public GetShippersHandler(IShipperRepository shipperRepository, ICompanyRepository companyRepository)
        {
            _shipperRepository = shipperRepository;
            _companyRepository = companyRepository;
        }

        public Task<GenericQueryResult> Handle(GetShippersRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var company = _companyRepository.Search(request.CompanyId);

                var query = _shipperRepository.Read(request.CompanyId);

                var canBan = query.Any(s => s.ShipperHasCompanies.Any(shc => shc.CompanyId == request.CompanyId && shc.Shipper.UserId == request.UserId && shc.ShipperRole == EnShipperRole.Creator));

                if (!string.IsNullOrEmpty(request.Name))
                    query = query.Where(s => s.User.Name.ToLower().Contains(request.Name.ToLower()));

                int pageCount = (((query.Count() - 1) / 20) + 1);
                query = query.Skip((request.Page - 1) * 20).Take(20);

                if (query == null || !query.Any())
                    return Task.FromResult(new GenericQueryResult(404, null, new { shippersCount = query.Count() }));

                var shippers = new List<GetShippersResponse>();

                var i = query.Where(s => s.ShipperHasCompanies.Any(shc => shc.CompanyId == request.CompanyId && shc.Shipper.UserId == request.UserId))?.FirstOrDefault();

                var creator = query.Where(s => s.ShipperHasCompanies.Any(shc => shc.CompanyId == request.CompanyId && shc.ShipperRole == EnShipperRole.Creator))?.FirstOrDefault();

                if (i != null && creator != null)
                {
                    if (i?.Id == creator?.Id)
                        shippers.Add(ConvertEntityToResponse(creator, request.CompanyId, request.UserId));
                    else
                    {
                        shippers.Add(ConvertEntityToResponse(i, request.CompanyId, request.UserId));
                        shippers.Add(ConvertEntityToResponse(creator, request.CompanyId, request.UserId));
                    }
                }
                else if(i != null)
                    shippers.Add(ConvertEntityToResponse(i, request.CompanyId, request.UserId));
                else if(creator != null)
                    shippers.Add(ConvertEntityToResponse(creator, request.CompanyId, request.UserId));

                shippers.AddRange(query.Where(s => s.ShipperHasCompanies.Any(shc => shc.CompanyId == request.CompanyId && shc.ShipperRole != EnShipperRole.Creator && s.UserId != request.UserId)).Select(r => ConvertEntityToResponse(r, request.CompanyId, request.UserId)).ToList());

                //criador
                    //eu
                    //shippers

                //shippers
                    //eu
                    //criador
                    //shippers

                return Task.FromResult(new GenericQueryResult(200, null, new { pageCount = pageCount, code = company.Code, shippersCount = query.Count(), canBan = canBan, shippers = shippers }));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GenericQueryResult(500, ex.Message, ex.InnerException));
            }
        }

        private static GetShippersResponse ConvertEntityToResponse(Shipper shipper, Guid companyId, Guid userId)
        {
            return new GetShippersResponse(
                shipper.Id,
                EnumServices.GetDescription(shipper.ShipperHasCompanies.FirstOrDefault(shc => shc.CompanyId == companyId).ShipperRole),
                shipper.UserId == userId ? shipper.User.Name + " (Eu)" : shipper.User.Name,
                shipper.ShipperHasCompanies.FirstOrDefault(shc => shc.CompanyId == companyId).CreatedAt.ToString("dd/MM/yyyy")
            );
        }
    }
}
