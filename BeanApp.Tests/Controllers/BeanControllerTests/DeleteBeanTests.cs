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
    public class DeleteBeanTests
    {
        public Mock<IBeanService> BeanSetup()
        {
            var mockBeanService = new Mock<IBeanService>();
            var bean = new Bean("TestBean", "Test Aroma", "TestColour", 1M, DateTime.Today.AddDays(1),
                new BeanImage("Test Name", 123, "Test type", "Test file location")
                {
                    Id = 1
                })
            {
                Id = 1
            };

            mockBeanService.Setup(s => s.GetBeanById(1))
                .ReturnsAsync(bean);
            mockBeanService.Setup(s => s.Delete(bean))
                .ReturnsAsync(true);

            return mockBeanService;
        }
        public Mock<IBeanService> FailedBeanSetup()
        {
            var mockBeanService = new Mock<IBeanService>();

            mockBeanService.Setup(s => s.GetBeanById(1))
                .Throws(new Exception());
            mockBeanService.Setup(s => s.Delete(It.IsAny<Bean>()))
                .Throws(new Exception());

            return mockBeanService;
        }

        public Mock<IBeanImageService> BeanImageSetup()
        {
            var mockBeanImageService = new Mock<IBeanImageService>();
            return mockBeanImageService;
        }


        [Test]
        public async Task Delete_Should_Return_The_Delete_Page()
        {
            // Act
            var beanController = new BeansController(BeanSetup().Object, BeanImageSetup().Object);
            var result = await beanController.Delete(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task ViewDeletePage_Should_Return_NotFound_If_Bean_Is_Null()
        {

            // Act
            var beanController = new BeansController(BeanSetup().Object, BeanImageSetup().Object);
            var result = await beanController.Delete(100) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.True(result.ViewName is "NotFound");
        }

        [Test]
        public async Task ViewDeletePage_Should_Return_A_500_Error_If_Exception_Is_Thrown()
        {
            // Act
            var beanController = new BeansController(FailedBeanSetup().Object, BeanImageSetup().Object);
            var result = await beanController.Delete(1) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.True(result.ActionName is "HttpStatusCodeHandler, Error");
        }

        [Test]
        public async Task Delete_Should_Delete_Bean_And_Return_To_Homepage()
        {
            // Act
            var beanController = new BeansController(BeanSetup().Object, BeanImageSetup().Object);
            var result = await beanController.DeleteConfirmed(1) as RedirectToActionResult;
         

            // Assert
            Assert.IsNotNull(result);
            Assert.True(result.ActionName is "Index");
        }

        [Test]
        public async Task Delete_Should_Return_A_500_Error_If_Exception_Is_Thrown()
        {
            // Act
            var beanController = new BeansController(FailedBeanSetup().Object, BeanImageSetup().Object);
            var result = await beanController.DeleteConfirmed(1) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.True(result.ActionName is "HttpStatusCodeHandler, Error");
        }
    }
}
