using Microsoft.AspNetCore.Http;
using System;
using System.IO;
namespace Commom.Services
{
    public static class UploadServices
    {
        public static string Xml(IFormFile xml)
        {
            var fileName = $"OK-Entrega-XML-{Guid.NewGuid().ToString().Replace("-", "")}{Path.GetExtension(xml.FileName)}";

            var caminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads\XML", fileName);

            using var streamImagem = new FileStream(caminhoArquivo, FileMode.Create);

            xml.CopyTo(streamImagem);

            return "http://localhost:5000/Uploads/XML/" + fileName;
        }
    }
}
