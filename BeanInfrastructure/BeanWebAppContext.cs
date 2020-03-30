using System;
using System.Collections.Generic;
using System.Text;
using BeanApp.Domain.Models;
using BeanApp.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace BeanApp.Infrastructure
{
    public class BeanWebAppContext : IdentityDbContext 
    {
        public BeanWebAppContext(DbContextOptions<BeanWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<Bean> Beans { get; set; }
        public DbSet<BeanImage> BeanImages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfiguration(new BeanConfiguration());
            builder.ApplyConfiguration(new BeanImageConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
