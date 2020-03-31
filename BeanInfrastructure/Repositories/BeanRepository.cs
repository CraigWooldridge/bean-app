using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeanApp.Domain.Models;
using BeanApp.Domain.Repositories;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BeanApp.Infrastructure.Repositories
{
    public class BeanRepository : IBeanRepository
    {
        private readonly BeanWebAppContext _context;

        public BeanRepository(
            BeanWebAppContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Bean>> GetAll()
        {
            return await _context.Beans.ToListAsync();
        }

        public async Task<Bean> GetBeanOfTheDay()
        {
            return await _context.Beans
                .Include(bean => bean.Image)
                .Where(bean => bean.DateToBeShownOn == DateTime.Today)
                .FirstOrDefaultAsync();
        }

        public async Task<Bean> GetBeanById(int id)
        {
            return await _context.Beans
                .Include(bean => bean.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Bean> Create(Bean bean)
        {
            var createdBean =_context.Beans.Add(bean);
            await _context.SaveChangesAsync();
            return createdBean.Entity;
        }

        public async Task<Bean> Edit(int id)
        {
            return await _context.Beans.FindAsync(id);
        }

        public async Task<Bean> Edit(int id, Bean bean)
        {
            var updatedBean =_context.Beans.Update(bean);
            await _context.SaveChangesAsync();
            return updatedBean.Entity;
        }

        public async Task<bool> Delete(Bean bean)
        {
            _context.Beans.Remove(bean);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Bean> DateCheck(Bean bean)
        {
            return await _context.Beans
                .Where(b => b.DateToBeShownOn == bean.DateToBeShownOn && b.Id != bean.Id)
                .FirstOrDefaultAsync();
        }
    }
}
