using Commom.Commands;
using Commom.Enum;
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

                    var order = new Order(filePath, note.ide.cUF.ToString(), DateTime.Parse(note.ide.dEmi.ToString()), company, (EnModelNFE)(int)note.ide.mod, note.ide.serie.ToString(), note.ide.nNF.ToString(), (EnIssueType)(int)note.ide.tpEmis, note.ide.cNF.ToString(), note.ide.cDV.ToString(), note.ide.natOp.ToString(), note.det.prod.CFOP.ToString(), DateTime.Parse(note.ide.dSaiEnt.ToString()), note.emit.enderEmit.CEP.ToString(), note.emit.enderEmit.xLgr.ToString(), note.emit.enderEmit.nro.ToString(), note.emit.enderEmit.xCpl.ToString(), note.emit.enderEmit.xBairro.ToString(), note.emit.enderEmit.UF.ToString(), note.emit.enderEmit.xMun.ToString(), note.dest.xNome.ToString(), note.dest.CNPJ.ToString(), note.dest.enderDest.CEP.ToString(), note.dest.enderDest.xLgr.ToString(), note.dest.enderDest.nro.ToString(), note.dest.enderDest.xCpl.ToString(), note.dest.enderDest.xBairro.ToString(), note.dest.enderDest.UF.ToString(), note.dest.enderDest.xMun.ToString(), note.transp.xNome.ToString(), note.transp.CNPJ.ToString(), (EnVehicleType)(int) note.transp.tpVei, note.transp.plVei.ToString(), Convert.ToDecimal(note.total.valTot), Convert.ToDecimal(note.transp.vol.pesoB));

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
