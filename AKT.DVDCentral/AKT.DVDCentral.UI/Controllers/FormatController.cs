using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AKT.DVDCentral.BL;
using AKT.DVDCentral.BL.Models;

namespace AKT.DVDCentral.UI.Controllers
{
    public class FormatController : Controller
    {
        // GET: FormatController
        public ActionResult Index()
        {
            return View(FormatManager.Load());
        }

        // GET: FormatController/Details/5
        public ActionResult Details(int id)
        {
            return View(FormatManager.LoadByID(id));
        }

        // GET: FormatController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FormatController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Format format)
        {
            try
            {
                FormatManager.Insert(format);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: FormatController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(FormatManager.LoadByID(id));
        }

        // POST: FormatController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Format format)
        {
            try
            {
                FormatManager.Update(format);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: FormatController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(FormatManager.LoadByID(id));
        }

        // POST: FormatController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Format format)
        {
            try
            {
                FormatManager.Delete(id);
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
