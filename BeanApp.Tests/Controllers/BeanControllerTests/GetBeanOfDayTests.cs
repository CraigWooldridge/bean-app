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
    public class GetBeanOfDayTests
    {
        public Mock<IBeanService> BeanSetup()
        {
            var mockBeanService = new Mock<IBeanService>();

            mockBeanService.Setup(s => s.GetBeanOfTheDay())
                .ReturnsAsync(new Bean("TestBean", "Test Aroma", "TestColour", 1M, DateTime.Today.AddDays(1),
                    new BeanImage("Test Name", 123, "Test type", "Test file location")
                    {
                        Id = 1
                    })
                {
                    Id = 1
                });

            return mockBeanService;
        }
        public Mock<IBeanService> FailedBeanSetup()
        {
            var mockBeanService = new Mock<IBeanService>();

            mockBeanService.Setup(s => s.GetBeanOfTheDay())
                .Throws(new Exception());

            return mockBeanService;
        }

        public Mock<IBeanImageService> BeanImageSetup()
        {
            var mockBeanImageService = new Mock<IBeanImageService>();
            return mockBeanImageService;
        }

        [Test]
        public async Task GetBeanOfDay_Should_Return_The_Bean_Of_Day()
        {
            // Act
            var beanController = new BeansController(BeanSetup().Object, BeanImageSetup().Object);
            var result = await beanController.GetBeanOfTheDay() as ViewResult;
            var resultBody = result.Model as Bean;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Bean>(resultBody);
            Assert.AreEqual("TestBean", resultBody.BeanName);
        }

        [Test]
        public async Task GetBeanOfDay_Should_Return_NotFound_If_Bean_Of_Day_Is_Null()
        {
            var mockBeanService = new Mock<IBeanService>();

            mockBeanService.Setup(s => s.GetBeanOfTheDay())
                .ReturnsAsync(It
                    .IsAny<Bean>);

            // Act
            var beanController = new BeansController(mockBeanService.Object, BeanImageSetup().Object);
            var result = await beanController.GetBeanOfTheDay() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.True(result.ViewName is "NotFound");
        }

        [Test]
        public async Task GetBeanOfDay_Should_Return_A_500_Error_If_Exception_Is_Thrown()
        {
            // Act
            var beanController = new BeansController(FailedBeanSetup().Object, BeanImageSetup().Object);
            var result = await beanController.GetBeanOfTheDay() as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.True(result.ActionName is "HttpStatusCodeHandler, Error");
        }
    }
}
