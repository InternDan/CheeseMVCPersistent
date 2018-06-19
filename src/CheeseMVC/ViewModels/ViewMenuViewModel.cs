using CheeseMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.ViewModels
{
    public class ViewMenuViewModel
    {
        public Menu Menu { get; set; }
        public IList<CheeseMenu> Items { get; set; }

        //TODO maybe add a constructor that accepts a list of CheeseMenu objects?

        public ViewMenuViewModel(IList<CheeseMenu> cheeseMenus)
        {
            Items = cheeseMenus;
        }

    }
}
