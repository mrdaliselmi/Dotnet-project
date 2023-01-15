using Bikes.AppDbContext;
using Bikes.Helpers;
using Bikes.Models;
using Bikes.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using cloudscribe.Pagination.Models;
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


        public IActionResult Index(String searchString, String sortOrder,int pageNumber=1, int pageSize=2)
        {
            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.CurrentFilter = searchString;
            ViewBag.PriceSortParam = String.IsNullOrEmpty(sortOrder) ? "Price_desc" : "";
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            var bikes = from b in vroomDbContext.Bikes.Include(m => m.Make).Include(m => m.Model)
                        select b;
            var BikeCount = bikes.Count();
            if (!String.IsNullOrEmpty(searchString) )
            {
                bikes = bikes.Where(b => b.Make.Name.Contains(searchString));
                BikeCount = bikes.Count();
            }
            //Sorting Logic
            switch (sortOrder)
            {
                case "Price_desc":
                    bikes = bikes.OrderByDescending(b => b.Price);
                    break;
                default:
                    bikes = bikes.OrderBy(b => b.Price);
                    break;
            }
            bikes = bikes
                .Skip(ExcludeRecords)
                .Take(pageSize);
            var result = new PagedResult<Bike>
            {
                Data = bikes.AsNoTracking().ToList(),
                TotalItems = BikeCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return View(result);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            BikeVM.Bike= vroomDbContext.Bikes.SingleOrDefault(b=> b.Id == id);
            BikeVM.Models= vroomDbContext.Models.Where(m=>m.Make== BikeVM.Bike.Make);
            if (BikeVM.Bike == null )
            {
                return NotFound();
            }
            return View(BikeVM);
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
        //
        //}
    }
}
