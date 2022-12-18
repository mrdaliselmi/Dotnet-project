using Bikes.AppDbContext;
using Bikes.Models;
using Bikes.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bikes.Controllers
{
    public class ModelController : Controller
    {
        private readonly VroomDbContext vroomDbContext;

        [BindProperty]
        public ModelViewModel modelVm { get; set; }
        public ModelController(VroomDbContext vroomDbContext)
        {
            this.vroomDbContext = vroomDbContext;
            modelVm = new ModelViewModel()
            {
                Makes = vroomDbContext.Makes.ToList(),
                Model = new Models.Model()
            };

        }

        public IActionResult Index()
        {

            var model = vroomDbContext.Models.Include(m => m.Make);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(modelVm);
        }

        [HttpPost, ActionName("Create")]
        public IActionResult CreatePost()
        {
            //if (ModelState.IsValid)
            //{
            vroomDbContext.Add(this.modelVm.Model);
            vroomDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
            //}
            //return View(modelVm);
        }


        public IActionResult Delete(int id)
        {
            {
                var model = vroomDbContext.Models.Find(id);
                if (model == null)
                {
                    return NotFound();
                }
                vroomDbContext.Models.Remove(model);
                vroomDbContext.SaveChanges();
                return (RedirectToAction(nameof(Index)));
            }
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            modelVm.Model = vroomDbContext.Models.Include(m => m.Make).SingleOrDefault(m => m.Id == id);
            if (modelVm.Model == null)
            {
                return NotFound();
            }
            return View(modelVm);
        }


        [HttpPost, ActionName("Edit")]
        public IActionResult EditPost()
        {
            vroomDbContext.Update(this.modelVm.Model);
            vroomDbContext.SaveChanges();
            return (RedirectToAction(nameof(Index)));

        }
    }
}
