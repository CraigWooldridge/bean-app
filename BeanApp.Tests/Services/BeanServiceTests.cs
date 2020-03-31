using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BeanApp.Domain.Models;
using BeanApp.Domain.Repositories;
using BeanApp.Services;
using Moq;
using NUnit.Framework;

namespace BeanApp.Tests.Services
{
    [TestFixture]
    public class BeanServiceTests
    {
        public Mock<IBeanRepository> BeanRepositorySetup()
        {
            var mockBeanRepository = new Mock<IBeanRepository>();
            var bean = new Bean("TestBean", "Test Aroma", "TestColour", 1M, DateTime.Today.AddDays(1),
                new BeanImage("Test Name", 123, "Test type", "Test file location")
                {
                    Id = 1
                })
            {
                Id = 1
            };

            mockBeanRepository.Setup(s => s.GetBeanOfTheDay())
                .ReturnsAsync(bean);

            mockBeanRepository.Setup(s => s.GetBeanById(1))
                .ReturnsAsync(bean);

            mockBeanRepository.Setup(s => s.Create(It.IsAny<Bean>()))
                .ReturnsAsync(bean);

            mockBeanRepository.Setup(s => s.Edit(1))
                .ReturnsAsync(bean);

            mockBeanRepository.Setup(s => s.Edit(1, It.IsAny<Bean>()))
                .ReturnsAsync(bean);

            mockBeanRepository.Setup(s => s.Delete(It.IsAny<Bean>()))
                .ReturnsAsync(true);

            mockBeanRepository.Setup(s => s.DateCheck(It.IsAny<Bean>()))
                .ReturnsAsync(bean);

            return mockBeanRepository;
        }

        [Test]
        public async Task GetAll_Should_Return_A_List_Of_Beans()
        {
            var mockBeanRepository = new Mock<IBeanRepository>();
            var beanList = new List<Bean>();
            for (var i = 0; i < 25; i++)
            {
                beanList.Add(new Bean("TestBean", "Test Aroma", "TestColour", 1M, DateTime.Today.AddDays(i),
                    new BeanImage("Test Name", 123, "Test type", "Test file location")
                    {
                        Id = i
                    })
                {
                    Id = i
                });
            };

            mockBeanRepository.Setup(s => s.GetAll())
                .ReturnsAsync(beanList); 

            var beanService = new BeanService(mockBeanRepository.Object);
            var result = await beanService.GetAll();
            var resultBody = result as List<Bean>;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Bean>>(resultBody);
            Assert.AreEqual(resultBody.Count, 25);
        }

        [Test]
        public async Task GetBeanOfDay_Should_Return_The_Bean_Of_Day()
        {
            var beanService = new BeanService(BeanRepositorySetup().Object);
            var result = await beanService.GetBeanOfTheDay();
            var resultBody = result as Bean;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Bean>(resultBody);
        }

        [Test]
        public async Task GetBeanById_Should_Return_A_Single_Bean()
        {
            var beanService = new BeanService(BeanRepositorySetup().Object);
            var result = await beanService.GetBeanById(1);
            var resultBody = result as Bean;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Bean>(resultBody);
        }

        [Test]
        public async Task Create_Should_Return_Created_Bean()
        {
            var beanService = new BeanService(BeanRepositorySetup().Object);
            var result = await beanService.Create(
                new Bean("TestBean", "Test Aroma", "TestColour", 1M, DateTime.Today.AddDays(1), null)
                {
                    Id = 1
                });
            var resultBody = result as Bean;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Bean>(resultBody);
        }

        [Test]
        public async Task Edit_Should_Return_A_Single_Bean()
        {
            var beanService = new BeanService(BeanRepositorySetup().Object);
            var result = await beanService.Edit(1);
            var resultBody = result as Bean;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Bean>(resultBody);
        }

        [Test]
        public async Task EditBean_Should_Return_A_Edited_Bean()
        {
            var beanService = new BeanService(BeanRepositorySetup().Object);
            var result = await beanService.Edit(1,
                new Bean("TestBean", "Test Aroma", "TestColour", 1M, DateTime.Today.AddDays(1), null)
                {
                    Id = 1
                });
            var resultBody = result as Bean;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Bean>(resultBody);
        }

        [Test]
        public async Task Delete_Should_Return_True()
        {
            var beanService = new BeanService(BeanRepositorySetup().Object);
            var result = await beanService.Delete(It.IsAny<Bean>());

            Assert.IsNotNull(result);
            Assert.AreEqual(result, true);
        }

        [Test]
        public async Task DateCheck_Should_Return_A_Valid_Bean()
        {
            var beanService = new BeanService(BeanRepositorySetup().Object);
            var result = await beanService.DateCheck(It.IsAny<Bean>());
            var resultBody = result as Bean;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Bean>(resultBody);
        }
    }
}
