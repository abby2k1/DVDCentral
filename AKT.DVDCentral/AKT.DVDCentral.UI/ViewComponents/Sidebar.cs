using AKT.DVDCentral.BL;
using Microsoft.AspNetCore.Mvc;

namespace AKT.DVDCentral.UI.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(GenreManager.Load()
                .OrderBy(p => p.Description));
        }
    }
}
