using System.Collections.Generic;
using System.Threading.Tasks;
using BeanApp.Domain.Models;
using BeanApp.Domain.Repositories;
using BeanApp.Services.Interfaces;

namespace BeanApp.Services
{
    public class BeanService : IBeanService
    {
        private readonly IBeanRepository _beanRepository;

        public BeanService(
            IBeanRepository beanRepository)
        {
            _beanRepository = beanRepository;
        }

        public async Task<IEnumerable<Bean>> GetAll()
        {
            return await _beanRepository.GetAll();
        }

        public async Task<Bean> GetBeanOfTheDay()
        {
            return await _beanRepository.GetBeanOfTheDay();
        }

        public async Task<Bean> GetBeanById(int id)
        {
            return await _beanRepository.GetBeanById(id);
        }

        public async Task<Bean> Create(Bean bean)
        {
            return await _beanRepository.Create(bean);
        }

        public async Task<Bean> Edit(int id)
        {
            return await _beanRepository.Edit(id);
        }

        public async Task<Bean> Edit(int id, Bean bean)
        {
            return await _beanRepository.Edit(id, bean);
        }

        public async Task<bool> Delete(Bean bean)
        {
            return await _beanRepository.Delete(bean);
        }

        public async Task<Bean> DateCheck(Bean bean)
        {
            return await _beanRepository.DateCheck(bean);
        }
    }
}
