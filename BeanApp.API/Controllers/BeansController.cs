using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BeanApp.Domain.Models;
using BeanApp.Infrastructure;
using BeanApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace BeanApp.API.Controllers
{
    [Authorize]
    public class BeansController : Controller
    {
        private readonly IBeanService _beanService;
        private readonly IBeanImageService _beanImageService;

        public BeansController(IBeanService beanService, IBeanImageService beanImageService)
        {
            _beanService = beanService;
            _beanImageService = beanImageService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var beanList = await _beanService.GetAll();
            return View(beanList);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetBeanOfTheDay()
        {
            var dailyBean = await _beanService.GetBeanOfTheDay();
            if (dailyBean == null)
            {
                ViewBag.ErrorMessage = "The daily bean has not been posted at the moment, please try again later";
                return View("NotFound");
            }
            return View(dailyBean);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {

            var bean = await _beanService.GetBeanById(id);
            if (bean == null)
            {
                ViewBag.ErrorMessage = "Bean Not Found";
                return View("NotFound");
            }

            return View(bean);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BeanName,Aroma,Colour,CostPer100g,DateToBeShownOn,Image")] Bean bean, IFormFile file)
        {
            var dateCheck = await _beanService.DateCheck(bean);
            if (dateCheck != null)
            {
                ViewBag.ErrorMessage = "Invalid date, A bean of the day already exists for this date.";
                return View("InvalidDate");
            }

            if (file == null)
            {
                ViewBag.ErrorMessage = "You haven't selected an image for this Bean!";
                return View("NoImage");
            }

            if (ModelState.IsValid)
            {
                var uploadedImage = await _beanImageService.UploadImage(file);
                bean.Image = uploadedImage;

                var createdBean = await _beanService.Create(bean);
                return RedirectToAction(nameof(Index));
            }
            return View(bean);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var bean = await _beanService.GetBeanById(id);
            if (bean == null)
            {
                ViewBag.ErrorMessage = "Bean Not Found";
                return View("NotFound");
            }
            return View(bean);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BeanName,Aroma,Colour,CostPer100g,DateToBeShownOn")] Bean bean)
        {
            if (id != bean.Id)
            {
                ViewBag.ErrorMessage = "Bean Not Found";
                return View("NotFound");
            }

            var dateCheck = await _beanService.DateCheck(bean);
            if (dateCheck != null)
            {
                ViewBag.ErrorMessage = "Invalid date, A bean of the day already exists for this date.";
                return View("InvalidDate");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var updatedBean = await _beanService.Edit(id, bean);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bean);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var bean = await _beanService.GetBeanById(id);
            if (bean == null)
            {
                ViewBag.ErrorMessage = "Bean Not Found";
                return View("NotFound");
            }

            return View(bean);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bean = await _beanService.GetBeanById(id);
            await _beanService.Delete(bean);
            return RedirectToAction(nameof(Index));
        }
    }
}
