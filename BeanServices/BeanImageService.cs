using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BeanApp.Domain.Models;
using BeanApp.Domain.Repositories;
using BeanApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace BeanApp.Services
{
    public class BeanImageService : IBeanImageService
    {
        private readonly IBeanImageRepository _beanImageRepository;
        private IHostEnvironment _hostingEnvironment;

        public BeanImageService(
            IBeanImageRepository beanImageRepository,
            IHostEnvironment hostingEnviroment)
        {
            _beanImageRepository = beanImageRepository;
            _hostingEnvironment = hostingEnviroment;
        }
        public async Task<BeanImage> UploadImage(IFormFile image)
        {
            try
            {
                var uploads = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot\\uploads");
                if (image.Length > 0)
                {
                    var fileName = image.FileName;
                    var fileLength = image.Length;
                    var filePath = Path.Combine(uploads, image.FileName);
                    var fileType = Path.GetExtension(filePath);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }

                    var fileLocation = "/uploads/" + fileName;
                    var imageToUpload = new BeanImage(
                        fileName,
                        fileLength,
                        fileType,
                        fileLocation);

                    return await _beanImageRepository.UploadImage(imageToUpload);
                }
                return null;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return null;
            }
        }
    }
}
