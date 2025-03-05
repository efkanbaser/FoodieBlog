using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieBlog.Business.Common
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;

        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task<string> SaveImageAsync(IFormFile imageFile, string folderPath)
        {
            // This works but this is terrible and won't work in unix based systems
            // Ensure the folder exists
            var webRootPath = Path.Combine(_env.WebRootPath, folderPath.TrimStart('/'));
            //var webRootPath = Directory.GetCurrentDirectory() + "/wwwroot" + folderPath;
            if (!Directory.Exists(webRootPath))
            {
                Directory.CreateDirectory(webRootPath);
            }

            // Generate a unique file name
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(webRootPath, fileName).Replace("\\", "/");

            // Save the file to the wwwroot folder
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            // Return the relative path to store in the database
            return Path.Combine(folderPath, fileName).Replace("\\", "/");
        }
    }
}
