using Microsoft.AspNetCore.Mvc;
using PRPicks.Models;

namespace PRPicks.Components.ViewComponents
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var pRList = new List<PRList>
        {
            new PRList { Controller = "Home", Action = "Index", Label = "Home" },
            new PRList { Controller = "Collections", Action = "Index", Label = "Collection" },
            new PRList { Controller = "Products", Action = "Index", Label = "Products" },
            new PRList { Controller = "Carts", Action = "Index", Label = "View my cart" },
            new PRList { Controller = "Home", Action = "", Label = "About" },
            new PRList { Controller = "Home", Action = "Brief", Label = "Brief" },
        };

            return View(pRList);
        }
    }
}