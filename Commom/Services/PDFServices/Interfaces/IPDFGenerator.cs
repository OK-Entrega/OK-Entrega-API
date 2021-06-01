using System.Threading.Tasks;

namespace Commom.Services.PDFServices.Interfaces
{
    public interface IPDFGenerator
    {
        public Task<byte[]> Generate(object data, string template);
    }
}
