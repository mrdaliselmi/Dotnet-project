using Bikes.AppDbContext;
using Bikes.Helpers;
using Bikes.Models;
using Bikes.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Hosting;
namespace Bikes.Controllers
{
    public class BikeController : Controller
    {
        private readonly VroomDbContext vroomDbContext;
        private readonly IWebHostEnvironment _hostingEnvironment;


        [BindProperty]
        public BikeViewModel BikeVM { get; set; }
        public BikeController(VroomDbContext vroomDbContext, IWebHostEnvironment hostingEnvironment)
        {
            this.vroomDbContext = vroomDbContext;
            BikeVM = new BikeViewModel()
            {
                Makes = vroomDbContext.Makes.ToList(),
                Models = vroomDbContext.Models.ToList(),
                Bike = new Models.Bike(),
            };
            _hostingEnvironment = hostingEnvironment;

        }

        public IActionResult Index()
        {

            var bikes = vroomDbContext.Bikes.Include(m => m.Make).Include(m => m.Model);
            return View(bikes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(BikeVM);
        }

        [HttpPost, ActionName("Create")]
        public IActionResult CreatePost()
        {
            //if (ModelState.IsValid)
            //{
            vroomDbContext.Add(this.BikeVM.Bike);
            vroomDbContext.SaveChanges();
            var BikeId = BikeVM.Bike.Id;
            string wwrootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            var savedBike = vroomDbContext.Bikes.Find(BikeId);
            if (files.Count != 0)
            {
                var ImagePath = @"images\bike\";
                var extension = Path.GetExtension(files[0].FileName);
                var relativeImagePath = ImagePath + BikeId + extension;
                var absImagePath = Path.Combine(wwrootPath, relativeImagePath);

                using (var FileStream = new FileStream(absImagePath, FileMode.Create))
                {
                    files[0].CopyTo(FileStream);
                }
                savedBike.ImagePath = relativeImagePath;
                vroomDbContext.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
            //}
            //return View(modelVm);
        }


        public IActionResult Delete(int id)
        {
            {
                var bike = vroomDbContext.Bikes.Find(id);
                if (bike == null)
                {
                    return NotFound();
                }
                vroomDbContext.Bikes.Remove(bike);
                vroomDbContext.SaveChanges();
                return (RedirectToAction(nameof(Index)));
            }
        }


        //[HttpGet]
        //public IActionResult Edit(int id)
        //{
        //    BikeVM.Bike = vroomDbContext.Bikes.Include(m => m.Make).Include(m => m.Model).SingleOrDefault(m => m.Id == id);
        //    if (BikeVM.Bike == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(BikeVM);
        //}


        //[HttpPost, ActionName("Edit")]
        //public IActionResult EditPost()
        //{
        //    vroomDbContext.Update(this.BikeVM.Bike);
        //    vroomDbContext.SaveChanges();
        //    return (RedirectToAction(nameof(Index)));

        //}
    }
}
