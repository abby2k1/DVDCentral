using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AKT.DVDCentral.BL;
using AKT.DVDCentral.BL.Models;

namespace AKT.DVDCentral.UI.Controllers
{
    public class OrderItemController : Controller
    {
        // GET: OrderItemController
        public ActionResult Index(int id)
        {
            return View(OrderItemManager.LoadByOrderID(id));
        }
    }
}
