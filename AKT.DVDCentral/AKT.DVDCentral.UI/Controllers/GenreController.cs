using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AKT.DVDCentral.BL;
using AKT.DVDCentral.BL.Models;

namespace AKT.DVDCentral.UI.Controllers
{
    public class GenreController : Controller
    {
        // GET: GenreController
        public ActionResult Index()
        {
            return View(GenreManager.Load());
        }

        // GET: GenreController/Details/5
        public ActionResult Details(int id)
        {
            return View(GenreManager.LoadByID(id));
        }

        // GET: GenreController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GenreController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Genre genre)
        {
            try
            {
                GenreManager.Insert(genre);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: GenreController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(GenreManager.LoadByID(id));
        }

        // POST: GenreController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Genre genre)
        {
            try
            {
                GenreManager.Update(genre);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: GenreController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(GenreManager.LoadByID(id));
        }

        // POST: GenreController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Genre genre)
        {
            try
            {
                GenreManager.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }
    }
}
