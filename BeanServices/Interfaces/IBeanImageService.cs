using System.Threading.Tasks;
using BeanApp.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace BeanApp.Services.Interfaces
{
    public interface IBeanImageService
    {
        Task<BeanImage> UploadImage(IFormFile image);
    }
}
