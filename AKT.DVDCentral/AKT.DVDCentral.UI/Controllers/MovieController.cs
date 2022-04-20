using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AKT.DVDCentral.BL;
using AKT.DVDCentral.BL.Models;

namespace AKT.DVDCentral.UI.Controllers
{
    public class MovieController : Controller
    {
        // GET: MovieController
        public ActionResult Index()
        {
            return View(MovieManager.Load());
        }
        public ActionResult GenreFilter(int id)
        {
            ViewBag.GenreDesc = GenreManager.LoadByID(id).Description;
            return View(MovieManager.LoadByGenreID(id));
        }
    }
}
