using BeanApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeanApp.Infrastructure.Configuration
{
    internal class BeanImageConfiguration : IEntityTypeConfiguration<BeanImage>
    {
        public void Configure(EntityTypeBuilder<BeanImage> builder)
        {
            builder.HasKey(image => image.Id);

            builder.HasOne(image => image.Bean)
                .WithOne(bean => bean.Image)
                .HasForeignKey<Bean>(b => b.Id);
        }
    }
}
