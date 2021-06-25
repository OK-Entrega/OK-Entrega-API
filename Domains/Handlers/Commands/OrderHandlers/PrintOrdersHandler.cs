using BarcodeLib;
using Commom.Commands;
using Commom.Services.PDFServices.Interfaces;
using Domains.Commands.Requests.OrderRequests;
using Domains.Commands.Responses.OrderResponses;
using Domains.Entities;
using Domains.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Handlers.Commands.OrderHandlers
{
    public class PrintOrdersHandler : IRequestHandler<PrintOrdersRequest, GenericCommandResult>
    {
        private readonly IOrderRepository _orderRepository;

        public PrintOrdersHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task<GenericCommandResult> Handle(PrintOrdersRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.OrdersIds == null)
                    return Task.FromResult(new GenericCommandResult(400, "Nenhuma nota a ser impressa!", null));

                var orders = new List<PrintOrdersResponse>();

                foreach (var orderId in request.OrdersIds)
                {
                    var order = _orderRepository.Search(orderId);
                    orders.Add(new PrintOrdersResponse(order, GenerateBarCode(order.AccessKey)));
                }

                return Task.FromResult(new GenericCommandResult(200, null, orders));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GenericCommandResult(500, ex.Message, ex.InnerException));
            }
        }

        private static string GenerateBarCode(string accessKey)
        {
            Barcode barcode = new Barcode()
            {
                EncodedType = TYPE.CODE128C,
                IncludeLabel = false,
                Alignment = AlignmentPositions.CENTER,
                Width = 900,
                Height = 60,
                RotateFlipType = RotateFlipType.RotateNoneFlipNone,
                BackColor = Color.White,
                ForeColor = Color.Black,
            };

            Image img = barcode.Encode(TYPE.CODE128C, accessKey);

            using (var ms = new MemoryStream())
            {
                img.Save(ms, ImageFormat.Bmp);
                return string.Format("data:image/png;base64,{0}", Convert.ToBase64String(ms.ToArray()));
            }
        }
    }
}
