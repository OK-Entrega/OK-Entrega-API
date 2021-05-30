using Commom.Commands;
using Commom.Responses;
using Commom.Services;
using Domains.Commands.Requests.OrderRequests;
using Domains.Entities;
using Domains.Repositories;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Domains.Handlers.Commands.OrderHandlers
{
    public class CreateOrdersHandler : IRequestHandler<CreateOrdersRequest, GenericCommandResult>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICompanyRepository _companyRepository;

        public CreateOrdersHandler(IOrderRepository orderRepository, ICompanyRepository companyRepository)
        {
            _orderRepository = orderRepository;
            _companyRepository = companyRepository;
        }

        public Task<GenericCommandResult> Handle(CreateOrdersRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var errors = new List<Alert>();

                if (request.Files == null)
                    return Task.FromResult(new GenericCommandResult(400, "Nenhum arquivo para fazer upload!", null));

                if (!request.Files.Any(f => f.ContentType.Contains("xml")))
                    return Task.FromResult(new GenericCommandResult(400, "Você deve enviar apenas arquivos xml!", null));

                var company = _companyRepository.Search(request.CompanyId);

                foreach (var file in request.Files)
                {
                    var filePath = UploadServices.Xml(file);

                    XmlDocument doc = new XmlDocument();
                    doc.Load(filePath);
                    dynamic note = JsonConvert.DeserializeObject(JsonConvert.SerializeXmlNode(doc));
                    note = note.nfeProc.NFe.infNFe;

                    var a = note.ide.cUF.ToString();

                    Console.WriteLine(a.GetType());

                    var order = new Order(filePath, note.ide.cUF, note.ide.dEmi.ToString(), company, note.ide.mod, note.ide.serie, note.ide.nNF, note.ide.tpEmis, note.ide.cNF, note.ide.cDV, note.ide.natOp, note.det.prod.CFOP, note.ide.dSaiEnt, note.emit.enderEmit.CEP, note.emit.enderEmit.xLgr, note.emit.enderEmit.nro, note.emit.enderEmit.xCpl, note.emit.enderEmit.xBairro, note.emit.enderEmit.UF, note.emit.enderEmit.xMun, note.dest.xNome, note.dest.CNPJ, note.dest.enderDest.CEP, note.dest.enderDest.xLgr, note.dest.enderDest.nro, note.dest.enderDest.xCpl, note.dest.enderDest.xBairro, note.dest.enderDest.UF, note.dest.enderDest.xMun, note.transp.xNome, note.transp.CNPJ, note.total.valTot, note.transp.vol.pesoB);

                    if (_orderRepository.Search(order.AccessKey) != null)
                        errors.Add(new Alert($"A nota {order.AccessKey} já existe!"));
                    else
                    {
                        _orderRepository.Create(order);
                    }
                }

                if(errors.Count == request.Files.Count)
                    return Task.FromResult(new GenericCommandResult(400, "Todas as notas enviadas já existem!", null));

                return Task.FromResult(new GenericCommandResult(200, $"{request.Files.Count - errors.Count} de {request.Files.Count} notas cadastradas com sucesso!", null));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GenericCommandResult(500, ex.Message, ex.InnerException));
            }
        }
    }
}
