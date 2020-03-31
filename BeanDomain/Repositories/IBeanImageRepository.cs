using System.Threading.Tasks;
using BeanApp.Domain.Models;

namespace BeanApp.Domain.Repositories
{
    public interface IBeanImageRepository
    {
        Task<BeanImage> UploadImage(BeanImage image);
    }
}
