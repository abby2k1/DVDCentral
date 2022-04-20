using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AKT.DVDCentral.BL;
using AKT.DVDCentral.BL.Models;

namespace AKT.DVDCentral.UI.Controllers
{
    public class RatingController : Controller
    {
        // GET: RatingController
        public ActionResult Index()
        {
            return View(RatingManager.Load());
        }

        // GET: RatingController/Details/5
        public ActionResult Details(int id)
        {
            return View(RatingManager.LoadByID(id));
        }

        // GET: RatingController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RatingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Rating rating)
        {
            try
            {
                RatingManager.Insert(rating);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: RatingController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag["Title"] = "Edit";
            return View(RatingManager.LoadByID(id));
        }

        // POST: RatingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Rating rating)
        {
            try
            {
                RatingManager.Update(rating);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: RatingController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(RatingManager.LoadByID(id));
        }

        // POST: RatingController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Rating rating)
        {
            try
            {
                RatingManager.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }
    }
}
