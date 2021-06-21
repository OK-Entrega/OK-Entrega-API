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

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads\XML", fileName);

            using var streamImagem = new FileStream(filePath, FileMode.Create);

            xml.CopyTo(streamImagem);

            return "http://localhost:5000/Uploads/XML/" + fileName;
        }

        public static string Image(IFormFile image, bool serverDirectoryPathToo = false)
        {
            var fileName = $"OK-Entrega-Image-{Guid.NewGuid().ToString().Replace("-", "")}{Path.GetExtension(image.FileName)}";

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads\Image", fileName);

            using var streamImagem = new FileStream(filePath, FileMode.Create);

            image.CopyTo(streamImagem);

            if(serverDirectoryPathToo)
                return "http://localhost:5000/Uploads/Image/" + " " + filePath;
            return "http://localhost:5000/Uploads/Image/";
        }
    }
}
