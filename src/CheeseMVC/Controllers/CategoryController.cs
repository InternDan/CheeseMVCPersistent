using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CheeseMVC.Controllers
{
    

    public class CategoryController : Controller
    {
        private readonly CheeseDbContext context;

        public CategoryController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            return View(context.Categories.ToList());
        }

        public IActionResult Add()
        {
            AddCategoryViewModel model = new AddCategoryViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(AddCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                CheeseCategory newCategory = new CheeseCategory();
                newCategory.Name = model.Name;
                context.Categories.Add(newCategory);
                context.SaveChanges();
                return Redirect("/Category");
            }
            else
            {
                return View(model);
            }
        }
    }
}