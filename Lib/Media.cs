using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Lib
{
    public enum RequestFrom { None, Create, Edit, AddFile };

    public class FileModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
    public static class Media 

    {

        public static byte[] Image { get; set; }

        public static RequestFrom RequestFrom = RequestFrom.None;
        public static string RequestControllerName = "";
        public static string RequestControllerAction = "";
        public static int? ObjectId = null;

        public static IWebHostEnvironment _appEnvironment;
        public static async void AddFileToFolder(IFormFile uploadedFile)
        {

            if (uploadedFile != null)
            {
                // путь к папке Files

                string path = "/Files/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                FileModel file = new FileModel { Name = uploadedFile.FileName, Path = path };

            }


        }


        public static void Init(string controllerName, string controllerAction, int? objectId, byte[] image)
        {

            RequestControllerName = controllerName;
            RequestControllerAction = controllerAction;
            ObjectId = objectId;

            if (RequestControllerAction == "Edit")
            {
                if (RequestFrom != RequestFrom.AddFile)
                {
                   Image = image;
                }
                RequestFrom = RequestFrom.Edit;
            }

            if (RequestControllerAction == "Create")
            {
                if (RequestFrom != RequestFrom.AddFile)
                {
                    Image = image;
                }
                RequestFrom = RequestFrom.Create;
            }

        }

        public static byte[] SelectImage(IFormFile uploadedFile)
        {

            if (uploadedFile != null)
            {

                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(uploadedFile.OpenReadStream()))
                {
                    Image = binaryReader.ReadBytes((int)uploadedFile.Length);
                }
            }
            RequestFrom = RequestFrom.AddFile;
            return Image;

        }

        
      
    }
}
