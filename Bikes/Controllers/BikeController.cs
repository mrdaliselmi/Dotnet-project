using Bikes.AppDbContext;
using Bikes.Helpers;
using Bikes.Models;
using Bikes.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Hosting.Internal;

namespace Bikes.Controllers
{
    public class BikeController : Controller
    {
        private readonly VroomDbContext vroomDbContext;
        private readonly HostingEnvironment _hostingEnvironment;


        [BindProperty]
        public BikeViewModel BikeVM { get; set; }
        public BikeController(VroomDbContext vroomDbContext)
        {
            this.vroomDbContext = vroomDbContext;
            BikeVM = new BikeViewModel()
            {
                Makes = vroomDbContext.Makes.ToList(),
                Models = vroomDbContext.Models.ToList(),
                Bike = new Models.Bike(),
            };
            //this._hostingEnvironment = he;

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
            //string wwrootPath = _hostingEnvironment.WebRootPath;



            return RedirectToAction(nameof(Index));
            //}
            //return View(modelVm);
        }


        //public IActionResult Delete(int id)
        //{
        //    {
        //        var bike = vroomDbContext.Bikes.Find(id);
        //        if (bike == null)
        //        {
        //            return NotFound();
        //        }
        //        vroomDbContext.Bikes.Remove(bike);
        //        vroomDbContext.SaveChanges();
        //        return (RedirectToAction(nameof(Index)));
        //    }
        //}


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
