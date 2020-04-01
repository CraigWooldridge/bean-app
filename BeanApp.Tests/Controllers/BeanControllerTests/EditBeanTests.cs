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
    public class EditBeanTests
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

            mockBeanService.Setup(s => s.Edit(It.IsAny<int>(), It.IsAny<Bean>()))
                .ReturnsAsync(bean);

            mockBeanService.Setup(s => s.GetBeanById(1))
                .ReturnsAsync(bean);

            mockBeanService.Setup(s => s.DateCheck(It.IsAny<Bean>()))
                .ReturnsAsync((Bean)null);

            return mockBeanService;
        }
        public Mock<IBeanService> FailedBeanSetup()
        {
            var mockBeanService = new Mock<IBeanService>();

            mockBeanService.Setup(s => s.Edit(It.IsAny<int>(), It.IsAny<Bean>()))
                .Throws(new Exception());
            mockBeanService.Setup(s => s.GetBeanById(1))
                .Throws(new Exception());

            return mockBeanService;
        }

        public Mock<IBeanImageService> BeanImageSetup()
        {
            var mockBeanImageService = new Mock<IBeanImageService>();
            return mockBeanImageService;
        }


        [Test]
        public async Task Edit_Should_Return_The_Edit_Page()
        {
            // Act
            var beanController = new BeansController(BeanSetup().Object, BeanImageSetup().Object);
            var result = await beanController.Edit(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Bean>(result.Model);
        }

        [Test]
        public async Task ViewEditPage_Should_Return_NotFound_If_Bean_Is_Null()
        {

            // Act
            var beanController = new BeansController(BeanSetup().Object, BeanImageSetup().Object);
            var result = await beanController.Edit(100) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.True(result.ViewName is "NotFound");
        }

        [Test]
        public async Task ViewEditPage_Should_Return_A_500_Error_If_Exception_Is_Thrown()
        {
            // Act
            var beanController = new BeansController(FailedBeanSetup().Object, BeanImageSetup().Object);
            var result = await beanController.Edit(1) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.True(result.ActionName is "HttpStatusCodeHandler, Error");
        }

        [Test]
        public async Task Edit_Should_Return_Edited_Bean()
        {
            // Act
            var beanController = new BeansController(BeanSetup().Object, BeanImageSetup().Object);
            var result = await beanController.Edit(1,
                    new Bean("TestBean", "Test Aroma", "TestColour", 1M, DateTime.Today.AddDays(1), null)
                    {
                        Id = 1
                    }) as RedirectToActionResult;
         

            // Assert
            Assert.IsNotNull(result);
            Assert.True(result.ActionName is "Index");
        }

        [Test]
        public async Task Edit_Should_Return_InvalidDate_If_BeanDate_Already_Exists()
        {
            var mockBeanService = new Mock<IBeanService>();
            mockBeanService.Setup(s => s.DateCheck(It.IsAny<Bean>()))
                .ReturnsAsync(new Bean("TestBean", "Test Aroma", "TestColour", 1M, DateTime.Today.AddDays(1),
                    new BeanImage("Test Name", 123, "Test type", "Test file location")
                    {
                        Id = 1
                    })
                {
                    Id = 1
                });

            // Act
            var beanController = new BeansController(mockBeanService.Object, BeanImageSetup().Object);
            var result = await beanController.Edit(1, It.IsAny<Bean>()) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.True(result.ViewName is "InvalidDate");
        }

        [Test]
        public async Task Edit_Should_Return_NotFound_If_Bean_Is_Null()
        {

            // Act
            var beanController = new BeansController(BeanSetup().Object, BeanImageSetup().Object);
            var result = await beanController.Edit(100,
                new Bean("TestBean", "Test Aroma", "TestColour", 1M, DateTime.Today.AddDays(1), null)
                {
                    Id = 1
                }) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.True(result.ViewName is "NotFound");
        }

        [Test]
        public async Task Edit_Should_Return_A_500_Error_If_Exception_Is_Thrown()
        {
            // Act
            var beanController = new BeansController(FailedBeanSetup().Object, BeanImageSetup().Object);
            var result = await beanController.Edit(1, 
                new Bean("TestBean", "Test Aroma", "TestColour", 1M, DateTime.Today.AddDays(1), null)
                {
                    Id = 1
                }) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.True(result.ActionName is "HttpStatusCodeHandler, Error");
        }
    }
}
