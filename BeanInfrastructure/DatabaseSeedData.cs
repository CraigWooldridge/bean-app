using System;
using System.Collections.Generic;
using System.Text;
using BeanApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace BeanApp.Infrastructure
{
    public static class DatabaseSeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BeanWebAppContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<BeanWebAppContext>>()))
            {
                if (context.Beans.Any())
                {
                    return;
                }

                context.Beans.AddRange(
                    new Bean("Baked Bean", "Fresh Tomato", "Orange", 1.00M, DateTime.Today, 
                        new BeanImage("beans.jfif", 12302, ".jfif", "/uploads/beans.jfif")
                        ),
                    new Bean("Runner Bean", "Earthy", "Green", 1.50M, DateTime.Today.AddDays(1),
                        new BeanImage("runner bean.jfif", 9628, ".jfif", "/uploads/runner bean.jfif")));
                context.SaveChanges();
            }
        }
    }
}
