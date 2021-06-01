using System.Threading.Tasks;

namespace Commom.Services.PDFServices.Interfaces
{
    public interface IRazorPageRenderer
    {
        Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);
    }
}
