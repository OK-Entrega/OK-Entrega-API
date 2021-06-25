using Commom.Services.PDFServices.Interfaces;
using DinkToPdf;
using DinkToPdf.Contracts;
using System.Threading.Tasks;

namespace Commom.Services.PDFServices.Providers
{
    public class PDFGeneratorServices : IPDFGenerator
    {
        private readonly IConverter _converter;
        private readonly IRazorPageRenderer _renderer;

        public PDFGeneratorServices(
            IConverter converter,
            IRazorPageRenderer renderer
        )
        {
            _converter = converter;
            _renderer = renderer;
        }

        public async Task<byte[]> Generate(object data, string template)
        {
            string cssPath = $"Templates/PDF/{template.ToLower()}.css";

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4
				},
                Objects = {
                    new ObjectSettings() {
                        PagesCount = true,
                        HtmlContent = await _renderer.RenderViewToStringAsync(
                            $"Templates/PDF/{template}.cshtml",
                            data
                        ),
                        WebSettings = 
                        { 
                            DefaultEncoding = "utf-8",
                            UserStyleSheet = cssPath, 
                            LoadImages = true},
                        HeaderSettings = 
                        { 
                            FontSize = 9,
                            Right = "Página [page] de [toPage]",
                            Line = false,
                            Spacing = 2.812
                        },
                        FooterSettings = 
                        { 
                            Spacing = 20.000 
                        }            
                    }
                }
            };

            return _converter.Convert(doc);
        }
    }
}
