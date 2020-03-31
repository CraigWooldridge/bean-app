using System.Threading.Tasks;
using BeanApp.Domain.Models;
using BeanApp.Domain.Repositories;

namespace BeanApp.Infrastructure.Repositories
{
    public class BeanImageRepository : IBeanImageRepository
    {
        private readonly BeanWebAppContext _context;

        public BeanImageRepository(
            BeanWebAppContext context)
        {
            _context = context;
        }

        public async Task<BeanImage> UploadImage(BeanImage image)
        {
            var beanImage =_context.BeanImages.Add(image);
            await _context.SaveChangesAsync();
            return beanImage.Entity;
        }
    }
}
