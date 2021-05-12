﻿using Commom.Commands;
using Commom.Utils;
using Domains.Commands.Requests.ShipperRequests;
using Domains.Entities;
using Domains.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Handlers.Commands.UserHandlers
{
    public class CreateShipperHandler : IRequestHandler<CreateShipperRequest, GenericCommandResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IShipperRepository _shipperRepository;

        public CreateShipperHandler(IUserRepository userRepository, IShipperRepository shipperRepository)
        {
            _userRepository = userRepository;
            _shipperRepository = shipperRepository;
        }

        public Task<GenericCommandResult> Handle(CreateShipperRequest request, CancellationToken cancellationToken)
        {
            try
            {
                bool exists = _shipperRepository.Search(request.Email) != null;
                if (exists)
                    return Task.FromResult(new GenericCommandResult(400, "Já existe um usuário cadastrado com esse email!", request.Email));

                string passwordEncoded = Password.Encrypt(request.Password);

                var user = new User(request.Name, passwordEncoded);
                var shipper = new Shipper(request.Email, user.Id);

                _userRepository.Create(user);
                _shipperRepository.Create(shipper);

                Sendgrid.SendEmail(shipper.Email, "Bem-vindo!", "Seja muito bem vindo ao OKEntrega, um sistema SaaS para ajudar na gestão de entregas da sua empresa!");

                return Task.FromResult(new GenericCommandResult(200, "Seja bem vindo, " + user.Name, JWT.Generate(user.Name, shipper.Email, user.Id, 100)));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GenericCommandResult(500, ex.Message, ex.InnerException));
            }
        }
    }
}
