using Commom.Commands;
using Domains.Commands.Requests.UserRequests;
using Domains.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Handlers.Commands.UserHandlers
{
    public class VerifyAccountHandler : IRequestHandler<VerifyAccountRequest, GenericCommandResult>
    {
        private readonly IDelivererRepository _delivererRepository;
        private readonly IShipperRepository _shipperRepository;

        public VerifyAccountHandler(IDelivererRepository delivererRepository, IShipperRepository shipperRepository)
        {
            _delivererRepository = delivererRepository;
            _shipperRepository = shipperRepository;
        }

        public Task<GenericCommandResult> Handle(VerifyAccountRequest request, CancellationToken cancellationToken)
        {
            //verifica se ja ta ativo
            try
            {
                if (request.Discriminator == null)
                    return Task.FromResult(new GenericCommandResult(400, null, null));
                else if (request.Discriminator == "Shipper")
                {
                    var shipper = _shipperRepository.Search((Guid) request.ShipperId);

                    if (shipper.User.Active)
                        return Task.FromResult(new GenericCommandResult(400, "Seu email já foi verificado!", null));
                    else
                    {
                        shipper.User.TurnActive();
                    }

                    _shipperRepository.Update(shipper);

                    return Task.FromResult(new GenericCommandResult(200, "Email verificado com sucesso!", null));
                }
                else
                {
                    var deliverer = _delivererRepository.Search((Guid) request.DelivererId);

                    if (deliverer.User.Active || deliverer.VerifyingCode == null)
                        return Task.FromResult(new GenericCommandResult(400, "Seu número de celular já foi verificado!", null));
                    else
                    {
                        deliverer.User.TurnActive();
                        deliverer.TurnVerified();
                    }

                    _delivererRepository.Update(deliverer);

                    return Task.FromResult(new GenericCommandResult(200, "Número de celular verificado com sucesso!", null));
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GenericCommandResult(500, ex.Message, ex.InnerException));
            }
        }
    }
}
