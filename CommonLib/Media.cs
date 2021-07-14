using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace CommonLib
{
    public class FileModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
    public static class Media
    {

        static IWebHostEnvironment _appEnvironment;
        public static async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {

            if (uploadedFile != null)
            {
                // путь к папке Files

                string path = "/Files/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new System.IO.FileStream(_appEnvironment.WebRootPath + path, System.IO.FileAccess.Write))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                FileModel file = new FileModel { Name = uploadedFile.FileName, Path = path };
                //_context.Files.Add(file);
                //_context.SaveChanges();
            }

            return null;// RedirectToAction("Index");
        }
    }
}
