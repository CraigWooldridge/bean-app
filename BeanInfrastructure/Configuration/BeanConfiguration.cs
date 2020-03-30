using System;
using System.Collections.Generic;
using System.Text;
using BeanApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeanApp.Infrastructure.Configuration
{
    internal class BeanConfiguration : IEntityTypeConfiguration<Bean>
    {
        public void Configure(EntityTypeBuilder<Bean> builder)
        {
            builder.HasKey(bean => bean.Id);

            builder.HasOne(bean => bean.Image)
                .WithOne(image => image.Bean)
                .HasForeignKey<BeanImage>(b => b.Id);
        }
    }
}
