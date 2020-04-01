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
    public class CreateBeanTests
    {
        public Mock<IBeanService> BeanSetup()
        {
            var mockBeanService = new Mock<IBeanService>();

            mockBeanService.Setup(s => s.Create(It.IsAny<Bean>()))
                .ReturnsAsync(new Bean("TestBean", "Test Aroma", "TestColour", 1M, DateTime.Today.AddDays(1),
                    new BeanImage("Test Name", 123, "Test type", "Test file location")
                    {
                        Id = 1
                    })
                {
                    Id = 1
                });

            mockBeanService.Setup(s => s.DateCheck(It.IsAny<Bean>()))
                .ReturnsAsync((Bean)null);

            return mockBeanService;
        }
        public Mock<IBeanService> FailedBeanSetup()
        {
            var mockBeanService = new Mock<IBeanService>();

            mockBeanService.Setup(s => s.Create(It.IsAny<Bean>()))
                .Throws(new Exception());

            return mockBeanService;
        }

        public Mock<IBeanImageService> BeanImageSetup()
        {
            var mockBeanImageService = new Mock<IBeanImageService>();
            mockBeanImageService.Setup(s => s.UploadImage(It.IsAny<IFormFile>()))
                .ReturnsAsync(new BeanImage("Test Name", 123, "Test type", "Test file location")
                {
                    Id = 1
                });

            return mockBeanImageService;
        }

        public Mock<IFormFile> FileSetup()
        {
            var mockFile = new Mock<IFormFile>();
            return mockFile;
        }

        [Test]
        public void Create_Should_Return_The_Create_Page()
        {
            // Act
            var beanController = new BeansController(BeanSetup().Object, BeanImageSetup().Object);
            var result = beanController.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Create_Should_Return_New_Bean()
        {
            // Act
            var beanController = new BeansController(BeanSetup().Object, BeanImageSetup().Object);
            var result = await beanController.Create(
                    new Bean("TestBean", "Test Aroma", "TestColour", 1M, DateTime.Today.AddDays(1), null)
                    {
                        Id = 1
                    },
                    FileSetup().Object) as RedirectToActionResult;
         

            // Assert
            Assert.IsNotNull(result);
            Assert.True(result.ActionName is "Index");
        }

        [Test]
        public async Task Create_Should_Return_NoImage_If_file_Is_Null()
        {

            // Act
            var beanController = new BeansController(BeanSetup().Object, BeanImageSetup().Object);
            var result = await beanController.Create(
                new Bean("TestBean", "Test Aroma", "TestColour", 1M, DateTime.Today.AddDays(1),
                    new BeanImage("Test Name", 123, "Test type", "Test file location")
                    {
                        Id = 1
                    })
                {
                    Id = 1
                },
                null) as ViewResult;
            

            // Assert
            Assert.IsNotNull(result);
            Assert.True(result.ViewName is "NoImage");
        }

        [Test]
        public async Task Create_Should_Return_InvalidDate_If_BeanDate_Already_Exists()
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
            var result = await beanController.Create(It.IsAny<Bean>(), null) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.True(result.ViewName is "InvalidDate");
        }

        [Test]
        public async Task Create_Should_Return_A_500_Error_If_Exception_Is_Thrown()
        {
            // Act
            var beanController = new BeansController(FailedBeanSetup().Object, BeanImageSetup().Object);
            var result = await beanController.Create(
                new Bean("TestBean", "Test Aroma", "TestColour", 1M, DateTime.Today.AddDays(1), null)
                {
                    Id = 1
                },
                FileSetup().Object) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.True(result.ActionName is "HttpStatusCodeHandler, Error");
        }
    }
}
