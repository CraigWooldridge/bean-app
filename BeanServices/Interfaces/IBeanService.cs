using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BeanApp.Domain.Models;

namespace BeanApp.Services.Interfaces
{
    public interface IBeanService
    {
        Task<IEnumerable<Bean>> GetAll();
        Task<Bean> GetBeanOfTheDay();
        Task<Bean> GetBeanById(int id);
        Task<Bean> Create(Bean bean);
        Task<Bean> Edit(int id);
        Task<Bean> Edit(int id, Bean bean);
        Task<bool> Delete(Bean bean);
        Task<Bean> DateCheck(Bean bean);
    }
}
