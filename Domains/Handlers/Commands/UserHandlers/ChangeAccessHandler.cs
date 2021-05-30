using Commom.Commands;
using Domains.Commands.Requests.UserRequests;
using Domains.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Handlers.Commands.UserHandlers
{
    public class ChangeAccessHandler : IRequestHandler<ChangeAccessRequest, GenericCommandResult>
    {
        private readonly IUserRepository _userRepository;

        public ChangeAccessHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<GenericCommandResult> Handle(ChangeAccessRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Discriminator == null)
                    return Task.FromResult(new GenericCommandResult(400, null, null));
                else if (request.Discriminator == "Shipper")
                {
                    var user = _userRepository.Search(request.UserId);

                    if(user.Shipper.CodeEmail.Substring(0, 4) != request.Code)
                        return Task.FromResult(new GenericCommandResult(400, "Código incorreto!", null));
                    else
                    {
                        user.Shipper.ChangeEmail(user.Shipper.CodeEmail[4..]);
                        user.Shipper.RequestNewEmail(null);
                    }

                    _userRepository.Update(user);

                    return Task.FromResult(new GenericCommandResult(200, "Email alterado com sucesso!", null));
                }
                else
                {
                    var user = _userRepository.Search(request.UserId);

                    if (user.Deliverer.CodeCellphoneNumber.Substring(0, 4) != request.Code)
                        return Task.FromResult(new GenericCommandResult(400, "Código incorreto!", null));
                    else
                    {
                        user.Deliverer.ChangeCellphoneNumber(user.Deliverer.CodeCellphoneNumber[4..]);
                        user.Deliverer.RequestNewCellphoneNumber(null);
                    }

                    _userRepository.Update(user);

                    return Task.FromResult(new GenericCommandResult(200, "Número de celular alterado com sucesso!", null));
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GenericCommandResult(500, ex.Message, ex.InnerException));
            }
        }
    }
}
