using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BeanApp.API.Controllers;
using BeanApp.Domain.Models;
using BeanApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Moq;

namespace BeanApp.Tests.Controllers
{
    [TestFixture]
    public class IndexBeanTests
    {
        public Mock<IBeanService> BeanSetup()
        {
            var mockBeanService = new Mock<IBeanService>();
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
            }

            mockBeanService.Setup(s => s.GetAll())
                .ReturnsAsync(beanList);

            return mockBeanService;
        }
        public Mock<IBeanService> FailedBeanSetup()
        {
            var mockBeanService = new Mock<IBeanService>();

            mockBeanService.Setup(s => s.GetAll())
                .Throws(new Exception());

            return mockBeanService;
        }

        public Mock<IBeanImageService> BeanImageSetup()
        {
            var mockBeanImageService = new Mock<IBeanImageService>();
            return mockBeanImageService;
        }

        [Test]
        public async Task Index_Should_Return_A_List_Of_Beans()
        {
            // Act
            var beanController = new BeansController(BeanSetup().Object, BeanImageSetup().Object);
            var result = await beanController.Index() as ViewResult;
            var resultBody = result.Model as List<Bean>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Bean>>(resultBody);
            Assert.AreEqual(resultBody.Count, 25);
        }

        [Test]
        public async Task Index_Should_Return_A_500_Error_If_Exception_Is_Thrown()
        {
            // Act
            var beanController = new BeansController(FailedBeanSetup().Object, BeanImageSetup().Object);
            var result = await beanController.Index() as ViewResult;

            // Assert
            Assert.IsNull(result);
        }
    }
}
