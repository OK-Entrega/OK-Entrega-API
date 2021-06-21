using Commom.Queries;
using Domains.Queries.Requests.UserRequests;
using Domains.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Handlers.Queries.UserHandlers
{
    public class GetProfileHandler : IRequestHandler<GetProfileRequest, GenericQueryResult>
    {
        private readonly IUserRepository _userRepository;

        public GetProfileHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<GenericQueryResult> Handle(GetProfileRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _userRepository.Search(request.UserId);

                if (request.Discriminator == "Shipper")
                {
                    var result = new
                    {
                        Name = user.Name,
                        Email = user.Shipper.Email
                    };

                    return Task.FromResult(new GenericQueryResult(200, null, result));
                }
                else
                {
                    var result = new
                    {
                        Name = user.Name,
                        CellphoneNumber = user.Deliverer.CellphoneNumber
                    };

                    return Task.FromResult(new GenericQueryResult(200, null, result));
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GenericQueryResult(500, ex.Message, ex.InnerException));
            }
        }
    }
}
