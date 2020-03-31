using System.Threading.Tasks;
using BeanApp.Domain.Models;
using BeanApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            try
            {
                var beanList = await _beanService.GetAll();
                return View(beanList);
            }
            catch
            {
                return RedirectToAction("HttpStatusCodeHandler, Error");
            }
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetBeanOfTheDay()
        {
            try
            {
                var dailyBean = await _beanService.GetBeanOfTheDay();
                if (dailyBean == null)
                {
                    ViewBag.ErrorMessage = "The daily bean has not been posted at the moment, please try again later";
                    return View("NotFound");
                }

                return View(dailyBean);
            }
            catch
            {
                return RedirectToAction("HttpStatusCodeHandler, Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var bean = await _beanService.GetBeanById(id);
                if (bean == null)
                {
                    ViewBag.ErrorMessage = "Bean Not Found";
                    return View("NotFound");
                }

                return View(bean);
            }
            catch
            {
                return RedirectToAction("HttpStatusCodeHandler, Error");
            }
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
            try
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
            catch
            {
                return RedirectToAction("HttpStatusCodeHandler, Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var bean = await _beanService.GetBeanById(id);
                if (bean == null)
                {
                    ViewBag.ErrorMessage = "Bean Not Found";
                    return View("NotFound");
                }

                return View(bean);
            }
            catch
            {
                return RedirectToAction("HttpStatusCodeHandler, Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BeanName,Aroma,Colour,CostPer100g,DateToBeShownOn")] Bean bean)
        {
            try
            {
                var dateCheck = await _beanService.DateCheck(bean);
                if (dateCheck != null)
                {
                    ViewBag.ErrorMessage = "Invalid date, A bean of the day already exists for this date.";
                    return View("InvalidDate");
                }

                if (id != bean.Id)
                {
                    ViewBag.ErrorMessage = "Bean Not Found";
                    return View("NotFound");
                }

                if (ModelState.IsValid)
                {
                    var updatedBean = await _beanService.Edit(id, bean);

                    return RedirectToAction(nameof(Index));
                }
                return View(bean);
            }
            catch
            {
                return RedirectToAction("HttpStatusCodeHandler, Error");
            } 
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var bean = await _beanService.GetBeanById(id);
                if (bean == null)
                {
                    ViewBag.ErrorMessage = "Bean Not Found";
                    return View("NotFound");
                }

                return View(bean);
            }
            catch
            {
                return RedirectToAction("HttpStatusCodeHandler, Error");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var bean = await _beanService.GetBeanById(id);
                await _beanService.Delete(bean);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("HttpStatusCodeHandler, Error");
            }
        }
    }
}
