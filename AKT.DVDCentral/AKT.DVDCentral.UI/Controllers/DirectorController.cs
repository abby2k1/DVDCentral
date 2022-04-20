using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AKT.DVDCentral.BL;
using AKT.DVDCentral.BL.Models;

namespace AKT.DVDCentral.UI.Controllers
{
    public class DirectorController : Controller
    {
        // GET: DirectorController
        public ActionResult Index()
        {
            return View(DirectorManager.Load());
        }

        // GET: DirectorController/Details/5
        public ActionResult Details(int id)
        {
            return View(DirectorManager.LoadByID(id));
        }

        // GET: DirectorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DirectorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Director director)
        {
            try
            {
                DirectorManager.Insert(director);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: DirectorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(DirectorManager.LoadByID(id));
        }

        // POST: DirectorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Director director)
        {
            try
            {
                DirectorManager.Update(director);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: DirectorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(DirectorManager.LoadByID(id));
        }

        // POST: DirectorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Director director)
        {
            try
            {
                DirectorManager.Delete(id);
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
