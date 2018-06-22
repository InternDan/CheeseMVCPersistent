using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CheeseMVC.Controllers
{
    public class MenuController : Controller
    {
        private CheeseDbContext context;

        public MenuController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {

            return View(context.Menus.ToList());
        }

        public IActionResult ViewMenu(int id)
        {
            Menu menu = context.Menus.SingleOrDefault(m => m.ID == id);
            List<CheeseMenu> items = context
                .CheeseMenus
                .Include(item => item.Cheese)
                .Where(cm => cm.MenuID == id)
                .ToList();
            ViewMenuViewModel model = new ViewMenuViewModel(items);
            model.Menu = menu;
            model.Items = items;

            return View(model);
        }

        public IActionResult Add()
        {
            AddMenuViewModel model = new AddMenuViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(AddMenuViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                Menu newMenu = new Menu { Name = model.Name };
                context.Menus.Add(newMenu);
                context.SaveChanges();
                return Redirect("/Menu/ViewMenu/" + newMenu.ID.ToString());
            }

        }

        public IActionResult AddItem(int id)
        {
            Menu menu = context.Menus.SingleOrDefault(m => m.ID == id);
            AddMenuItemViewModel model = new AddMenuItemViewModel(menu, context.Cheeses.ToList());
            return View(model);
        }

        [HttpPost]
        public IActionResult AddItem(AddMenuItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                IList<CheeseMenu> existingItems = context.CheeseMenus
                    .Where(cm => cm.CheeseID == model.CheeseID)
                    .Where(cm => cm.MenuID == model.MenuID).ToList();

                if (existingItems.Count == 0)
                {
                    //TODO clean this up
                    CheeseMenu cm = new CheeseMenu();
                    cm.CheeseID = model.CheeseID;
                    cm.MenuID = model.MenuID;
                    context.CheeseMenus.Add(cm);
                    context.SaveChanges();
                    return Redirect("/Menu/ViewMenu/" + model.MenuID.ToString());
                }
            }
            return View();
        }
    }
}